using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    
    SoundControllerAll sound_controller_all;
    ScoreController score;
    [Header("Skill")]
    public Rigidbody2D bulletPrefab;
    Camera_shake Camera;
    public GameObject Death_effect;
    int e = 1;
    public float bulletPos;
    public float bulletSpeed;
    public GameObject shootPos;
    public float Timer = 15f;
    public float Timercad;
    float Timer2 = 5f;
    float Timer2cad;
    public float Health = 100f;

    [Header("Movement")]
    public float moveAccel;
    public float maxSpeed;

    [Header("Jump")]


    private Rigidbody2D rig;
    private Animator anim;
    private CharacterSoundController sound;
    public Color color = Color.red;

    private Transform player;

    float cooldown;
    float CoolTime = 0.5f;

    private void Start()
    {
        sound_controller_all = FindObjectOfType<SoundControllerAll>();
        player = GameObject.FindGameObjectWithTag("Attack_Posisi_Player").transform;
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sound = GetComponent<CharacterSoundController>();
        Timercad = Timer;
        Timer2cad = Timer2;
        Camera = FindObjectOfType<Camera_shake>();
        score = FindObjectOfType<ScoreController>();
        if (score.level >= 4)
        {
            Health = 300;
            Boss_Health.MaxHealth = 300f;
            transform.localScale += new Vector3(0.1f, 0.1f, 0);
            transform.position = new Vector2(this.transform.position.x,this.transform.position.y+0.3f);
        }
    }

    private void Update()
    {
        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
        }
        else
        {
            Timer = Timercad;
        }
        if (Timer <= 10f && Timer > 6)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.position.x, this.transform.position.y), 5 * Time.deltaTime);
        }
        else if (Timer <= 6f && Timer >= 4)
        {

            maxSpeed = 15;
        }
        else
        {
            maxSpeed = 5;
        }


        if (Timer2 > 0)
        {
            Timer2 -= Time.deltaTime;
        }
        else
        {
            sound_controller_all.PlayBoss_Laser();
            Fire();
            Timer2 = Timer2cad;
        }

        if (e == 1)
        {
            e += 1;
        }

    }

    private void FixedUpdate()
    {
        Vector2 velocityVector = rig.velocity;

        velocityVector.x = Mathf.Clamp(velocityVector.x + moveAccel * Time.deltaTime, 0.0f, maxSpeed);

        rig.velocity = velocityVector;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        string tag = col.gameObject.tag;
        if (tag == "Bullet")
        {
            Camera.start = true;
            Destroy(col.gameObject);
            Health -= 10;
            /*      color.a = 0.1f;
                 GetComponent<Renderer>().material.color = color;
                 StartCoroutine("CoolDown");

         */
        }
        if (tag == "Skill_Spark")
        {
            Camera.start = true;
            Health -= 10;
        }
        if (tag == "Skill_Slash")
        {
            Camera.start = true;
            Health -= 10;
        }
        if (Health <= 0f)
        {
            sound_controller_all.stop();
            Camera.start = true;
            Instantiate(Death_effect, transform.position, Quaternion.identity);
            Instantiate(Death_effect, transform.position, Quaternion.identity);
            StartCoroutine("CoolDown");
            Time.timeScale = 0.1f;
        }
        else if(Health <= Health/2)
        {
            sound_controller_all.PlayBoss_Malfunction();
            GetComponent<Renderer>().material.color = Color.red;
            Timer2cad = 1f;
        }
    }


    public void Fire()
    {
        float ran= Random.Range(-2f, 5f);
        if (score.level >= 5)
        {
            ran = player.position.y;
        }
        Rigidbody2D bPrefab = Instantiate(bulletPrefab, shootPos.transform.position, shootPos.transform.rotation) as Rigidbody2D;
        bPrefab.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletPos * bulletSpeed, 0));
        bPrefab.GetComponent<Rigidbody2D>().velocity = new Vector2(bPrefab.GetComponent<Rigidbody2D>().velocity.x, ran); ;
    }


    IEnumerator CoolDown()
    {
        cooldown = 0;
        while (true)
        {
            cooldown += Time.deltaTime;
            if (cooldown > (CoolTime-0.1))
            {
                transform.localScale = new Vector2(0, 0);
            }
            if (cooldown > CoolTime)
            {

                Destroy(this.gameObject);
                StopCoroutine("CoolDown");
                score.Win();
            }
            yield return null;
        }
    }
}