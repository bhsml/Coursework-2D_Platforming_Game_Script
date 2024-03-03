using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    // Attack
    [SerializeField]
    protected int damage;
    [SerializeField]
    protected float pushBackForceY;
    //[SerializeField]
    protected bool damageContinue; // Check that the damage need to apply if player does not leave damaging area
    //[SerializeField]
    protected bool canAttack;
    private void Start()
    {
        damageContinue = false;
        canAttack = true;
    }

    // damages player
    // Or waits until player can be damaged
    // Loop is exited when player leave damage zone
    protected virtual IEnumerator InvulnWearOff(Collider2D other)
    {
        // print("Character enter Coroutine"); // See if character started the Coroutine
        if (other.CompareTag("Player"))
        {
            while (damageContinue)
            {
                //print("damageContinue"); // See if Damage Continues
                yield return new WaitUntil(() => other.gameObject.GetComponent<PlayerHealth>().CanHurt() == true); // Waits until player can be damaged
                other.gameObject.GetComponent<PlayerHealth>().AddDamage(damage);
                PushBack(other.transform);
            }
            
        }

    }
    
    // Damage other, if able to attack
    // Sets damageContinue to true to continue attack
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        damageContinue = true;
        //print("damageContinue True");  //See if trigger true
        if (canAttack) 
            StartCoroutine("InvulnWearOff", other);
    }
    
    // Sets damageContinue to false to stop attack
    // And stops InvulnWearOff
    private void OnTriggerExit2D(Collider2D other)
    {
        StopCoroutine("InvulnWearOff");
        damageContinue = false;
        //print("damageContinue False"); //See if trigger false
    }

    // Pushback player in y axis
    protected void PushBack(Transform pushedObject)
    {
        pushedObject.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, pushBackForceY);
    }

    // Make enemy not able to attack anymore
    public void TurnOffAttack()
    {
        canAttack = false;
    }
}
