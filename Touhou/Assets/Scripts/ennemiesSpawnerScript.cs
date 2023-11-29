using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class ennemiesSpawnerScript : MonoBehaviour
{
    private Vector2 startDirection;
    private Rigidbody2D rb;
    private float speed = 10f;
    private bool canMove = false;
    private int index;

    void Awake()
    {
        GetComponent<PolygonCollider2D>().enabled = false;
        rb = GetComponent<Rigidbody2D>();
        setStartDirection();
    }

    public void setStartDirection()
    {
        index = GameObject.FindGameObjectsWithTag("ennemiesSpawner").Length;
        switch (index)
        {
            default: startDirection = new Vector2(99, 0); GetComponent<rotatingMovement>().rotationSpeed = 0f; transform.localScale *= 1f;
            break;
            case 1: startDirection = new Vector2(99, 0); GetComponent<rotatingMovement>().rotationSpeed = 0f; transform.localScale *= 1f;
            break;
            case 2: startDirection = new Vector2(90, 75); GetComponent<rotatingMovement>().rotationSpeed = 6f; transform.localScale *= 0.75f;
            break;
            case 3: startDirection = new Vector2(90, -75); GetComponent<rotatingMovement>().rotationSpeed = -6f; transform.localScale *= 0.75f;
            break;
        }

        canMove = true;
    }
    void Update()
    {
        if(canMove)
        {
            if(Mathf.Round(transform.position.x) != Mathf.Round(startDirection.x))
            {
                rb.MovePosition(rb.position - startDirection * speed * Time.deltaTime);
            }
            else
            {
                GetComponent<PolygonCollider2D>().enabled = true;
                if(index > 1)
                {
                    ParticleSystem.ShapeModule shapeModule = transform.GetChild(0).GetComponent<ParticleSystem>().shape;
                    shapeModule.shapeType = ParticleSystemShapeType.Cone;
                }
                this.enabled = false;
            }
        }
    }
}
