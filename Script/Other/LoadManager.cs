using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadManager : Singleton<LoadManager>
{
    private void Awake()
    {
        Instantiate(Resources.Load("Prefab/Canvas/ItemCanvas", typeof(GameObject)));
        Instantiate(Resources.Load("Prefab/Canvas/PlayerHPCanvas", typeof(GameObject)));
        Instantiate(Resources.Load("Prefab/Player/PlayerRespawn", typeof(GameObject)));
    }
}
