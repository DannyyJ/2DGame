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

        // Sätt hälsa och skada beroende på svårighetsgrad
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
            rb.velocity = Vector2.zero; // Nollställ först för att undvika "stackad" kraft
            rb.AddForce(knockbackDir.normalized * knockbackForce, ForceMode2D.Impulse);
        }

        // Döda om hp <= 0
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy defeated!");
        Destroy(gameObject);
        // Här kan du t.ex. kalla på GameManager.Instance.GameWon() eller liknande
    }
}
