using UnityEngine;
using System.Collections;

public class ChaserSeeking : MonoBehaviour {

	private bool chaseStarted;

	public GameObject Player;
	public float ChaseSpeed;
	// Use this for initialization
	private float playerx, playery, playerz, chaserx, chasery, chaserz;
	
	void Start () {
		chaseStarted = false;
		playerx = Player.transform.position.x;
		playery = Player.transform.position.y;
		playerz = Player.transform.position.z;
		chaserx = transform.position.x;
		chasery = transform.position.y;
		chaserz = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 distance = new Vector3 ();
		distance = Player.transform.position - transform.position;
		int sx = (int)((transform.position.z) / 10);
		int sy = (int)((transform.position.x + 5) / 10);
		int tx = (int)(Player.transform.position.z / 10);
		int ty = (int)((Player.transform.position.x + 5) / 10);
		if (chaseStarted == false) checkForChaseStarting (distance);
		chasePlayer (sx, sy, tx, ty, distance);
	}

	private void checkForChaseStarting(Vector3 distance){
		if (distance.magnitude > 10)
		{
			chaseStarted = true;
		}
	}

	private void chasePlayer(int sx, int sy, int tx, int ty, Vector3 distance){
		if (!chaseStarted) return;
		GameManager gamemanager = GameManager.ThisClass;
		MazeGenerator mazeGen = gamemanager.mazeGen;
		ArrayList list = new ArrayList ();
		int []listans = new int[1000];
		Grid tGrid = new Grid ();
		tGrid.iRow = sx;
		tGrid.iCol = sy;
		list.Add (tGrid);
		int ans = 0;
		int start = 0;
		if (sx.Equals (tx) && sy.Equals (ty)) {
			if (distance.magnitude < 2) {
				// DIE
				//diebody db = new diebody();
				//Transform trans = (Transform)Instantiate(db, transform, Quaternion.identity);
				float x = Player.transform.position.x;
				gamemanager.tx = x;
				gamemanager.ty = Player.transform.position.y;
				gamemanager.tz = Player.transform.position.z;
				gamemanager.trans = Player.transform;
				gamemanager.died = true;
				Player.transform.position = new Vector3(playerx, playery, playerz);
				transform.position = new Vector3 (chaserx, chasery, chaserz);
				chaseStarted = false;
				//diebody db = trans.GetComponent<diebody>();
				//db.transform.position = trans.position;
				//db.transform.rotation = trans.rotation;
				return;
			}
			transform.position += distance.normalized * .5f;
			return;
		}
		int end = 0;
		while (start <= end) {
			tGrid = (Grid)list[start];
			int direction = 0;
			int x1 = tGrid.iRow;
			int y1 = tGrid.iCol;
			if (x1 < 0 || x1 >= mazeGen.rowNum || y1 < 0 || y1 >= mazeGen.colNum) {
				start++;
				continue;
			}
			tGrid = (Grid)mazeGen.indexedGrids[x1 * mazeGen.colNum + y1]; 
			if (start == 0) direction = 0; else direction = (int)listans[start];
			if (tGrid.NorthBroken) {
				Grid sGrid = new Grid();
				sGrid.iRow = x1 - 1;
				sGrid.iCol = y1;
				if (start == 0) direction = 1;
				if (((sGrid.iRow).Equals(tx)) && (sGrid.iCol.Equals(ty))) {
					ans = direction;
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
		if (ans == 1) {
			transform.position += new Vector3(0.0f, 0.0f, -1.0f) * ChaseSpeed;return;}
		else if (ans == 2) {
			transform.position += new Vector3(0.0f, 0.0f, 1.0f) * ChaseSpeed;return;}
		else if (ans == 3) {
			transform.position += new Vector3(1.0f, 0.0f, 0.0f) * ChaseSpeed;return;}
		else if (ans ==4) {
			transform.position += new Vector3(-1.0f, 0.0f, 0.0f) * ChaseSpeed;return;}
		else 
			transform.position += distance.normalized * ChaseSpeed;
	}
}
