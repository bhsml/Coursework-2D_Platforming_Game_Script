using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    // Triggers death of objects or remove objects
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TriggerDeath();
        }
        else if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>().TriggerDeath();
        }
        else if (other.CompareTag("Collectable"))
        {
            other.GetComponent<Collectable>().TriggerCollect();
        }
    }
}
