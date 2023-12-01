using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ennemiesSpawnerScript : MonoBehaviour
{
    private Vector2 startDirection;
    private Rigidbody2D rb;
    private float speed = 0.05f;
    private bool canMove = false;
    public int index;

    void Awake()
    {
        transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
        GetComponent<CircleCollider2D>().enabled = false;
        rb = GetComponent<Rigidbody2D>();
        
    }

    void Start()
    {
        setStartDirection();
    }

    public void setStartDirection()
    {
       //index = GameObject.FindGameObjectsWithTag("ennemiesSpawner").Length;
        ParticleSystem.MainModule _mainModule = transform.GetChild(0).GetComponent<ParticleSystem>().main;
        switch (index)
        {
            default: startDirection = new Vector2(99, 0); GetComponent<rotatingMovement>().rotationSpeed = 0f; transform.localScale *= 1f;
            break;
            case 1: startDirection = new Vector2(99, 0); GetComponent<rotatingMovement>().rotationSpeed = 0f; transform.localScale *= 1f;
            break;
            case 2: startDirection = new Vector2(90, 45); GetComponent<rotatingMovement>().rotationSpeed = 6f; transform.localScale *= 0.75f;
            break;
            case 3: startDirection = new Vector2(90, -45); GetComponent<rotatingMovement>().rotationSpeed = -6f; transform.localScale *= 0.75f;
            break;

            case 4: startDirection = new Vector2(0, 0); GetComponent<rotatingMovement>().rotationSpeed = 0f; transform.localScale *= 1f; 
            break;
            case 5: startDirection = new Vector2(95, 40); GetComponent<rotatingMovement>().rotationSpeed = 6f; transform.localScale *= 0.35f; 
            _mainModule.startSize = 2;
            break;
            case 6: startDirection = new Vector2(-95, 40); GetComponent<rotatingMovement>().rotationSpeed = -6f; transform.localScale *= 0.35f;
            _mainModule.startSize = 2;
            break;


            case 7: startDirection = new Vector2(0, 0); GetComponent<rotatingMovement>().rotationSpeed = 0f; transform.localScale *= 0.5f; 
            break;
            case 8: startDirection = new Vector2(95, -40); GetComponent<rotatingMovement>().rotationSpeed = 6f; transform.localScale *= 0.5f; 
            _mainModule.startSize = 3;
            break;
            case 9: startDirection = new Vector2(-95, -40); GetComponent<rotatingMovement>().rotationSpeed = -6f; transform.localScale *= 0.5f;
            _mainModule.startSize = 3;
            break;
        }

        canMove = true;
    }
    void Update()
    {
        if(canMove)
        {
            if(Mathf.Abs(transform.position.x - startDirection.x) > 3f || Mathf.Abs(transform.position.y - startDirection.y) > 3f)
            {
                transform.position = Vector3.MoveTowards(transform.position, startDirection, speed);
               // rb.MovePosition(rb.position - startDirection * speed * Time.fixedDeltaTime);
            }
            if(Mathf.Abs(transform.position.x - startDirection.x) <= 3f && Mathf.Abs(transform.position.y - startDirection.y) <= 3f)
            {
               onDirection();
            }
        }
    }

    private void onDirection()
    {
                ParticleSystem.ShapeModule _shapeModule = transform.GetChild(0).GetComponent<ParticleSystem>().shape;
                GetComponent<CircleCollider2D>().enabled = true;
                transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                switch (index)
                {
                    default:
                    break;
                    case 1:
                    _shapeModule.shapeType = ParticleSystemShapeType.Cone;
                    GetComponent<spawnersEventsManager>().calculSpawnRate(30);
                    break;
                    case 2:
                    _shapeModule.shapeType = ParticleSystemShapeType.Cone;
                    GetComponent<spawnersEventsManager>().calculSpawnRate(30);
                    break;
                    case 3: GetComponent<spawnersEventsManager>().calculSpawnRate(30);
                    break;

                    case 4:
                    GetComponent<spawnersEventsManager>().calculSpawnRate(200);
                    _shapeModule.shapeType = ParticleSystemShapeType.Donut;
                    break;
                    case 5:
                    GetComponent<randomDirection>().enabled = true;
                    _shapeModule.shapeType = ParticleSystemShapeType.Donut;
                    break;
                    case 6:
                    GetComponent<randomDirection>().enabled = true;
                    _shapeModule.shapeType = ParticleSystemShapeType.Donut;
                    break;

                    case 7:
                    GetComponent<spawnersEventsManager>().calculSpawnRate(100);
                    _shapeModule.shapeType = ParticleSystemShapeType.Donut;
                    break;
                    case 8:
                    GetComponent<spawnersEventsManager>().calculSpawnRate(100);
                    GetComponent<randomDirection>().enabled = true;
                    _shapeModule.shapeType = ParticleSystemShapeType.Donut;
                    break;
                    case 9:
                    GetComponent<spawnersEventsManager>().calculSpawnRate(100);
                    GetComponent<randomDirection>().enabled = true;
                    _shapeModule.shapeType = ParticleSystemShapeType.Donut;
                    break;
                }
                this.enabled = false;
    }
}
