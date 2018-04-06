using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	private float offset;

	public GameObject player;
	
	void Start () {
		offset = transform.position.x - player.transform.position.x;
	}
	
	void LateUpdate () {
		transform.position = new Vector3(player.transform.position.x + offset, transform.position.y, transform.position.z);
	}
}
