using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBtn : MonoBehaviour
{
    public string SkillName;
    float cooldown;
    float CoolTime;
    float CoolTime_basic = 1;
    float CoolTime_shoot = 5;
    float CoolTime_sword = 5;
    float CoolTime_spark = 15;
    //public float BasicAttackTime=10f;
    public Image CdImg;
    bool cool;
    CharacterMoveController Player;

    void Start()
    {
        Player = FindObjectOfType<CharacterMoveController>();
        CoolTime_shoot = CoolTime_shoot - CoolTime_shoot * (PlayerPrefs.GetFloat("Shoot") / 100);
        CoolTime_sword = CoolTime_sword - CoolTime_sword * (PlayerPrefs.GetFloat("Sword") / 100);
        CoolTime_spark = CoolTime_spark - CoolTime_spark * (PlayerPrefs.GetFloat("Spark") / 100);
    }

    public void JumpButton()
    {
        if (!cool)
        {
            Player.JumpButton();
        }
    }
    public void BasicAttack()
    {
        if (!cool)
        {
            cool = true;
            Player.Basic();
            CoolTime = CoolTime_basic;
            StartCoroutine("CoolDown");
        }
    }
    public void Sword()
    {
        if (!cool)
        {
            cool = true;
            Player.Sword();
            CoolTime = CoolTime_sword;
            StartCoroutine("CoolDown");
        }
    }
    public void Shoot()
    {
        if (!cool)
        {
            cool = true;
            Player.Fire();
            CoolTime = CoolTime_shoot;
            StartCoroutine("CoolDown");
        }
    }
    public void Spark()
    {
        if (!cool)
        {
            cool = true;
            Player.Spark();
            CoolTime = CoolTime_spark;
            StartCoroutine("CoolDown");
        }
    }
    IEnumerator CoolDown()
    {
        cooldown = 0;
        while (true)
        {
            cooldown += Time.deltaTime;
            CdImg.fillAmount = cooldown / CoolTime;
            if (cooldown > CoolTime)
            {
                cool = false;
                StopCoroutine("CoolDown");
            }
            yield return null;
        }
    }
}
