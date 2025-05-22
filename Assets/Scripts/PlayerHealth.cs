using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int Health;
    public int maxHealth = 10;

    private MovementScript movementScript;

    void Start()
    {
        Health = maxHealth;
        movementScript = GetComponent<MovementScript>();
    }

    public void TakeDamage(int amount)
    {
        if (movementScript.isAttacking)
            return;

        Health -= amount;

        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
