using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameLoop : MonoBehaviour
{
    private int currentWave = 1;
    [SerializeField] private GameObject ennemiesSpawner;
    private List<spawnersEventsManager> spawnersEvent = new List<spawnersEventsManager>();
    private int nextIndex = 1;
    public void newSpawnerGoToDirection()
    {
       Vector2 spawnPosition = new Vector2(0f,0f);
      switch (currentWave)
      {
        default: spawnPosition = new Vector2(150f,0f);
        break;
        case 1: spawnPosition = new Vector2(150f,0f);
        break;
        case 2: spawnPosition = new Vector2(0f,250f);
        break;
        case 3: spawnPosition = new Vector2(0f,-250f);
        break;      
      }
        GameObject spawner = Instantiate(ennemiesSpawner, new Vector3(spawnPosition.x, spawnPosition.y, 0), Quaternion.identity);
        spawner.GetComponent<ennemiesSpawnerScript>().index = nextIndex;
        nextIndex ++;
      switch (currentWave)
      {
        default: spawner.GetComponent<spawnersEventsManager>().setSpeedMove(25);
        break;
        case 1: spawner.GetComponent<spawnersEventsManager>().setSpeedMove(25);
        break;
        case 2: spawner.GetComponent<spawnersEventsManager>().setSpeedMove(35);
        break;
        case 3: spawner.GetComponent<spawnersEventsManager>().setSpeedMove(50);
        break;    
      }
    }

    public void addSpawnerToList(spawnersEventsManager _spawner)
    {
      spawnersEvent.Add(_spawner);
    }

    public void modifyRateOverTime(int valueToApply)
    {
      foreach (spawnersEventsManager _spawner in GameObject.FindObjectsOfType<spawnersEventsManager>())
      {
        _spawner.calculSpawnRate(valueToApply);
      }
    }

    public void newWave()
    {
      currentWave ++;
    }

    public void incereaseSpeedMove()
    {
      foreach (spawnersEventsManager _spawner in GameObject.FindObjectsOfType<spawnersEventsManager>())
      {
        _spawner.increaseSpeedMove();
      }
    }

        public void destroyAllSpawners()
    {
      foreach (spawnersEventsManager _spawner in GameObject.FindObjectsOfType<spawnersEventsManager>())
      {
        _spawner.onRemove();
      }
    }
}      
