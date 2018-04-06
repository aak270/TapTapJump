using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : RaycastController {

	float distance = 7;
	float startPosition;
	bool changeDirection = false;

	public LayerMask passengersMask;
	Vector3 move;

	public override void Start () {
		base.Start();
		move.y = 1;
		startPosition = transform.position.y;
	}
	
	void Update () {
		UpdateRaycastOrigins();

		if(changeDirection && (transform.position.y - startPosition > distance || transform.position.y < startPosition + 0.5)){
			move.y = -move.y;
			changeDirection = false;
		}else if(transform.position.y - startPosition < distance - 2 && transform.position.y > startPosition + 2){
			changeDirection= true;
		}

		Vector3 velocity = move * Time.deltaTime;

		MovePassengers(velocity);
		transform.Translate(velocity);
	}

	void MovePassengers(Vector3 velocity){
		HashSet<Transform> movedPassengers = new HashSet<Transform>();

		float directionY = Mathf.Sign(velocity.y);

		//vertically moving platform
		if(velocity.y != 0){
			float rayLength = Mathf.Abs (velocity.y) + skinWidth;

			for (int i = 0; i < verticalRayCount; i ++) {
				Vector2 rayOrigin = (directionY == -1)?raycastOrigins.bottomLeft:raycastOrigins.topLeft;
				rayOrigin += Vector2.right * (verticalRaySpacing * i);
				RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, passengersMask);

				if(hit){
					if(!movedPassengers.Contains(hit.transform)){
						movedPassengers.Add(hit.transform);
						float pushX = (directionY == 1)? velocity.x: 0;
						float pushY = velocity.y - (hit.distance - skinWidth) * directionY;

						hit.transform.Translate(new Vector3(pushX, pushY));
					}
				}
			}
		}

		//passenger on a moving horizontaly or downward platform
		if(directionY == -1 || velocity.y == 0 && velocity.x != 0){
			float rayLength = 2 * skinWidth;

			for (int i = 0; i < verticalRayCount; i ++) {
				Vector2 rayOrigin = raycastOrigins.topLeft + Vector2.right * (verticalRaySpacing * i);
				RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, rayLength, passengersMask);

				if(hit){
					if(!movedPassengers.Contains(hit.transform)){
						movedPassengers.Add(hit.transform);
						float pushX =velocity.x;
						float pushY = velocity.y;

						hit.transform.Translate(new Vector3(pushX, pushY));
					}
				}
			}
		}
	}
}
