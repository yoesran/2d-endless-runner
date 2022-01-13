using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject GameWin;
    SoundControllerAll sound_controller_all;
    [Header("Skill")]
    public Rigidbody2D bulletPrefab;
    Camera_shake Camera;
    public GameObject Death_effect;
    //public Rigidbody2D slash;
    int e = 1;
    //public Rigidbody2D sparkPrefab;
    public float bulletPos;
    public float bulletSpeed;
    public GameObject shootPos;
    //public GameObject basicpos;
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
            Instantiate(Death_effect, col.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            Win();
        }
        else if(Health <= 50f )
        {
            sound_controller_all.PlayBoss_Malfunction();
            GetComponent<Renderer>().material.color = Color.red;
            Timer2cad = 1f;
        }
    }

    //public void Basic()
    //{
    //    sound.PlayBasic();
    //    anim.Play("BasicAttack");

    //    Rigidbody2D bPrefab = Instantiate(slash, basicpos.transform.position, basicpos.transform.rotation, gameObject.transform) as Rigidbody2D;
    //}
    public void Fire()
    {
        //sound.PlayShoot();
        //anim.Play("Shoot");
        //memunculkan peluru pada posisi gameobject shootpos
        Rigidbody2D bPrefab = Instantiate(bulletPrefab, shootPos.transform.position, shootPos.transform.rotation) as Rigidbody2D;
        //memberikan dorongan peluru sebesar bulletSpeed dengan arah terbangnya bulletPos 
        bPrefab.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletPos * bulletSpeed, 0));
        bPrefab.GetComponent<Rigidbody2D>().velocity = new Vector2(bPrefab.GetComponent<Rigidbody2D>().velocity.x, Random.Range(-2f, 5f)); ;
    }
    //public void Spark()
    //{
    //    sound.PlaySpark();
    //    anim.Play("Shoot");
    //    //memunculkan peluru pada posisi gameobject shootpos
    //    Rigidbody2D bPrefab = Instantiate(sparkPrefab, shootPos.transform.position, shootPos.transform.rotation) as Rigidbody2D;
    //    //memberikan dorongan peluru sebesar bulletSpeed dengan arah terbangnya bulletPos 
    //    bPrefab.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletPos * bulletSpeed, 0));
    //}
    private void Win()
    {
        // set high score
        //      score.FinishScoring();
        Time.timeScale = 0f;
        // stop camera movement
        // show gameover
        GameWin.SetActive(true);

        // disable this too
        this.enabled = false;
    }
}