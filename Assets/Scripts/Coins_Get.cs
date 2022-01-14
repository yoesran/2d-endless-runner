using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins_Get : MonoBehaviour
{
    public Text coins;
    // Start is called before the first frame update
    void Start()
    {
        coins = GetComponent<Text>();
        coins.text = PlayerPrefs.GetFloat("Coins").ToString();
    }
    
}
