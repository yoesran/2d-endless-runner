using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBtn : MonoBehaviour
{
    public string SkillName;
    float cooldown;
    public float CoolTime;
    public float BasicAttackTime=10f;
    public Image CdImg;
    bool cool;
    CharacterMoveController Player;

    void Start()
    {
        Player = FindObjectOfType<CharacterMoveController>();
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
            StartCoroutine("CoolDown");
        }
    }
    public void Shoot()
    {
        if (!cool)
        {
            cool = true;
            Player.Fire();
            StartCoroutine("CoolDown");
        }
    }
    public void Spark()
    {
        if (!cool)
        {
            cool = true;
            Player.Spark();
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
