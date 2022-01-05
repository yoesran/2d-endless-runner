using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyBehavior : MonoBehaviour
{
    public float speed;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.eulerAngles = new Vector3(0, -180, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

  


    }
    public void TakeDamage(int damage)
    {
        Destroy(this);
    }

}
