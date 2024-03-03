using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    // Uses the trigger variable of animator to destroy item
    public void TriggerCollect()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        gameObject.GetComponent<Animator>().SetTrigger("collected");
    }

    // Destroy object
    protected void DestroyItem()
    {
        Destroy(gameObject);
    }
}
