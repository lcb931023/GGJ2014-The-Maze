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
		int sx = (int)((transform.position.z) / 10);
		int sy = (int)((transform.position.x+5) / 10);
		int tx = (int)(Player.transform.position.z / 10);
		int ty = (int)((Player.transform.position.x + 5) / 10);
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
		Debug.Log (sx + "," + sy + "to" + tx + "," + ty);
		ArrayList list = new ArrayList ();
		int []listans = new int[1000];
		Grid tGrid = new Grid ();
		tGrid.iRow = sx;
		tGrid.iCol = sy;
		list.Add (tGrid);
		int ans = 0;
		int start = 0;
		if (distance.magnitude < 1) return; // die!
		if (sx.Equals (tx) && sy.Equals (ty)) {
						transform.position += distance.normalized * .1f;
						return;
				}
		int end = 0;
		while (start <= end) {
			tGrid = (Grid)list[start];
			int direction = 0;
			int x1 = tGrid.iRow;
			int y1 = tGrid.iCol;
			tGrid = (Grid)mazeGen.indexedGrids[x1 * mazeGen.colNum + y1]; //
			if (start == 0) direction = 0; else direction = (int)listans[start];
			if (tGrid.NorthBroken) {
				Grid sGrid = new Grid();
				sGrid.iRow = x1 - 1;
				sGrid.iCol = y1;
				if (start == 0) direction = 1;
				if (((sGrid.iRow).Equals(tx)) && (sGrid.iCol.Equals(ty))) {
					ans = direction;
					//Debug.Log("N" + tx + ty + sGrid.iRow + sGrid.iCol+ "D" + direction);
					break;
				}
				bool flag = true;
				for (int i = 0; i < list.Count; ++i)
					if (((Grid)list[i]).iCol == sGrid.iCol && ((Grid)list[i]).iRow == sGrid.iRow) flag = false;
				if (flag && sGrid.iCol>= 0 && sGrid.iCol < mazeGen.colNum && sGrid.iRow >= 0 && sGrid.iRow < mazeGen.rowNum) {
					list.Add(sGrid);
					end ++;
					listans[end]=direction;
				}
			}
			if (tGrid.SouthBroken) {
				Grid sGrid = new Grid();
				sGrid.iRow = x1 + 1;
				sGrid.iCol = y1;
				if (start == 0) direction = 2;
				if (((sGrid.iRow).Equals(tx)) && (sGrid.iCol.Equals(ty))) {
					ans = direction;
					//Debug.Log("S" + tx + ty + sGrid.iRow + sGrid.iCol+ "D" + direction);
					break;
				}
				bool flag = true;
				for (int i = 0; i < list.Count; ++i)
					if (((Grid)list[i]).iCol == sGrid.iCol && ((Grid)list[i]).iRow == sGrid.iRow) flag = false;
				if (flag && sGrid.iCol>= 0 && sGrid.iCol < mazeGen.colNum && sGrid.iRow >= 0 && sGrid.iRow < mazeGen.rowNum) {
					list.Add(sGrid);
					end ++;
					listans[end]=direction;
				}
			}
			if (tGrid.EastBroken) {
				Grid sGrid = new Grid();
				sGrid.iRow = x1;
				sGrid.iCol = y1 + 1;
				if (start == 0) direction = 3;
				if (((sGrid.iRow).Equals(tx)) && (sGrid.iCol.Equals(ty))) {
					ans = direction;
					//Debug.Log("E" + tx + ty + sGrid.iRow + sGrid.iCol + "D" + direction);
					break;
				}
				bool flag = true;
				for (int i = 0; i < list.Count; ++i)
					if (((Grid)list[i]).iCol == sGrid.iCol && ((Grid)list[i]).iRow == sGrid.iRow) flag = false;
				if (flag && sGrid.iCol>= 0 && sGrid.iCol <= mazeGen.colNum && sGrid.iRow >= 0 && sGrid.iRow <= mazeGen.rowNum) {
					list.Add(sGrid);
					end ++;
					listans[end]=direction;
				}
			}
			if (tGrid.WestBroken) {
				Grid sGrid = new Grid();
				sGrid.iRow = x1;
				sGrid.iCol = y1 - 1;
				if (start == 0) direction = 4;
				if (((sGrid.iRow).Equals(tx)) && (sGrid.iCol.Equals(ty))) {
					ans = direction;
					//Debug.Log("W" + tx + ty + sGrid.iRow + sGrid.iCol+ "D" + direction);
					break;
				}
				bool flag = true;
				for (int i = 0; i < list.Count; ++i)
					if (((Grid)list[i]).iCol == sGrid.iCol && ((Grid)list[i]).iRow == sGrid.iRow) flag = false;
				if (flag && sGrid.iCol>= 0 && sGrid.iCol < mazeGen.colNum && sGrid.iRow >= 0 && sGrid.iRow < mazeGen.rowNum) {
					list.Add(sGrid);
					end ++;
					listans[end]=direction;
				}
			}
			start ++;
		}
		Debug.Log ("ans:"+ans);
		if (ans == 1) {
			transform.position += new Vector3(0.0f, 0.0f, -0.1f);return;}
		else if (ans == 2) {
			transform.position += new Vector3(0.0f, 0.0f, +0.1f);return;}
		else if (ans == 3) {
			transform.position += new Vector3(0.1f, 0.0f, 0.0f);return;}
		else if (ans ==4) {
			transform.position += new Vector3(-0.1f, 0.0f, 0.0f);return;}
		/*else 
			transform.position += distance.normalized * .1f;*/
	}
}
