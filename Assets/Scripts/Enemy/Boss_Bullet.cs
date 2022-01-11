using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Bullet : MonoBehaviour
{
    CharacterMoveController Player;
    public float lifetime = 3f;
    private float timer;


    private void Start()
    {
        Player = FindObjectOfType<CharacterMoveController>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        string tag = col.gameObject.tag;
        if (tag == "Player")
        {
            Player.Health -= 10;
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
