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
		int sx = (int)(transform.position.z / 10);
		int sy = (int)(transform.position.x / 10);
		int tx = (int)(Player.transform.position.z / 10);
		int ty = (int)(Player.transform.position.x / 10);
		if (chaseStarted == false) checkForChaseStarting (distance);
		chasePlayer (sx, sy, tx, ty, distance);
	}

	private void checkForChaseStarting(Vector3 distance){
		if (distance.magnitude > 5)
		{
			chaseStarted = true;
		}
	}

	private void chasePlayer(int sx, int sy, int tx, int ty, Vector3 distance){
		if (!chaseStarted) return;
		GameManager gamemanager = GameManager.ThisClass;
		MazeGenerator mazeGen = gamemanager.mazeGen;
		ArrayList list = new ArrayList ();
		ArrayList listans = new ArrayList ();
		Grid tGrid = new Grid ();
		tGrid.iCol = sx;
		tGrid.iRow = sy;
		list.Add (tGrid);
		int ans = 0;
		int start = 0;
		if (distance.magnitude < 1) return; // die!
		if (sx == tx && sy == ty) transform.position += distance.normalized * .1f;
		int end = 0;
		while (start <= end) {
			tGrid = (Grid)list[start];
			int direction = 0;
			int x1 = tGrid.iCol;
			int y1 = tGrid.iRow;
			if (start == 0) direction = 0; else direction = (int)listans[start];
			//tGrid = (Grid)mazeGen.indexedGrids[x1 * mazeGen.rowNum + y1];
			if (tGrid.NorthBroken) {
				transform.position += new Vector3(0.0f, 0.1f, 0.0f);
				return;
				Grid sGrid = new Grid();
				sGrid.iCol = x1 - 1;
				sGrid.iRow = y1;
				if (direction == 0) direction = 1;
				if (sGrid.iCol == tx && sGrid.iRow == ty) {
					ans = direction;
					break;
				}
				if (!list.Contains(sGrid) && sGrid.iCol>= 0 && sGrid.iCol < mazeGen.colNum && sGrid.iRow >= 0 && sGrid.iRow < mazeGen.rowNum) {
					list.Add(sGrid);
					listans.Add (direction);
					end ++;
				}
			}
			if (tGrid.SouthBroken) {
				transform.position += new Vector3(0.0f, 0.1f, 0.0f);
				return;
				Grid sGrid = new Grid();
				sGrid.iCol = x1 + 1;
				sGrid.iRow = y1;
				if (direction == 0) direction = 2;
				if (sGrid.iCol == tx && sGrid.iRow == ty) {
					ans = direction;
					break;
				}
				if (!list.Contains(sGrid) && sGrid.iCol>= 0 && sGrid.iCol < mazeGen.colNum && sGrid.iRow >= 0 && sGrid.iRow < mazeGen.rowNum) {
					list.Add(sGrid);
					listans.Add (direction);
					end ++;
				}
			}
			if (tGrid.EastBroken) {
				transform.position += new Vector3(0.0f, 0.1f, 0.0f);
				return;
				Grid sGrid = new Grid();
				sGrid.iCol = x1;
				sGrid.iRow = y1 + 1;
				list.Add(sGrid);
				if (sGrid.iCol == tx && sGrid.iRow == ty) {
					ans = direction;
					break;
				}
				if (!list.Contains(sGrid) && sGrid.iCol>= 0 && sGrid.iCol <= mazeGen.colNum && sGrid.iRow >= 0 && sGrid.iRow <= mazeGen.rowNum) {
					list.Add(sGrid);
					listans.Add (direction);
					end ++;
				}
			}
			if (tGrid.WestBroken) {
				transform.position += new Vector3(0.0f, 0.1f, 0.0f);
				return;
				Grid sGrid = new Grid();
				sGrid.iCol = x1;
				sGrid.iRow = y1 - 1;
				list.Add(sGrid);
				if (sGrid.iCol == tx && sGrid.iRow == ty) {
					ans = direction;
					break;
				}
				if (!list.Contains(sGrid) && sGrid.iCol>= 0 && sGrid.iCol < mazeGen.colNum && sGrid.iRow >= 0 && sGrid.iRow < mazeGen.rowNum) {
					list.Add(sGrid);
					listans.Add (direction);
					end ++;
				}
			}
			break;
			start ++;
		}
		if (ans == 1) {
			transform.position += new Vector3(0.0f, 0.0f, -0.1f);}
		else if (ans == 2) {
			transform.position += new Vector3(0.0f, 0f, +0.1f); }
		else if (ans == 3) {
			transform.position += new Vector3(0.1f, 0.0f, 0.0f); }
		else if (ans ==4) {
			transform.position += new Vector3(-0.1f, 0.0f, 0.0f); }
		//else 
		//	transform.position += distance.normalized * .1f;
	}
}
