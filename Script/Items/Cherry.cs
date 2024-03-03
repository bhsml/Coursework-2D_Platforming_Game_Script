using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : Collectable
{
    // When Player touches Item collect Item
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TriggerCollect();
            PlayerCollectibleMenu.Instance.SetCountCherryText();
        }
    }
}
