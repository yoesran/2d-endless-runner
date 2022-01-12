using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Col_Basic : MonoBehaviour
{
    CharacterMoveController Player;
    Camera_shake Camera;
    //float lifetime = 4f;
    //private float timer;
    public GameObject Death_effect;
    private Animator camAnim;

    private void Start()
    {
        Player = FindObjectOfType<CharacterMoveController>();
        Camera = FindObjectOfType<Camera_shake>();
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        string tag = col.gameObject.tag;
        if (tag == "Enemy")
        {
            Camera.start = true;
            Instantiate(Death_effect, col.transform.position, Quaternion.identity);
            camAnim.Play("Shake");
            Destroy(col.gameObject);
            Player.Coins += 5;
        }
        else if (tag == "Enemy Fly")
        {
            Camera.start = true;
            Instantiate(Death_effect, col.transform.position, Quaternion.identity);
            Destroy(col.gameObject);
            Player.Coins += 10;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        string tag = col.gameObject.tag;
        if (tag == "Enemy")
        {
            Camera.start = true;
            Instantiate(Death_effect, col.transform.position, Quaternion.identity);
            Destroy(col.gameObject);
            Player.Coins += 5;
        }
        else if (tag == "Enemy Fly")
        {
            Camera.start = true;
            Instantiate(Death_effect, col.transform.position, Quaternion.identity);
            Destroy(col.gameObject);
            Player.Coins += 10;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //timer += Time.deltaTime;
        //if (timer > lifetime)
        //{
        //    timer = 0;
        //    Destroy(gameObject);
        //}
    }
}

