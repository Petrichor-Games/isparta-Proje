using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinLine : MonoBehaviour
{
    public GameObject coinPrefab;
    public GameObject coinSpawn;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        SpawnCoins(coinSpawn);
    }

    void SpawnCoins(GameObject asd)
    {
        int coinsToSpawn = 5;
        for (int i = 0; i < coinsToSpawn; i++)
        {
            GameObject temp = Instantiate(coinPrefab, asd.transform);
            temp.transform.position = GetRandomPointInCollider(asd.GetComponent<Collider>());
        }
    }


    Vector3 GetRandomPointInCollider(Collider collider)
    {
        Vector3 point = new Vector3(
            Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            Random.Range(collider.bounds.min.z, collider.bounds.max.z)
            );

        if (point != collider.ClosestPoint(point))
        {
            point = GetRandomPointInCollider(collider);
        }

        point.y = 1;
        return point;
    }
}
