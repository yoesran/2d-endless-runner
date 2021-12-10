using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBtn : MonoBehaviour
{
    public string SkillName;
    float cooldown;
    public float CoolTime;
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
          //  Player.BasicAttack = true;
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
