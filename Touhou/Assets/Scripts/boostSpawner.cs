using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boostSpawner : MonoBehaviour
{
    [SerializeField] private boostPolymorph boostPrefab;
    private float camHeight;
    private float camWidth;
    void Start()
    {
        camHeight = Camera.main.orthographicSize * 2;
        camWidth = camHeight * Camera.main.aspect;
        StartCoroutine(spawnBoostCoroutine());
    }

    IEnumerator spawnBoostCoroutine()
    {
        while (true)
        {
            spawnBoost();
            yield return new WaitForSeconds(Random.Range(3f, 35f));
        }
    }

    void spawnBoost()
    {
        float randomX = Random.Range(-camWidth / 2, camWidth / 2);
        float randomY = Random.Range(-camHeight / 2, camHeight / 2);
        boostPolymorph boost = Instantiate(boostPrefab, new Vector3(randomX, randomY, 0), Quaternion.identity);
    }
}
