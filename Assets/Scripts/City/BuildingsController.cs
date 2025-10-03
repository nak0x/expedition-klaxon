using System.Collections;
using System.Collections.Generic;
using Scriptables;
using UnityEngine;
using City.Chunks;
using Utils;

public class BuildingsController : SingletonMonoBehaviour<BuildingsController>
{
    public GameObject spawnAnchor;
    public ChunkTypeList chunkTypeList;
    
    public int chunkCount = 0;
    public delegate void ChunkEvent(Chunk chunk);
    public ChunkEvent OnChunkCreationRequested;
    
    void Awake()
    {
        this.OnChunkCreationRequested += SpawnNewChunkEvent;
        SpawnChunk();
    }

    void SpawnNewChunkEvent(Chunk chunk)
    {
        SpawnChunk();
    }

    public void SpawnChunk()
    {
        GameObject chunk = GameObject.Instantiate(chunkTypeList.GetRandomChunk().Prefab, spawnAnchor.transform.position, Quaternion.identity, this.transform);
    }

}
