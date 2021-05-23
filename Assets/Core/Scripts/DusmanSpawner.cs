using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DusmanSpawner : MonoBehaviour
{
    public GameObject DusmanPrefab;
    
    void Start()
    {
        Instantiate(DusmanPrefab, transform);
    }
    
}
