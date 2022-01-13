using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveController : MonoBehaviour
{
    [Header("Skill")]
    public Rigidbody2D bulletPrefab;
    Camera_shake Camera;
    public Rigidbody2D slash;
    int e = 1;
    public Rigidbody2D sparkPrefab;
    public float bulletPos;
    public float bulletSpeed;
    public GameObject shootPos;
    public GameObject basicpos;
    public GameObject basicAttack;
    public GameObject Death_effect_character;

    public float Health = 100f;
    public float Coins = 0f;
    float BlinkCooldown;
    
    

    [Header("Movement")]
    public float moveAccel;
    public float maxSpeed;
    public float velocity_y;

    [Header("Jump")]
    public float jumpAccel;

    [Header("Ground Raycast")]
    public float groundRaycastDistance;
    public LayerMask groundLayerMask;

    [Header("Scoring")]
    public ScoreController score;
    public float scoringRatio;

    [Header("GameOver")]
    public GameObject gameOverScreen;
    public float fallPositionY;

    [Header("Camera")]
    public CameraMoveController gameCamera;

    private Rigidbody2D rig;
    private Animator anim;
    public CharacterSoundController sound;
    public Color color = Color.red;
    

    public static bool isJumping = false;
    public bool canDoubleJump = false;
    public static bool isOnGround;
    public static bool DoubleJump;

    private float lastPositionX;

    private void Start()
    {
        
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sound = GetComponent<CharacterSoundController>();
        lastPositionX = transform.position.x;
        Camera = FindObjectOfType<Camera_shake>();
    }

    private void Update()
    {
        if (velocity_y <= -50)
        {
            Die();
        }
        if (e == 1)
        {
            sound.PlayStartSound();
            e += 1;
        }    
        anim.SetBool("Jumping", isJumping);
        anim.SetBool("DoubleJump", DoubleJump);

        // calculate score
        int distancePassed = Mathf.FloorToInt(transform.position.x - lastPositionX);
        int scoreIncrement = Mathf.FloorToInt(distancePassed / scoringRatio);

        if (scoreIncrement > 0)
        {
            score.IncreaseCurrentScore(scoreIncrement);
            lastPositionX += distancePassed;
        }

        // game over
        if (Health<=0f)
        {
            Die();
        }
    }
    public void JumpButton()
    {
        if (isOnGround == true)
        {
            rig.velocity = new Vector2(rig.velocity.x, jumpAccel);
            isJumping = true;
            canDoubleJump = true;
            sound.PlayJump();

        }
        else if (isJumping == true && canDoubleJump == true)
        {
            rig.velocity = new Vector2(rig.velocity.x, jumpAccel - 7);
            canDoubleJump = false;
            DoubleJump = true;
            sound.PlayJump();
            
        }
    }

    private void FixedUpdate()
    {
        // calculate velocity vector
        Vector2 velocityVector = rig.velocity;

        velocity_y = velocityVector.y;
        velocityVector.x = Mathf.Clamp(velocityVector.x + moveAccel * Time.deltaTime, 0.0f, maxSpeed);

        rig.velocity = velocityVector;
    }

    private void GameOver()
    {
        Time.timeScale = 0f;
        // stop camera movement
        gameCamera.enabled = false;

        // show gameover
        gameOverScreen.SetActive(true);

        // disable this too
        this.enabled = false;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position + (Vector3.down * groundRaycastDistance), Color.white);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && rig.velocity.y <= 0)
        {
            isOnGround = true;
            isJumping = false;
            DoubleJump = false;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        isOnGround = false;    
    }

    public void Basic()
    {
        sound.PlayBasic();
        anim.Play("BasicAttack");
    }
    public void Sword()
    {
        sound.PlayBasic();
        anim.Play("SkillSword");
    }
    public void Fire()
    {
        sound.PlayShoot();
        anim.Play("Shoot");
        //memunculkan peluru pada posisi gameobject shootpos
        Rigidbody2D bPrefab = Instantiate(bulletPrefab, shootPos.transform.position, shootPos.transform.rotation) as Rigidbody2D;
        //memberikan dorongan peluru sebesar bulletSpeed dengan arah terbangnya bulletPos 
        bPrefab.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletPos * bulletSpeed, 0));
    }
    public void Spark()
    {
        sound.PlaySpark();
        anim.Play("Shoot");
        Rigidbody2D bPrefab = Instantiate(sparkPrefab, shootPos.transform.position, shootPos.transform.rotation) as Rigidbody2D;
        bPrefab.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletPos * bulletSpeed, 0));
    }
    private void Die()
    {
        Instantiate(Death_effect_character, transform.position, Quaternion.identity);
        sound.PlayPlayerDestroy();
        GameOver();
        Destroy(this.gameObject);
    }
}