using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    CharacterMoveController Player;
    public int lifetime = 2;
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
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        string tag = col.gameObject.tag;
        if (tag == "Enemy")
        {
            Destroy(col.gameObject);
            Player.Coins += 5;
            Destroy(gameObject);
        }
        else if (tag == "Enemy Fly")
        {
            Destroy(col.gameObject);
            Player.Coins += 10;
            Destroy(gameObject);
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
