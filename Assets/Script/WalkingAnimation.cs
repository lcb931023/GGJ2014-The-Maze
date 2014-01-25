using UnityEngine;
using System.Collections;

public class WalkingAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		animation.CrossFade("idle", 0.25f);  
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("w")) {
			animation.CrossFade ("walk", 0.25f);
		} else {
			animation.CrossFade("idle", 0.25f);
		}
	}
}
