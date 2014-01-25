using UnityEngine;
using System.Collections;

public class Grid {

	// Default to false
	public bool NorthBroken;
	public bool SouthBroken;
	public bool WestBroken;
	public bool EastBroken;

	public int iRow;
	public int iCol;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	/*
	public void ErectWalls(Object wallPrefab) {
		// add walls according to the broken booleans. whichever is broken doesn't get added
		if (!NorthBroken) {
			Instantiate(wallPrefab, 
             			new Vector3 (iCol * 10, 0, iRow * 10), 
            			Quaternion.identity);
		}
	}
*/
	public bool wallIntact(){
		return !(NorthBroken || SouthBroken || WestBroken || EastBroken);
	}
}
