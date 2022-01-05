using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlySpawn : MonoBehaviour
{
    public GameObject EnemyFlyPrefab;
    public GameObject Platform;
    public int jumlah_spawn;
    private Transform player;

    private void Start()
    {
        StartCoroutine(new_spawn());
    }
    void Update()
    {
  /*      if (spawn_cek < jumlah_spawn)
        {
            StartCoroutine(new_spawn());
            spawn_cek += 1;
        }
        */
    }
    public void TakeDamage(int damage)
    {
        Destroy(this);
    }
    IEnumerator new_spawn()
    {
        while (true) {
            default_spawn();
            yield return new WaitForSeconds(1);
            
        }
        



    }
    void default_spawn()
    {
        bool spawn = false;
        while (!spawn)
        {
            float spawnY = Random.Range(4f, -4f);
            float spawnX = Random.Range(10f, 35f);
            Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);
            if (!Platform)
            {
                continue;
            }
            else
            {
                if ((spawnPosition - Platform.transform.position).magnitude < 3)
                {
                    continue;
                }
                else
                {
                    GameObject newObject = Instantiate(EnemyFlyPrefab, spawnPosition, Quaternion.identity);
                    newObject.transform.localScale = new Vector2(0.13f, 0.13f);
                    spawn = true;
                }
            }
        }
    }

}
