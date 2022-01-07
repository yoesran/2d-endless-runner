using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    CharacterMoveController Player;
    float lifetime = 1f;
    private float timer;


    private void Start()
    {
        Player = FindObjectOfType<CharacterMoveController>();
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        string tag = col.gameObject.tag;
        if (tag == "Enemy")
        {
            Destroy(col.gameObject);
            Player.Coins += 5;
        }
        else if (tag == "Enemy Fly")
        {
            Destroy(col.gameObject);
            Player.Coins += 10;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        string tag = col.gameObject.tag;
        if (tag == "Enemy")
        {
            Destroy(col.gameObject);
            Player.Coins += 5;
        }
        else if (tag == "Enemy Fly")
        {
            Destroy(col.gameObject);
            Player.Coins += 10;
        }
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > lifetime)
        {
            timer = 0;
            Destroy(gameObject);
        }
    }
}
