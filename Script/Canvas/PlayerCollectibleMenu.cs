using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollectibleMenu : Singleton<PlayerCollectibleMenu>
{
    // Gotta get instance of it
    // Item Count
    int countCherry;
    int countGem;
    // Item Text;
    [SerializeField]
    Text countCherryText;
    [SerializeField]
    Text countGemText;
    
    // Start is called before the first frame update
    void Start()
    {
        countCherry = 0;
        countGem = 0;
        countCherryText.text = countCherry.ToString();
        countGemText.text = countCherry.ToString();
    }

    // Set Count of Cherry collected
    public void SetCountCherryText()
    {
        ++countCherry;
        countCherryText.text = countCherry.ToString();
    }

    // Set Count of Gem collected
    public void SetCountGemText()
    {
        ++countGem;
        countGemText.text = countGem.ToString();
    }
}
