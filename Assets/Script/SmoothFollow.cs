using UnityEngine;
using System.Collections;

/// <summary>
/// Smooth Follow
/// This camera smooths out rotation around the y-axis and height.
/// Horizontal Distance to the target is always fixed.
/// For every of those smoothed values we calculate the wanted value and the current value.
/// Then we smooth it using the Lerp function.
/// Then we apply the smoothed values to the transform's position.
/// This script should be attached to the camera.
/// </summary>
/// 
public class SmoothFollow : MonoBehaviour 
{
	public Transform target;
	public float distance = 5.0f;
	public float height = 2.0f;
	public float positionDamping = 5.0f;
	
	public float scrollMultiplier = 1.0f;
	public float scrollMax = 10.0f;
	public float scrollMin = 0.5f;
	public float scrollSensitivity = 1.0f;
	
	private float originx, originy, originz;
	bool origin = false;
	
	public bool lookAt = true;
	
	// Update is called once per frame
	void LateUpdate ()
	{
		if (!origin) {
			origin = true;
			originx = transform.position.x;
			originy = transform.position.y;
			originz = transform.position.z;
		}
		GameManager gamemanager = GameManager.ThisClass;
		if (gamemanager.win) {
			gamemanager.win = false;
			transform.position += new Vector3(0, .08f, 0);
			transform.LookAt(new Vector3(0, 0, 0));
			return;
		}
		if (gamemanager.died && gamemanager.PersonalityCount > 0) {
			transform.position = new Vector3(originx, originy, originz);
			gamemanager.died = false;
			return;
		}
		this.checkScroll();
		// Early out if we don't have a target
		if (!target)
			return;
		//here's some change to the old format: since it's in space, we cannot rely on y anymore
		//so instead we use the local up vector to create an increment we can add to the wantedPosition
		Vector3 wantedHeight = target.up * height * scrollMultiplier;
		// Set the position of the camera 
		Vector3 wantedPosition = target.position - target.forward * distance * scrollMultiplier;
		// Set the height of the camera
		wantedPosition += wantedHeight;
		//lerp it (transit it smoothly)
		transform.position = Vector3.Lerp(transform.position, wantedPosition, positionDamping* Time.deltaTime);
		
		// look at the target
		if(lookAt){
			transform.LookAt (target,target.transform.up);//the second parameter makes the up follow the object's up
		}
	}
	
	private void checkScroll(){
		scrollMultiplier = Mathf.Clamp( - Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity + scrollMultiplier, 
		                               scrollMin, 
		                               scrollMax);
	}
	
}