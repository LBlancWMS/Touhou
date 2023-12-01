using UnityEngine;
using System.Collections;

public class horizontalScroller : MonoBehaviour
{
	public float scrollSpeed;
	public float tileWidth;
	private float newPos;
	public int index;
  
	private Vector3 startPosition;

	void Awake()
	{
		startPosition = transform.position;	
		switch (index)
		{
			case 1: scrollSpeed = 11f;
			break;
			case 2: scrollSpeed = 5.5f;
			break;
			case 3: scrollSpeed = 22.5f;
			break;
		}
	}


	void Update()
	{
		newPos = Mathf.Repeat(Time.time * (scrollSpeed + 0.65f * Time.time), tileWidth);
		transform.position = startPosition + Vector3.left * newPos;
	}
}