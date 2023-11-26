
using System.Collections;
using UnityEngine;

public class boidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject projPrefab;
    private int numberToSpawn = 100;

    void Start()
    {
        StartCoroutine(spawnProjCoroutine());
    }

    IEnumerator spawnProjCoroutine()
    {
        while (numberToSpawn > 0)
        {
            spawnProj();
            yield return new WaitForSeconds(5f);
        }
    }

    void spawnProj()
    {
        for (int i = 0; i < 10; i++)
        {
            //numberToSpawn --;
            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-10f, 10f), Random.Range(-5f, 5f), 0f);
            Instantiate(projPrefab, spawnPosition, new Quaternion(0f,0f,0f,0f));
        }
    }
}
