using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollingBackground : MonoBehaviour 
{
    [SerializeField] private float maxHeight;
    [SerializeField] private float speed;
    [SerializeField] private float resetPosition;


    void FixedUpdate () 
    {
        if(transform.position.y > maxHeight)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, resetPosition, 0);
        }
    }

}