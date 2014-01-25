using UnityEngine;
using System.Collections;

public class ChaserSeeking : MonoBehaviour {

	private bool chaseStarted;

	public GameObject Player;
	// Use this for initialization
	void Start () {
		chaseStarted = false;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 distance = new Vector3 ();
		distance = Player.transform.position - transform.position;
		if (chaseStarted == false) checkForChaseStarting (distance);
		chasePlayer (distance);
	}

	private void checkForChaseStarting(Vector3 distance){
		if (distance.magnitude > 10)
		{
			chaseStarted = true;
		}
	}

	private void chasePlayer(Vector3 distance){
		if (chaseStarted) {
			transform.position += distance.normalized * .1f;
		}
	}
}
