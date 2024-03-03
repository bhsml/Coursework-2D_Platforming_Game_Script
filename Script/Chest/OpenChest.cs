using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    // Spawn point for item
    [SerializeField]
    private Transform SpawnPoint;
    // Item
    [SerializeField]
    private GameObject[] item;
    // Force for ejecting item
    [SerializeField]
    private float yForce;
    
    private void Start()
    {
        this.GetComponent<Animator>().speed = 0; // Stop Animator from playing animation
        if (SpawnPoint == null) // Getting the spawn point transform
        {
            Transform[] tCollect;
            tCollect = this.GetComponentsInChildren<Transform>();
            foreach (Transform t in tCollect)
            {
                if (t.CompareTag("Spawn Point"))
                {
                    SpawnPoint = t;
                }
            }
        }
    }
    // Opens Chest
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            this.GetComponent<Animator>().speed = 1; // Player Animation
            CreateItem();
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    // Create Item
    private void CreateItem()
    {
        GameObject newItem = Instantiate(item[0], SpawnPoint.position, SpawnPoint.rotation);
        newItem.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, yForce), ForceMode2D.Impulse);
    }

    // Disable Animator so that it doesn't run. Also it stop animation.
    public void DisableAnimator()
    {
        GetComponent<Animator>().enabled = false;
    }
}
