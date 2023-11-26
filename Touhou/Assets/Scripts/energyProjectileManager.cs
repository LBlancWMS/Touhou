using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class energyProjectileManager : MonoBehaviour
{
    public Rigidbody2D rb;
    private float lifeTimeStart;
    private float lifeTime = 5f;
    void Start()
    {
        
    }

    public void onSpawn(Vector2 directionVector)
    {
        lifeTimeStart = Time.time;
        rb.AddForce(directionVector * 60f, ForceMode2D.Impulse);
    }

    void Update()
    {
        if(Time.time >= lifeTimeStart + lifeTime)
        {
            Destroy(this.gameObject);
        }
    }

    
void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(this.gameObject);
    }
}
