using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : Singleton<PlayerRespawn>
{
    // Hold Player; Used for player Respawn.
    [SerializeField]
    private GameObject player;

    // Respawns player by creating another one.
    public void RespawnPlayer()
    {
        // print("RespawnPlayer Start"); // To see Player respawn start
        Instantiate(player, transform.position, transform.rotation);
        PlayerHPCanvas.Instance.SetCountDeathText();
    }

    // Sets the respawn point using this objects transform.
    public void SetSpawnPoint(Transform point)
    {
        transform.position = point.position;
    }
}
