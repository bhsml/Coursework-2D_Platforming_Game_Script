using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : EnemyDamage
{
    // overrides method to allow attacking Enemies
    override protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            damageContinue = true;
            StartCoroutine("InvulnWearOff", other);
        }
        else if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyHealth>().AddDamage(damage);
        }

    }
}
