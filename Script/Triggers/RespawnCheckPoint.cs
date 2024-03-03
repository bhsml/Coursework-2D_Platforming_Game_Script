using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnCheckPoint : MonoBehaviour
{
    // Spawn point when reaching checkpoint
    [SerializeField]
    private Transform SpawnPoint;
    private void Start()
    {
        if(SpawnPoint == null)
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerRespawn.Instance.SetSpawnPoint(SpawnPoint);
        }
    }
}
