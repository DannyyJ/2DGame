using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int Health;
    public int maxHealth = 10;
    public float knockbackForce = 7f;

    private MovementScript movementScript;
    private Rigidbody2D rb;

    void Start()
    {
        Health = maxHealth;
        movementScript = GetComponent<MovementScript>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int amount, Vector2 knockbackDir)
    {
        if (movementScript.isAttacking)
            return;

        Health -= amount;

        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            Vector2 knockback = new Vector2(knockbackDir.x, 0.5f).normalized * knockbackForce;
            rb.AddForce(knockback, ForceMode2D.Impulse);
        }

        if (Health <= 0)
        {
            SceneManager.LoadScene(2);
        }
    }
}
