using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomDirection : MonoBehaviour
{
        private Vector2 currentDirection;

    void FixedUpdate()
    {
        move();
    }
    void Start()
    {
        currentDirection = (Vector2)transform.position + Random.insideUnitCircle * 5f;
    }
    void move()
    {
        transform.Translate(currentDirection.normalized * 25f * Time.deltaTime);
       // rb.velocity = currentDirection.normalized * moveSpeed;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        randomDirectionFunc();
    }

    void randomDirectionFunc()
    {
        currentDirection = -currentDirection + (Random.insideUnitCircle * 1f);
        currentDirection.Normalize();
    }
}
