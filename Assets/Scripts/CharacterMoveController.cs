using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveController : MonoBehaviour
{
    public Transform AttackPosition;
    public float AttackRangeX;
    public float AttackRangeY;
    public LayerMask WhatIsEnemies;
    public int damage;

    public float Health = 100f;
    public float Coins = 0f;
    float BlinkCooldown;
    float BlinkTime = 0.5f;
    public GameObject Object_Player;
    

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
    private CharacterSoundController sound;
    public Color color = Color.red;
    

    private bool isJumping = false;
    private bool canDoubleJump = false;
    public bool isOnGround;
    public bool BasicAttack = false;
    public bool DoubleJump;

    private float lastPositionX;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sound = GetComponent<CharacterSoundController>();
        lastPositionX = transform.position.x;
    }

    private void Update()
    {
        // change animation
        if (BasicAttack == true)
        {
            playBasicAttackAnimation();
            BasicAttack = false;
        }
        anim.SetBool("Jumping", isJumping);
        anim.SetBool("BasicAttack", BasicAttack);
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
        if (Health==0f)
        {
            GameOver();
            Destroy(this.gameObject);
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
            rig.velocity = new Vector2(rig.velocity.x, jumpAccel - 5);
            canDoubleJump = false;
            DoubleJump = true;
        }
    }
    public void BasicAttackButton()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(AttackPosition.position, new Vector2(AttackRangeX, AttackRangeY),0, WhatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<EnemyFollowPlayer>().TakeDamage(damage);
            Coins += 5;
        }
    }

    public void playBasicAttackAnimation()
    {
        anim.Play("BasicAttack");
    }
    public void ButtonJump()
    {
        anim.Play("BasicAttack");
    }
    private void FixedUpdate()
    {
        // raycast ground
   /*     RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundRaycastDistance, groundLayerMask);
        if (hit)
        {
            if (!isOnGround && rig.velocity.y <= 0)
            {
                isOnGround = true;
            }
        }
        else
        {
            isOnGround = false;
        }
*/
        // calculate velocity vector
        Vector2 velocityVector = rig.velocity;

        velocity_y = velocityVector.y;
        velocityVector.x = Mathf.Clamp(velocityVector.x + moveAccel * Time.deltaTime, 0.0f, maxSpeed);

        rig.velocity = velocityVector;
    }

    private void GameOver()
    {
        // set high score
  //      score.FinishScoring();

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

    void OnTriggerEnter2D(Collider2D col)
    {
        string tag = col.gameObject.tag;
        if (tag == "Obstacle")
        {
            Health -= 10;
     /*      color.a = 0.1f;
            GetComponent<Renderer>().material.color = color;
            StartCoroutine("CoolDown");

    */
        }
        if (tag == "Enemy")
        {
            Destroy(col.gameObject);
            Health -= 20;
    /*      color.a = 0.1f;
            GetComponent<Renderer>().material.color = color;
            StartCoroutine("CoolDown");
       */
        }
        if (tag == "Coin")
        {
            Destroy(col.gameObject);
            Coins += 1;
            /*      color.a = 0.1f;
                    GetComponent<Renderer>().material.color = color;
                    StartCoroutine("CoolDown");
               */
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
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


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(AttackPosition.position, new Vector3(AttackRangeX, AttackRangeY, 1));
    }


    /*
    IEnumerator CoolDown()
    {
        BlinkCooldown = 0;
        while (true)
        {
            BlinkCooldown += Time.deltaTime;
            if (BlinkCooldown > BlinkTime)
            {
                color.a = 1f;
                GetComponent<Renderer>().material.color = color;
                StopCoroutine("CoolDown");
            }
            yield return null;
        }
    }

    */
}