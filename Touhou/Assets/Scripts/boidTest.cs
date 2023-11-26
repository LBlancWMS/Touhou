using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boidTest : MonoBehaviour
{
    float speed;
    Vector2 direction;
    public List<boidTest> boidComportementInScene;
    float moveCenterStrength;
    float localboidComportementDistance;
    float avoidOtherStrength;
    float collisionAvoidCheckDistance;
    float alignWithOthersStrength;
    float alignmentCheckDistance;


 void moveCenter()
 {

	Vector2 positionSum = transform.position;
	int count = 0;

	foreach (boidTest boid in boidComportementInScene)
	{
		float distance = Vector2.Distance(boid.transform.position, transform.position);
		if (distance <= localboidComportementDistance){
			positionSum += (Vector2)boid.transform.position;
			count++;
		}
	}

	if (count == 0)
    {
		return;
	}

	Vector2 positionAverage = positionSum / count;
	positionAverage = positionAverage.normalized;
	Vector2 faceDirection = (positionAverage - (Vector2) transform.position).normalized;

	float deltaTimeStrength = moveCenterStrength * Time.deltaTime;
	direction=direction+deltaTimeStrength*faceDirection/(deltaTimeStrength+1);
	direction = direction.normalized;
}

void avoidOther()
{

	Vector2 faceAwayDirection = Vector2.zero;
	foreach (boidTest boid in boidComportementInScene){
		float distance = Vector2.Distance(boid.transform.position, transform.position);

		if (distance <= collisionAvoidCheckDistance){
			faceAwayDirection =faceAwayDirection+ (Vector2)(transform.position - boid.transform.position);
		}
	}

	faceAwayDirection = faceAwayDirection.normalized;

	direction=direction+avoidOtherStrength*faceAwayDirection/(avoidOtherStrength +1);
	direction = direction.normalized;
}

void alignWithOthers()
{
	Vector2 directionSum = Vector3.zero;
	int count = 0;

	foreach (boidTest boid in boidComportementInScene)
    {
		float distance = Vector2.Distance(boid.transform.position, transform.position);
		if (distance <= localboidComportementDistance){
			directionSum += boid.direction;
			count++;
		}
	}

	Vector2 directionAverage = directionSum / count;
	directionAverage = directionAverage.normalized;
	float deltaTimeStrength = alignWithOthersStrength * Time.deltaTime;
	direction=direction+deltaTimeStrength*directionAverage/(deltaTimeStrength+1);
	direction = direction.normalized;

}
void Update()
{
	alignWithOthers();
	moveCenter();
	avoidOther();
	transform.Translate(direction * (speed * Time.deltaTime));
}
}
