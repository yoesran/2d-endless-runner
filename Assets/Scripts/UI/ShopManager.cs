using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Slider Sword, Shoot, Spark;
    float max = 90;
    float currentSword, currentShoot, currentSpark, coins;
    public Text swordText, shootText, sparkText;
    public Text coinsText, swordPrice, shootPrice, sparkPrice;
    float priceSword, priceShoot, priceSpark;

    float CoolTime_basic = 1;
    float CoolTime_shoot = 5;
    float CoolTime_sword = 5;
    float CoolTime_spark = 15;
    public Text cooldown_basic, cooldown_shoot, cooldown_spark;

    void Start()
    {
        CoolTime_shoot = CoolTime_shoot - CoolTime_shoot * (PlayerPrefs.GetFloat("Shoot") / 100);
        CoolTime_sword = CoolTime_sword - CoolTime_sword * (PlayerPrefs.GetFloat("Sword") / 100);
        CoolTime_spark = CoolTime_spark - CoolTime_spark * (PlayerPrefs.GetFloat("Spark") / 100);
        GetSet();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.DeleteKey("Sword");
            PlayerPrefs.DeleteKey("Shoot");
            PlayerPrefs.DeleteKey("Spark");
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    void GetSet()
    {
        currentSword = PlayerPrefs.GetFloat("Sword");
        currentShoot = PlayerPrefs.GetFloat("Shoot");
        currentSpark = PlayerPrefs.GetFloat("Spark");

        Sword.maxValue = max;
        Shoot.maxValue = max;
        Spark.maxValue = max;

        Sword.value = currentSword;
        Shoot.value = currentShoot;
        Spark.value = currentSpark;

        price();
    }
    public void buySword()
    {
        if (currentSword < max)
        {
            if (coins >= priceSword)
            {
                coins -= priceSword;
                PlayerPrefs.SetFloat("Coins", coins);
                currentSword += 10;
                PlayerPrefs.SetFloat("Sword", currentSword);
                Sword.value = currentSword;
                Application.LoadLevel(Application.loadedLevel);
                //price();
            }
            else
            {
                swordText.text = "Not enough coins ";
            }
        }
        else
        {
            swordText.text = "Max";
        }
    }
    public void buyShoot()
    {
        if (currentShoot < max)
        {
            if (coins >= priceShoot)
            {
                coins -= priceShoot;
                PlayerPrefs.SetFloat("Coins", coins);
                currentShoot += 10;
                PlayerPrefs.SetFloat("Shoot", currentShoot);
                Shoot.value = currentShoot;
                Application.LoadLevel(Application.loadedLevel);
                //price();
            }
            else
            {
                shootText.text = "Not enough coins ";
            }
        }
        else
        {
            shootText.text = "Max";
        }
    }
    public void buySpark()
    {
        if (currentSpark < max)
        {
            if (coins >= priceSpark)
            {

                coins -= priceSpark;
                PlayerPrefs.SetFloat("Coins", coins);
                currentSpark += 10;
                PlayerPrefs.SetFloat("Spark", currentSpark);
                Spark.value = currentSpark;
                Application.LoadLevel(Application.loadedLevel);
                //price();
            }
            else
            {
                sparkText.text = "Not enough coins ";
            }
        }
        else
        {
            sparkText.text = "Max";
        }
    }
    public void price()
    {
        coins = PlayerPrefs.GetFloat("Coins");

        priceSword = currentSword * 5;
        priceShoot = currentShoot * 5;
        priceSpark = currentSpark * 10;
        if (priceSword == 0)
        {
            priceSword = 25;
        }
        if (priceShoot == 0)
        {
            priceShoot = 25;
        }
        if (priceSpark == 0)
        {
            priceSpark = 50;
        }




        cooldown_basic.text = CoolTime_sword.ToString();
        cooldown_shoot.text = CoolTime_shoot.ToString();
        cooldown_spark.text = CoolTime_spark.ToString();

        coinsText.text = coins.ToString();
        swordPrice.text = priceSword.ToString();
        shootPrice.text = priceShoot.ToString();
        sparkPrice.text = priceSpark.ToString();

    }
    public void ResetCoins()
    {
        PlayerPrefs.SetFloat("Coins",1000);
        Application.LoadLevel(Application.loadedLevel);
    }
}
