// using UnityEngine;

// public class boidComportement : MonoBehaviour
// {
//     public float speed = 3f;
//     public float rotationSpeed = 3f;
//     public float separationDistance = 1f;
//     public LayerMask obstacleLayer;

//     private Vector2 fixedDirection;

//     void Start()
//     {
//         setRandomDirection();
//     }

//     void FixedUpdate()
//     {
//         boidLogic();
//     }

//     void boidLogic()
//     {
//         Vector2 separation = Vector2.zero;
//         Collider2D[] obstacles = Physics2D.OverlapCircleAll(transform.position, separationDistance, obstacleLayer);

//         foreach (Collider2D obstacle in obstacles)
//         {
            
//             if (obstacle.gameObject != gameObject)
//             {
//                 Debug.Log("test");
//                 separation += ((Vector2)transform.position - (Vector2)obstacle.transform.position) / separationDistance;
//             }
//         }

//         Vector2 targetDirection = (Vector2)transform.position + separation;
//         if(targetDirection != Vector2.zero)
//         {
//             float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
//             Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
//             transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
//         }

//         transform.Translate(Vector3.up * speed * Time.deltaTime);
//     }

//     void startMove()
//     {
//         transform.Translate(Vector3.up * speed * Time.deltaTime);
//     }

//     void setRandomDirection()
//     {
//         fixedDirection = Random.insideUnitCircle.normalized;
//         transform.up = fixedDirection;
//         startMove();
//     }
// }







using UnityEngine;

public class boidComportement : MonoBehaviour
{
    public float speed = 3f;
    public float rotationSpeed = 3f;
    public float separationDistance = 1f;
    public LayerMask obstacleLayer;

    private Vector2 fixedDirection;

    void Start()
    {
        setRandomDirection();
    }

    void FixedUpdate()
    {
        boidLogic();
    }

    void boidLogic()
    {
        Vector2 separation = Vector2.zero;
        Collider2D[] obstacles = Physics2D.OverlapCircleAll(transform.position, separationDistance, obstacleLayer);

        foreach (Collider2D obstacle in obstacles)
        {
            if (obstacle.gameObject != gameObject)
            {
                Debug.Log("test");
                separation += ((Vector2)transform.position - (Vector2)obstacle.transform.position) / separationDistance;
            }
        }

        Vector2 targetDirection = (Vector2)transform.position + separation.normalized;
        if (targetDirection != Vector2.zero)
{
    float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
    Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
}

        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    void startMove()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    void setRandomDirection()
    {
        fixedDirection = Random.insideUnitCircle.normalized;
        transform.up = fixedDirection;
        startMove();
    }
}
