using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static T instance;
    /* Unnecessary if remember to put the prefab into the game scene
    protected static string prefabLoc;
    protected static GameObject prefabInstance;
    */
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                // Look for existing object
                instance = GameObject.FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject myInst = new GameObject(typeof(T).Name + "(Singleton)");
                    instance = myInst.AddComponent<T>();
                }
                /* Unnecessary if remember to put the prefab into the game scene
                string prefabName = prefabLoc.Substring(prefabLoc.LastIndexOf("/") + 1);
                prefabInstance = GameObject.Find(prefabName);
                if (prefabInstance == null)
                {
                    //print("I Am prefablLoc: " + prefabLoc);
                    prefabInstance = Instantiate(Resources.Load(prefabLoc, typeof(GameObject))) as GameObject;
                }
                */
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this as T;
        }
    }

    /* Unnecessary if remember to put the prefab into the game scene
    protected static void setPrefabName(string x)
    {
        prefabLoc = x;
    }

    protected GameObject GetPrefabInstance()
    {
        return prefabInstance;
    }
    */
}
