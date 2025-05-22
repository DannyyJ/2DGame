using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public int damage;
    public float knockbackForce = 7f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (GameManager.Instance != null)
        {
            switch (GameManager.Instance.currentDifficulty)
            {
                case GameManager.Difficulty.Easy:
                    health = 3;
                    damage = 1;
                    break;
                case GameManager.Difficulty.Medium:
                    health = 10;
                    damage = 3;
                    break;
                case GameManager.Difficulty.Hard:
                    health = 15;
                    damage = 4;
                    break;
            }
        }
        else
        {
            health = 3;
            damage = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MovementScript player = collision.gameObject.GetComponent<MovementScript>();
            if (player != null && !player.isAttacking)
            {
                PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    Vector2 knockbackDir = (collision.transform.position - transform.position).normalized;
                    playerHealth.TakeDamage(damage, knockbackDir);
                    Debug.Log("Enemy damaged player!");

                    // Knockback på spelaren hanteras i PlayerHealth nu
                }
            }
        }
    }

    public void TakeDamage(int amount, Vector2 knockbackDir)
    {
        health -= amount;
        Debug.Log("Enemy took damage! Remaining health: " + health);

        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(knockbackDir.normalized * knockbackForce, ForceMode2D.Impulse);
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy defeated!");
        SceneManager.LoadScene(3);
    }
}
