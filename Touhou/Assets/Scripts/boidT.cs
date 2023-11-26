using UnityEngine;

public class boidT : MonoBehaviour
{
    private float speed = 80f;
    private float cohesionWeight = 1f;
    private float separationWeight = 1f;
    private float alignmentWeight = 1f;

    private float neighborRadius = 6f;
    private float separationRadius = 2f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "player")
        {
        spawnerT.Instance.ReturnToPool(gameObject);
        }
    }
    void Update()
    {
        applyBoid();
    }

    void applyBoid()
    {
        GameObject[] enemies = spawnerT.Instance.getActiveProjectiles();

        Vector2 averagePosition = Vector2.zero;
        Vector2 averageSeparation = Vector2.zero;
        Vector2 averageAlignment = Vector2.zero;

        int count = 0;

        foreach (GameObject enemy in enemies)
        {
            if (enemy != gameObject)
            {
                float distance = Vector2.Distance(transform.position, enemy.transform.position);


                if (distance < neighborRadius)
                {
                    averagePosition += (Vector2)enemy.transform.position;
                    count++;
                }


                if (distance < separationRadius)
                {
                    Vector2 toEnemy = (Vector2)transform.position - (Vector2)enemy.transform.position;
                    averageSeparation += toEnemy / Mathf.Pow(distance, 2f);
                }


                if (distance < neighborRadius)
                {
                    averageAlignment += enemy.GetComponent<boidT>().rb.velocity;
                }
            }
        }

        if (count > 0)
        {
            averagePosition /= count;
            averageAlignment /= count;
            averagePosition = (averagePosition - (Vector2)transform.position).normalized;

            Vector2 totalForce = (averagePosition * cohesionWeight) + (averageSeparation * separationWeight) + (averageAlignment * alignmentWeight);

            rb.AddForce(totalForce.normalized * speed * Time.deltaTime);
        }
    }
}
