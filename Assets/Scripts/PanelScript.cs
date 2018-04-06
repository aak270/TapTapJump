using UnityEngine;

public class PanelScript : MonoBehaviour {

	void Update () {
		if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended){
			gameObject.SetActive(false);
		}
	}
}
