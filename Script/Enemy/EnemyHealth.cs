using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
    // Health
    [SerializeField]
    private int enemyMaxHealth;
    [SerializeField]
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = enemyMaxHealth;
    }

    // Make Enemy lose health
    public void AddDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            TriggerDeath();
        }
    }

    // Triggers death for Animator Trigger
    public void TriggerDeath()
    {
        EnemyDamage enemyAttack = gameObject.GetComponentInChildren<EnemyDamage>();
        enemyAttack.TurnOffAttack(); // Enemy can't retaliate when dead.
        BoxCollider2D[] box = GetComponentsInChildren<BoxCollider2D>();
        foreach (BoxCollider2D b in box)
        {
            if(b.CompareTag("Enemy AI"))
            {
                b.enabled = false;
            }
        }
        GetComponent<Collider2D>().enabled = false; // Disable the collider for Health;
        GetComponent<Rigidbody2D>().isKinematic = true; // So Gravity does not affect Body
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0); // When Dying, should not move
        gameObject.GetComponent<Animator>().SetTrigger("die");
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
