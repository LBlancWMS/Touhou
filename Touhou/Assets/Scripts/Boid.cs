using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotationSpeed = 2.0f;
    public float neighborRadius = 5.0f;
    public float separationRadius = 2.0f;
    public float separationWeight = 2.0f;
    public float spiralForce = 2.0f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startVelocity();
    }

    void Update()
    {
        applyBoid();
    }

    void applyBoid()
    {
        List<Transform> neighbors = getNeighbors();

        Vector2 _alignment = alignment(neighbors);
        Vector2 _cohesion = cohesion(neighbors);
        Vector2 _separation = separation(neighbors);
        Vector2 _spiral = spiral();

        Vector2 combinedBehavior = _alignment + _cohesion + _separation + _spiral;

        rb.velocity = combinedBehavior * speed * Time.deltaTime;
        rotateDirection();
    }

    void startVelocity()
    {
        rb.velocity = Random.insideUnitCircle.normalized * speed;
    }

    List<Transform> getNeighbors()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, neighborRadius);
        List<Transform> neighbors = new List<Transform>();

        foreach (Collider2D collider in colliders)
        {
            if (collider.transform != transform && collider.CompareTag("boidFriendly"))
            {
                neighbors.Add(collider.transform);
            }
        }

        return neighbors;
    }

        void OnCollisionEnter2D(Collision2D col)
    {
        //if(col.gameObject.tag != "boidFriendly")
        //{
        spawnerT.Instance.ReturnToPool(gameObject);
        //}
    }

    Vector2 alignment(List<Transform> neighbors)
    {
        Vector2 averageDirection = Vector2.zero;

        foreach (Transform neighbor in neighbors)
        {
            averageDirection += (Vector2)neighbor.up;
        }

        averageDirection /= neighbors.Count;

        return (averageDirection - (Vector2)transform.up).normalized;
    }

    Vector2 cohesion(List<Transform> neighbors)
    {
        Vector2 averagePosition = Vector2.zero;

        foreach (Transform neighbor in neighbors)
        {
            averagePosition += (Vector2)neighbor.position;
        }

        averagePosition /= neighbors.Count;

        return (averagePosition - (Vector2)transform.position).normalized;
    }

    Vector2 separation(List<Transform> neighbors)
    {
        Vector2 separationVector = Vector2.zero;

        foreach (Transform neighbor in neighbors)
        {
            Vector2 toNeighbor = (Vector2)transform.position - (Vector2)neighbor.position;
            float distance = toNeighbor.magnitude;

            if (distance < separationRadius)
            {
                separationVector += toNeighbor / (distance * distance);
            }
        }

        return separationVector.normalized * separationWeight;
    }

    Vector2 spiral()
    {
        return transform.position.normalized * spiralForce;
    }

    void rotateDirection()
    {
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), rotationSpeed * Time.deltaTime);
    }
}
