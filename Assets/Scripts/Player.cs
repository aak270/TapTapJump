using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour {

	public static bool playable = true;

	int state = 0;

	private bool firstPressed = false;
	float keyDownTime = float.MinValue;

	float playerMove;
	float moveVelocity;

	float gravity = -4;
	float jumpVelocity;
	float jumpIncrement = 0.5f;
	const float maxJumpVelocity = 8;
	Vector3 velocity;

	Animator anim;
	Controller2D controller;

	public AudioSource jumpSound;
	public AudioSource landSound;

	void Start () {
		controller = GetComponent<Controller2D>();
		anim = GetComponent<Animator>();
	}

	void Update(){
		if(state != 4 && state != 5 && state > 1 && controller.collisions.below){
			anim.SetInteger("State", 0);
			state = 0;
		}
		if(state == 4 && controller.collisions.below){
			anim.SetInteger("State", 5);
			state = 5;

			landSound.Play();
		}
		if(state == 2 && transform.position.y > 3){
			anim.SetInteger("State", 3);
			state = 3;
		}
		if(state == 3 && velocity.y < 0){
			anim.SetInteger("State", 4);
			state = 4;
		}

		if(controller.collisions.above || controller.collisions.below){
			velocity.y = 0;
			playerMove = 0;
		}
		
		if(Time.timeScale == 1 && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && controller.collisions.below && !firstPressed){
			jumpVelocity = 0;
			firstPressed = true;
			keyDownTime = Time.time;

			anim.SetInteger("State", 1);
			state = 1;
		}

		if(((Time.time - keyDownTime) < 0.18f) && (jumpVelocity < maxJumpVelocity)){
			if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended){
				jumpVelocity += jumpIncrement;
				keyDownTime = Time.time;
			}
		}else if(firstPressed){
			firstPressed = false;
			velocity.y = jumpVelocity;

			playerMove = 1;
			float jumpTime = 1.2f * jumpVelocity / Mathf.Abs(gravity);
			float distance = Mathf.Pow(jumpVelocity, 2) / (2 * Mathf.Abs(gravity));
			moveVelocity = distance / jumpTime;

			anim.SetInteger("State", 2);
			state = 2;

			jumpSound.Play();
		}

		velocity.x = playerMove * moveVelocity;
		velocity.y += gravity * Time.deltaTime;
		controller.Move(velocity * Time.deltaTime);
	}
}
