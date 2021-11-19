using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesGeneratorController : MonoBehaviour
{
    [Header("Templates")]
    public List<GameObject> obstaclesTemplates;
    public float obstaclesWidth;
        
    [Header("Generator Area")]
    public Camera gameCamera;
    public float areaStartOffset;
    public float areaEndOffset;
    
    private List<GameObject> spawnedObstacles;

    private float lastGeneratedPositionX;
    private float lastRemovedPositionX;

    private const float debugLineHeight = 10.0f;

    // pool list
    private Dictionary<string, List<GameObject>> pool;

    private void Start()
    {
        // init pool
        pool = new Dictionary<string, List<GameObject>>();
        
        spawnedObstacles = new List<GameObject>();

        lastGeneratedPositionX = GetHorizontalPositionStart();
        lastRemovedPositionX = lastGeneratedPositionX - obstaclesWidth;
    }

    private void Update()
    {
        while (lastGeneratedPositionX < GetHorizontalPositionEnd())
        {
            GenerateObstacles(lastGeneratedPositionX);
            lastGeneratedPositionX += obstaclesWidth;
        }

        while (lastRemovedPositionX + obstaclesWidth < GetHorizontalPositionStart())
        {
            lastRemovedPositionX += obstaclesWidth;
            RemoveObstacles(lastRemovedPositionX);
        }
    }

    private float GetHorizontalPositionStart()
    {
        return gameCamera.ViewportToWorldPoint(new Vector2(0f, 0f)).x + areaStartOffset;
    }

    private float GetHorizontalPositionEnd()
    {
        return gameCamera.ViewportToWorldPoint(new Vector2(1f, 0f)).x + areaEndOffset;
    }

    private void GenerateObstacles(float posX)
    {
        GameObject newObstacles = null;
        
        newObstacles = GenerateFromPool(obstaclesTemplates[Random.Range(0, obstaclesTemplates.Count)], transform);
        
        newObstacles.transform.position = new Vector2(posX, -5f);

        spawnedObstacles.Add(newObstacles);
    }

    private void RemoveObstacles(float posX)
    {
        GameObject obstaclesToRemove = null;


        // find obstacles at posX
        foreach (GameObject item in spawnedObstacles)
        {
            if (item.transform.position.x == posX)
            {
                obstaclesToRemove = item;
                break;
            }
        }

        // after found;

        if (obstaclesToRemove != null)
        {
            spawnedObstacles.Remove(obstaclesToRemove);
            ReturnToPool(obstaclesToRemove);
        }
    }

    // pool function
    private GameObject GenerateFromPool(GameObject item, Transform parent)
    {
        if (pool.ContainsKey(item.name))
        {
            // if item available in pool
            if (pool[item.name].Count > 0)
            {
                GameObject newItemFromPool = pool[item.name][0];
                pool[item.name].Remove(newItemFromPool);
                newItemFromPool.SetActive(true);
                return newItemFromPool;
            }
        }
        else
        {
            // if item list not defined, create new one
            pool.Add(item.name, new List<GameObject>());
        }

        // create new one if no item available in pool
        GameObject newItem = Instantiate(item, parent);
        newItem.name = item.name;
        return newItem;
    }

    private void ReturnToPool(GameObject item)
    {
        if (!pool.ContainsKey(item.name))
        {
            Debug.LogError("INVALID POOL ITEM!!");
        }

        pool[item.name].Add(item);
        item.SetActive(false);
    }

    // debug
    private void OnDrawGizmos()
    {
        Vector3 areaStartPosition = transform.position;
        Vector3 areaEndPosition = transform.position;

        areaStartPosition.x = GetHorizontalPositionStart();
        areaEndPosition.x = GetHorizontalPositionEnd();

        Debug.DrawLine(areaStartPosition + Vector3.up * debugLineHeight / 2, areaStartPosition + Vector3.down * debugLineHeight / 2, Color.red);
        Debug.DrawLine(areaEndPosition + Vector3.up * debugLineHeight / 2, areaEndPosition + Vector3.down * debugLineHeight / 2, Color.red);
    }
}
