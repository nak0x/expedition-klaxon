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

    [Header("Kid Defaults")]
    public float kidSpeedCoef = 9f;
    public Vector3 kidMoveDirection = Vector3.back;

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }
    private IEnumerator SpawnLoop()
    {
        while (true)
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

        // Optionnel: décaler sur l’axe avant/arrière
        pos += kidMoveDirection.normalized * spawnZOffset;

        Kid kid = Instantiate(kidPrefab, pos, Quaternion.Euler(0, 180, 0), transform);
        kid.lane = lane;
        kid.speed = kidSpeedCoef * CityController.Instance.CurrentSpeed();
        kid.moveDirection = kidMoveDirection;
    }
}