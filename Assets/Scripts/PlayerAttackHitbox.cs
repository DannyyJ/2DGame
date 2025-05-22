using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHitbox : MonoBehaviour
{
    public int damage = 1;
    public float knockbackForce = 10f;
    public string enemyTag = "Enemy";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hitbox kolliderade med: " + collision.name);

        if (collision.CompareTag(enemyTag))
        {
            EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                Vector2 knockbackDir = (collision.transform.position - transform.root.position).normalized;
                enemy.TakeDamage(damage, knockbackDir);
                Debug.Log("Player hit enemy: " + collision.name);
            }
        }
    }
}
