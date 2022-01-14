using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsController : MonoBehaviour
{
    CharacterMoveController Player;
    public float currentCoins = 0;
    public Text coins;
    private void Start()
    {
        coins = GetComponent<Text>();
        // reset
        Player = FindObjectOfType<CharacterMoveController>();
    }
    void Update()
    {
        currentCoins = Player.Coins;
        coins.text = currentCoins.ToString();
    }

    public float GetCurrentCoin()
    {
        return currentCoins;
    }


    public void FinishCoins()
    {
        // set high score
        float a = PlayerPrefs.GetFloat("Coins");
        PlayerPrefs.SetFloat("Coins", a += currentCoins);

    }
}
