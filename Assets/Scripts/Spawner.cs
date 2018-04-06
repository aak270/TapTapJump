using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	float firstPositionX;
	float positionX;
	float distance;
	private float minDistance = 2;
	private float maxDistance = 12;
	private bool willSpawn = false;

	int obstacleType;
	int obstacleSize;

	public GameObject[] obstacle;

	void Update(){
		if(willSpawn){
			positionX = transform.position.x;
			if(positionX - firstPositionX < distance + 0.5 && positionX - firstPositionX > distance - 0.5){
				willSpawn = false;
				Spawn();
			}
		}
	}

	void OnTriggerExit2D(Collider2D other){
		firstPositionX = transform.position.x;
		if(other.tag == "Floating"){
			firstPositionX = transform.position.x + 5;
		}

		distance = Random.Range(minDistance, maxDistance);
		willSpawn = true;

		obstacleSize = obstacleType = Random.Range(0, 5);
		if(obstacleSize == 3 || obstacleSize == 4){
			obstacleSize = 0;
		}
	}

	public void Spawn(){
		Instantiate(obstacle[obstacleType], new Vector3(transform.position.x + 2.5f * (obstacleSize + 1), transform.position.y, transform.position.z), Quaternion.identity);
	}
}
