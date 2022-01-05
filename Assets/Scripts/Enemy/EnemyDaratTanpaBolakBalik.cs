using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDaratTanpaBolakBalik : MonoBehaviour
{
    public float speed;
    public float lineOfSite;
    private Transform player;
    Vector3 StartPosition;
    // Start is called before the first frame update
    void Start()
    {
        StartPosition = this.transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.eulerAngles = new Vector3(0, -180, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
    
        if (distanceFromPlayer < lineOfSite)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.position.x, this.transform.position.y), speed * Time.deltaTime);
        }
        
        

    }
    public void TakeDamage(int damage)
    {
        Destroy(this);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }
}
