using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    public float speed;
    public float lineOfSite;
    public float rayDist;
    private bool movingRight;
    public Transform groundDetect;
    private Transform player;
    Vector3 StartPosition;
    // Start is called before the first frame update
    void Start()
    {
        StartPosition = this.transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundCheck = Physics2D.Raycast(groundDetect.position, Vector2.down, rayDist);

        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
    
        if (distanceFromPlayer < lineOfSite)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.position.x, this.transform.position.y), speed * Time.deltaTime);
        }
        else if (groundCheck.collider==false)
        {
            transform.position = new Vector3(transform.position.x, StartPosition.y,transform.position.z);
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
        

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, rayDist);
    }
}
