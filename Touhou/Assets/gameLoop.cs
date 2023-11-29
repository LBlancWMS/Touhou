using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameLoop : MonoBehaviour
{
    [SerializeField] private GameObject ennemiesSpawner;
    public void newSpawnerGoToDirection()
    {
        GameObject spawner = Instantiate(ennemiesSpawner, new Vector3(150, 0, 0), Quaternion.identity);
      //  spawner.GetComponent<ennemiesSpawnerScript>().setStartDirection(new Vector2(startDirection_x, startDirection_y));
    }
}      
