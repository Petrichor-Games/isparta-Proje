using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DusmanSpawner : MonoBehaviour
{
    public GameObject DusmanPrefab;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(DusmanPrefab, transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
