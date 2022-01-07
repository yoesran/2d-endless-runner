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

    public void OnClickBtn()
    {
        if (!cool)
        {
            cool = true;
            Player.BasicAttack = true;
            StartCoroutine("CoolDown");
            //     StartCoroutine("BasicAttack");
        }
    }
    public void Shoot()
    {
        if (!cool)
        {
            cool = true;
            Player.Fire();
            StartCoroutine("CoolDown");
            //     StartCoroutine("BasicAttack");
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
    IEnumerator BasicAttack()
    {
        cooldown = 0;
        while (true)
        {
            cooldown += Time.deltaTime;
            if (cooldown > BasicAttackTime)
            {
                Player.BasicAttack = false;
                StopCoroutine("BasicAttack");
            }
            yield return null;
        }
    }
}
