using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatingMovement : MonoBehaviour
{
    public float rotationSpeed = 5f;

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
