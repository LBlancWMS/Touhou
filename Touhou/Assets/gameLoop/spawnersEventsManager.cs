using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnersEventsManager : MonoBehaviour
{
    private int lastEmissionValue;
    private bool canBeRemoved = false;
    private ParticleSystem.EmissionModule _emissionModule;
    private float speedIndex = 10;
    public void calculSpawnRate(int amountToModify)
    {
        lastEmissionValue += amountToModify;
        GameObject.Find("Timeline_gameLoop").GetComponent<gameLoop>().addSpawnerToList(this);
         _emissionModule = transform.GetChild(0).GetComponent<ParticleSystem>().emission;
       _emissionModule.rateOverTime =  lastEmissionValue;
    }


    void Awake()
    {
        GameObject.Find("Timeline_gameLoop").GetComponent<gameLoop>().addSpawnerToList(this);
         _emissionModule = transform.GetChild(0).GetComponent<ParticleSystem>().emission;
         //Invoke("onRemove", 60f);
    }

    public void onRemove()
    {
        canBeRemoved = true;
    }

    public void setSpeedMove(float speed)
    {
        ParticleSystem.MainModule _mainModule = transform.GetChild(0).GetComponent<ParticleSystem>().main;
        _mainModule.startSpeed = speed;
    }

    public void increaseSpeedMove()
    {
        speedIndex += 10f;
        ParticleSystem.MainModule _mainModule = transform.GetChild(0).GetComponent<ParticleSystem>().main;
        _mainModule.startSpeed = speedIndex;
    }

    void Update()
    {
        if(canBeRemoved)
        {
            if(this.gameObject.transform.localScale.x > 0.01f)
            {
                this.gameObject.transform.localScale -= new Vector3(0.005f, 0.005f, 0.005f);
            }
            else
            {
                this.enabled = false;
                _emissionModule.rateOverTime = 0;
                Invoke("onDestroyInvoke", 14f);
                //Destroy(this.gameObject);
            }
        }
    }

    private void onDestroyInvoke()
    {
        Destroy(this.gameObject);
    }
}
