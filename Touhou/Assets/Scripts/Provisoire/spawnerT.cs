using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnityEngine;

public class spawnerT : MonoBehaviour
{
    public static spawnerT Instance { get; private set; }

    public GameObject projectilePrefab;
    public int poolSize = 100;

    private List<GameObject> projectilePool = new List<GameObject>();
    private Vector2 currentDirection;
    public float moveSpeed = 50.0f;
    [SerializeField] private Rigidbody2D rb;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        initializePool();
        currentDirection = (Vector2)transform.position + Random.insideUnitCircle * 5f;
       // spawnInitialProjectiles();
        StartCoroutine("startSpawnCoroutine");
    }

    void initializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab, Vector2.zero, Quaternion.identity);
            projectile.SetActive(false);
            projectilePool.Add(projectile);
        }
    }

    // void spawnInitialProjectiles()
    // {
    //     for (int i = 0; i < poolSize; i++)
    //     {
    //         activateRandomProjectile();
    //     }
    // }

     IEnumerator startSpawnCoroutine()
    {
         for (int i = 0; i < poolSize; i++)
         {
            activateRandomProjectile();
            yield return new WaitForSeconds(0.085f);
        }

        StopCoroutine("startSpawnCoroutine");
    }

    void activateRandomProjectile()
    {
        GameObject projectile = projectilePool.Find(p => !p.activeSelf);

        if (projectile != null)
        {
            Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle * 5f;
            projectile.transform.position = spawnPosition;
            projectile.SetActive(true);
        }
    }

    public GameObject[] getActiveProjectiles()
    {
        return projectilePool.Where(projectile => projectile != null && projectile.activeSelf).ToArray();
    }

    public void ReturnToPool(GameObject projectile)
    {
        projectile.SetActive(false);
        Invoke("activateRandomProjectile", 1.0f);
    }

    void FixedUpdate()
    {
        move();
    }

    void move()
    {
        transform.Translate(currentDirection.normalized * moveSpeed * Time.deltaTime);
       // rb.velocity = currentDirection.normalized * moveSpeed;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        randomDirection();
    }

    void randomDirection()
    {
        currentDirection = -currentDirection + (Random.insideUnitCircle * 1f);
        currentDirection.Normalize();
    }
}
