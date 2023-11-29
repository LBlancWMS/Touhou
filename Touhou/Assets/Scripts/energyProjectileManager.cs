using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class energyProjectileManager : MonoBehaviour
{
    public Rigidbody2D rb;
    void Start()
    {
        
    }

    public void onSpawn(Vector2 directionVector)
    {
        rb.AddForce(directionVector * 60f, ForceMode2D.Impulse);
    }
        void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "obstacleMask" || col.gameObject.tag == "ennemiesSpawner")
        {
        Destroy(this.gameObject);
        }

    }

    void OnParticleCollision()
    {
        Destroy(this.gameObject);
    }
}
