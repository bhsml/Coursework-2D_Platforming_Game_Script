using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPCanvas : Singleton<PlayerHPCanvas>
{
    //const string prefabLoc = "Prefab/Canvas/PlayerHPCanvas"; // Would not need it if it exist in game scene

    /*
    private void Awake()
    {
        //setPrefabName(prefabLoc); // Would not need it if it exist in game scene
    }
    */

    // Counting Deaths
    int deathCount;
    [SerializeField]
    Text countDeathsText;

    private void Start()
    {
        deathCount = 0;
        countDeathsText.text = deathCount.ToString();
    }


    // Returns this gameObject or the object that this script is a component for.
    // So returns PlayerHPCanvas
    public GameObject GetObject()
    {
        return gameObject;
    }

    // Set Count of Deaths
    public void SetCountDeathText()
    {
        ++deathCount;
        countDeathsText.text = deathCount.ToString();
    }
}
