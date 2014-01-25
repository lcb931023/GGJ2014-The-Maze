/*     TODO
	Figure out the difference between different rotations, and how to do it locally
*/
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]


public class Steering : MonoBehaviour {
	
	public float moveSpeedMax;
	public float acceleration;
	private float moveSpeed;
	private Vector3 moveDirection;
	private CharacterController characterController;
	
	
	// The rotation factor, this will control the speed we rotate at.
	public float rotationSensitvity = 500.0f;
	
	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;//Hide the mouse
		moveDirection = transform.forward;
		moveSpeed = 0;
		characterController = gameObject.GetComponent<CharacterController>();
		
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("w"))
		{
			moveSpeed += acceleration;
			if (moveSpeed > moveSpeedMax) moveSpeed = moveSpeedMax;
		} else {
			moveSpeed -= acceleration * 2;
			if (moveSpeed < 0) moveSpeed = 0;
		}

		moveDirection = transform.forward * moveSpeed;
		
		steerWithMouse();
		
		characterController.Move(moveDirection * Time.deltaTime);//makes the head move forward
		
	}
	
	//calculate mouse rotation on each timestamp
	void steerWithMouse(){
		//the mouse x will apply to rotation around y axis, while 
		//the mouse y will apply to rotation around x axis
		//z is buying soysauce, so he has no business here
		
		Vector3 mouseChange 
			= new Vector3(
				0.0f,
				Input.GetAxis("Mouse X") * Time.deltaTime * rotationSensitvity,
				0.0f
				);

		//lad and gents, take note, this is how you steer in space. 
		//In order to rotate to the right rotation axis reference, we specitfy using Space.self
		transform.Rotate(mouseChange, Space.Self);
		
	}

}
