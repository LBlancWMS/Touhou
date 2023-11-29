using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nonPlayableCharacter : MonoBehaviour
{
    private Camera cam;
    private Vector2 directionn; 
    public float speed = 75f;
    private GameObject _nonPlayableCharacter;
    private float rotationAngle;

    void Awake()
    {
        cam = Camera.main;
        _nonPlayableCharacter = this.gameObject;
        SetRandomRotation();
    }

    void Update()
    {
        transform.Translate(directionn * speed * Time.deltaTime);
        Vector3 playerScreenPoint = cam.WorldToScreenPoint(transform.position);
    }

   private void screenLimitTeleportation(Vector3 newPosition)
    {
        transform.position = cam.ScreenToWorldPoint(newPosition);
        SetRandomRotation();
    }

    private void SetRandomRotation()
    {
        rotationAngle = Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
    }
}
