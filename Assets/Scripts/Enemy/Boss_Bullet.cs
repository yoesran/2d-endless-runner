using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Bullet : MonoBehaviour
{
    SoundControllerAll sound_controller_all;
    CharacterMoveController Player;
    public float lifetime = 3f;
    private float timer;
    public GameObject Hurt_effect;

    private void Start()
    {
        sound_controller_all = FindObjectOfType<SoundControllerAll>();
        Player = FindObjectOfType<CharacterMoveController>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        string tag = col.gameObject.tag;
        if (tag == "Player")
        {
            Instantiate(Hurt_effect, transform.position, Quaternion.identity);
            sound_controller_all.PlayPayer_Hurt();
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
