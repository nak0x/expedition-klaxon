using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidsSpawner : MonoBehaviour
{
    [Header("Refs")]
    public AnchorsController anchors;   // Left/Center/Right
    public Kid kidPrefab;

    [Header("Spawn")]
    public float spawnIntervalMin = 0.8f;
    public float spawnIntervalMax = 1.6f;
    public float spawnZOffset = 0f;     // ajoute un offset le long de l’axe de déplacement si besoin
    public bool spawnAllowed = true;


    private void Start()
    {
        StartCoroutine(SpawnLoop());
        GameController.Instance.OnPauseEvent += DisallowSpawn;
        GameController.Instance.OnResumeEvent += AllowSpawn;
    }

    private void AllowSpawn()
    {
        this.spawnAllowed = true;
    }

    private void DisallowSpawn()
    {
        this.spawnAllowed = false;
    }
    private IEnumerator SpawnLoop()
    {
        while (spawnAllowed)
        {
            yield return new WaitForSeconds(Random.Range(spawnIntervalMin, spawnIntervalMax));
            SpawnOne();
        }
    }

    private void SpawnOne()
    {
        if (anchors == null || kidPrefab == null) return;

        // Choisit une lane aléatoire
        Lane lane = (Lane)Random.Range(0, 3);
        Vector3 pos;

        switch (lane)
        {
            case Lane.Left:
                if (!anchors.leftAnchor) return;
                pos = anchors.leftAnchor.position;
                break;
            case Lane.Center:
                if (!anchors.centerAnchor) return;
                pos = anchors.centerAnchor.position;
                break;
            default:
                if (!anchors.rightAnchor) return;
                pos = anchors.rightAnchor.position;
                break;
        }

        pos += Vector3.back.normalized * spawnZOffset;

        Kid kid = Instantiate(kidPrefab, pos, Quaternion.Euler(0, 180, 0), transform);
        kid.lane = lane;
    }
}