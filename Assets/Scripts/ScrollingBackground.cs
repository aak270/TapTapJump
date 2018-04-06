using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {

	public bool scrolling, parallax;
	public float backgroundSize;

	private Transform cameraTransform;
	private Transform[] layers;
	private float viewZone = 10;
	private int leftIndex;
	private int rightIndex;

	public float parallaxSpeed;
	private float lastCameraX;

	private void Start(){
		cameraTransform = Camera.main.transform;
		lastCameraX = cameraTransform.position.x;
		layers = new Transform[transform.childCount];
		for(int i = 0; i < transform.childCount; i++){
			layers[i] = transform.GetChild(i);
		}

		leftIndex = 0;
		rightIndex = layers.Length - 1;
	}

	public void Update(){
		if(parallax){
			float deltaX = cameraTransform.position.x - lastCameraX;
			transform.position += Vector3.right * (deltaX * parallaxSpeed);
		}
	
		lastCameraX = cameraTransform.position.x;

		if(scrolling){
			if(cameraTransform.position.x < (layers[leftIndex].transform.position.x + viewZone)){
				ScrollLeft();
			}
			if(cameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone)){
				ScrollRight();
			}
		}
	}

	private void ScrollLeft(){
		layers[rightIndex].position = new Vector3(layers[leftIndex].position.x - backgroundSize, layers[leftIndex].position.y);
		leftIndex = rightIndex;
		rightIndex--;
		if(rightIndex < 0){
			rightIndex = layers.Length - 1;
		}
	}
 
	private void ScrollRight(){
		layers[leftIndex].position = new Vector3(layers[rightIndex].position.x + backgroundSize,layers[rightIndex].position.y);
		rightIndex = leftIndex;
		leftIndex++;
		if(leftIndex == layers.Length){
			leftIndex = 0;
		}
	}
}
