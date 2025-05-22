using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public int damage;
    public float knockbackForce = 7f; // Justerbar knockback

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // S�tt h�lsa och skada beroende p� sv�righetsgrad
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
    public void TakeDamage(int amount, Vector2 knockbackDir)
    {
        health -= amount;
        Debug.Log("Enemy took damage! Health: " + health);

        // Knockback-effekt
        if (rb != null)
        {
            rb.velocity = Vector2.zero; // Nollst�ll f�rst f�r att undvika "stackad" kraft
            rb.AddForce(knockbackDir.normalized * knockbackForce, ForceMode2D.Impulse);
        }

        // D�da om hp <= 0
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy defeated!");
        Destroy(gameObject);
        // H�r kan du t.ex. kalla p� GameManager.Instance.GameWon() eller liknande
    }
}
