using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public LevelChunkData[] LevelChunkData;
    public LevelChunkData FirstChunk;
    private LevelChunkData previousChunk;
    public Vector3 spawnOrigin;
    private Vector3 spawnPosition;
    public int chunkstospawn = 10;
    //private int chunkCount = 0;
    //public GameObject OyunSonuBolumu;

    void OnEnable()
    {
        TriggerExit.OnChunkExited += PickAndSpawnChunk;
    }

    private void OnDisable()
    {
        TriggerExit.OnChunkExited -= PickAndSpawnChunk;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            PickAndSpawnChunk();
        }
    }

    void Start()
    {
        previousChunk = FirstChunk;

        for (int i = 0; i < chunkstospawn; i++)
        {
            PickAndSpawnChunk();
        }
    }

    LevelChunkData PickNextChunk()
    {
        List<LevelChunkData> allowedChunkList = new List<LevelChunkData>();
        LevelChunkData nextChunk = null;

        spawnPosition = spawnPosition + new Vector3(0f, 0, previousChunk.chunkSize.y);

        nextChunk = LevelChunkData[Random.Range(0, allowedChunkList.Count)];

        return nextChunk;

    }

    void PickAndSpawnChunk()
    {
        // if (chunkstospawn==100)
        // {
        //     
        //     Instantiate(OyunSonuBolumu, spawnPosition + spawnOrigin, OyunSonuBolumu.transform.rotation);
        // }
        
        LevelChunkData chunkToSpawn = PickNextChunk();

        GameObject objectFromChunk = chunkToSpawn.LevelChunks[Random.Range(0, chunkToSpawn.LevelChunks.Length)];
        previousChunk = chunkToSpawn;
        Instantiate(objectFromChunk, spawnPosition + spawnOrigin, objectFromChunk.transform.rotation);
        //chunkstospawn++;

    }

    public void UpdateSpawnOrigin(Vector3 originDelta)
    {
        spawnOrigin = spawnOrigin + originDelta;
    }
}
