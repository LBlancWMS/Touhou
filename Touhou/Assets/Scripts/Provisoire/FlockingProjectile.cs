using UnityEngine;

public class FlockingProjectile : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 5f;
    public float neighborRadius = 2f;
    public float separationWeight = 1f;
    public float cohesionWeight = 1f;
    public float attractionWeight = 1f;
    private Vector2 separation;
    private Vector2 alignment;
    private Vector2 cohesion;
    private Vector2 toPlayer;
    private Vector2 toNeighbor;
    private Vector2 boidDirection;
    private GameObject player;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("player");
    }

    void Update()
    {
        calculBoid();
    }

void OnTriggerEnter2D(Collider2D other)
{
    spawnerT.Instance.ReturnToPool(gameObject);
}

    void calculBoid()
    {
        separation = Vector2.zero;
        alignment = Vector2.zero;
        cohesion = Vector2.zero;

        Collider2D[] neighbors = new Collider2D[50];
        int numNeighbors = Physics2D.OverlapCircleNonAlloc(transform.position, neighborRadius, neighbors);
        for (int i = 0; i < numNeighbors; i++)
            {
                Collider2D neighbor = neighbors[i];

                if (neighbor.gameObject != gameObject && neighbor.tag == "boidFriendly")
                {
                    toPlayer = player.transform.position - transform.position;
                    toNeighbor = neighbor.transform.position - transform.position;
                    float distance = toNeighbor.magnitude;
                        separation -= toNeighbor.normalized / distance;
                    // }

                // alignment += (Vector2)neighbor.GetComponent<FlockingProjectile>().transform.up;
                    cohesion += toNeighbor;
                }

        boidDirection = (separationWeight * separation.normalized + cohesionWeight * cohesion.normalized + attractionWeight * toPlayer.normalized).normalized;
        
        if(boidDirection == Vector2.zero)
        {
            boidDirection = (player.transform.position - transform.position).normalized;
        }
            Move();
        }
        
    }

    void Move()
    {
        transform.Translate(boidDirection * speed * Time.deltaTime);
    }
}
