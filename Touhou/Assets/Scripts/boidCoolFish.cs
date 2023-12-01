using UnityEngine;

public class boidCoolFish : MonoBehaviour
{
    public string boidTag = "player";
    public float separationRadius = 17.0f;
    public float alignRadius = 42.5f;
    public float cohesionRadius = 42.5f;
    public float separationWeight = 1.0f;
    public float alignWeight = 1.0f;
    public float cohesionWeight = 1.0f;
    public float speed = 5.0f;
    public float rotationSpeed = 5.0f;

    private Rigidbody2D rb;
    private Camera mainCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        rb.AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized.normalized * speed, ForceMode2D.Impulse);
    }

    void Update()
    {
        Vector2 separation = calculateSeparation();
        Vector2 alignment = calculateAlignement();
        Vector2 cohesion = calculateCohesion();
        Vector2 moveDirection = separation * separationWeight + alignment * alignWeight + cohesion * cohesionWeight;
        rb.AddForce(moveDirection.normalized * speed);
        if (rb.velocity.magnitude > speed)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }
        rotateDirection(moveDirection);
        teleportScreenBorders();
    }

    void rotateDirection(Vector2 targetDirection)
    {
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        float angleDiff = angle - rb.rotation;
        if (Mathf.Abs(angleDiff) > 180)
        {
            angleDiff = Mathf.Sign(angleDiff) * (360 - Mathf.Abs(angleDiff));
        }

        float rotation = Mathf.Clamp(angleDiff, -rotationSpeed, rotationSpeed);
        rb.MoveRotation(rb.rotation + rotation);
    }

    Vector2 calculateSeparation()
    {
        Vector2 separationVector = Vector2.zero;
        Collider2D[] nearBoids = Physics2D.OverlapCircleAll(transform.position, separationRadius);

        foreach (Collider2D boid in nearBoids)
        {
            if (boid.gameObject != gameObject && boid.CompareTag(boidTag))
            {
                separationVector += (Vector2)(transform.position - boid.transform.position);
            }
        }

        return separationVector.normalized;
    }

    Vector2 calculateAlignement()
    {
        Vector2 alignmentVector = Vector2.zero;
        Collider2D[] nearBoids = Physics2D.OverlapCircleAll(transform.position, alignRadius);

        foreach (Collider2D boid in nearBoids)
        {
            if (boid.gameObject != gameObject && boid.CompareTag(boidTag))
            {
                alignmentVector += boid.GetComponent<Rigidbody2D>().velocity;
            }
        }

        return alignmentVector.normalized;
    }

    Vector2 calculateCohesion()
    {
        Vector2 cohesionVector = Vector2.zero;
        Collider2D[] nearBoids = Physics2D.OverlapCircleAll(transform.position, cohesionRadius);

        foreach (Collider2D boid in nearBoids)
        {
            if (boid.gameObject != gameObject && boid.CompareTag(boidTag))
            {
                cohesionVector += (Vector2)boid.transform.position;
            }
        }

        if (nearBoids.Length > 1)
        {
            cohesionVector /= (nearBoids.Length - 1);
            cohesionVector = cohesionVector - (Vector2)transform.position;
        }

        return cohesionVector.normalized;
    }

    void teleportScreenBorders()
    {
       Vector3 position = Camera.main.WorldToViewportPoint(transform.position);

        if (position.x < 0 || position.x > 1 || position.y < 0 || position.y > 1)
        {
            position.x = Mathf.Repeat(position.x, 1.0f);
            position.y = Mathf.Repeat(position.y, 1.0f);
            transform.position = Camera.main.ViewportToWorldPoint(position);
        }
    }
}