/*@MazeGenerator
 * Make a col by row maze base with grids of 4 walls forming a square, in the form of arrays and booleans
 * Search through the grids one by one, knocking down walls randomly to generate a perfect maze
 * Erect walls using the wall prefab
 * Profit
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MazeGenerator : MonoBehaviour {

	// Controller
	public int colNum = 10;
	public int rowNum = 15;
	public Object wallPrefab;
	// Memory
	private Stack<int> checkedGrids; // Used to contain indexes and check for path making
	private ArrayList indexedGrids; // Used to determine grid locations
	private int wallBreakerIndex; // start breaking the walls at a random grid
	private int visitedCount;


	// Use this for initialization
	void Start () {
 		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Generate Randomly; Always have an escape.
	public void generateMaze() {
		checkedGrids = new Stack<int>();
		indexedGrids = new ArrayList ();
		wallBreakerIndex = (int)Mathf.Floor(Random.Range(0, rowNum*colNum));//sets the starting point of maze making (wall breaking)
		visitedCount = 1;
		// Add maze's grid base 
		for (int r = 0; r < rowNum; r++)
		{
			for( int c = 0; c < colNum; c++)
			{
				Grid tGrid = new Grid();
				tGrid.iRow = r;
				tGrid.iCol = c;
				indexedGrids.Add(tGrid);
			}
		}
		// [TODO] Locate the entrance and the exit

		// Break the walls to generate maze paths
		generatePaths ();
		makeExit ();
		erectWalls ();
	}
	private void generatePaths() {
		List<Grid> neighborGrids = new List<Grid>();
		// While not all grids are visited and made path,
		// visit a random neighboring grids and break walls to make path
		while(visitedCount < rowNum * colNum)
		{
			neighborGrids.Clear();
			// be careful with grids on the edge, for they don't have a neighbor on that side
			if (checkNorthEdge(wallBreakerIndex) == false)
			{
				// Add the unvisited, existing neighbors to neighbotGrids
				if (((Grid)indexedGrids[wallBreakerIndex - colNum]).wallIntact())
				{
					neighborGrids.Add((Grid)indexedGrids[wallBreakerIndex - colNum]);
				}
			}
			if (checkSouthEdge(wallBreakerIndex) == false)
			{
				if (((Grid)indexedGrids[wallBreakerIndex + colNum]).wallIntact())
				{
					neighborGrids.Add((Grid)indexedGrids[wallBreakerIndex + colNum]);
				}
			}
			if (checkWestEdge(wallBreakerIndex) == false)
			{
				if (((Grid)indexedGrids[wallBreakerIndex - 1]).wallIntact())
				{
					neighborGrids.Add((Grid)indexedGrids[wallBreakerIndex - 1]);
				}
			}
			if (checkEastEdge(wallBreakerIndex) == false)
			{
				if (((Grid)indexedGrids[wallBreakerIndex + 1]).wallIntact())
				{
					neighborGrids.Add((Grid)indexedGrids[wallBreakerIndex + 1]);
				}
			}
			// choose one of available neighbors, if there are
			if (neighborGrids.Count > 0) {
				Grid chosenGrid = neighborGrids[(int)Mathf.Floor(Random.Range(0, neighborGrids.Count))];
				//Debug.Log ("***Current: " + wallBreakerIndex);
				//Debug.Log ("***Next: " + indexedGrids.IndexOf(chosenGrid));
				// see this grid's direction to the old grid
				// SWITCH DOESNT WORK FOR VARIABLES SO I HAVE TO USE IF/ELSE #THROWUPALITTLE
				// North
				if (indexedGrids.IndexOf(chosenGrid) == wallBreakerIndex - colNum){
					((Grid)indexedGrids[wallBreakerIndex]).NorthBroken = true;
					((Grid)chosenGrid).SouthBroken = true;
					//Debug.Log("Gone North");
				} 
				// South
				else if (indexedGrids.IndexOf(chosenGrid) == wallBreakerIndex + colNum){
					((Grid)indexedGrids[wallBreakerIndex]).SouthBroken = true;
					((Grid)chosenGrid).NorthBroken = true;
					//Debug.Log("Gone South");
				}
				// West
				else if (indexedGrids.IndexOf(chosenGrid) == wallBreakerIndex - 1){
					((Grid)indexedGrids[wallBreakerIndex]).WestBroken = true;
					((Grid)chosenGrid).EastBroken = true;
					//Debug.Log("Gone West");
				}
				// East
				else if (indexedGrids.IndexOf(chosenGrid) == wallBreakerIndex + 1){
					((Grid)indexedGrids[wallBreakerIndex]).EastBroken = true;
					((Grid)chosenGrid).WestBroken = true;
					//Debug.Log("Gone East");
				}else{
					//Debug.Log ("***Bugged");
				}
				// Record the wallBreaker's path into checked Grids, so we may revisit later
				checkedGrids.Push(wallBreakerIndex);
				// move on
				wallBreakerIndex = indexedGrids.IndexOf(chosenGrid);
				visitedCount ++;
			} else {
				// if no move, go back a step on the wall breaking train
				wallBreakerIndex = checkedGrids.Pop();
			}
		}
	}

	// Let's start from [0], and end at [rowNum*colNum]
	private void makeExit() {
		((Grid)indexedGrids [rowNum * colNum - 1]).EastBroken = true;
	}

	// Due to lazy scripting, North and South is in fact switched, while East and West asts the same.
	private void erectWalls() {
		for (int i=0; i<rowNum * colNum; i++)
		{
			if(!((Grid)indexedGrids[i]).NorthBroken)
			{
				Instantiate(wallPrefab, 
				            new Vector3 (((Grid)indexedGrids[i]).iCol * 10, 0, ((Grid)indexedGrids[i]).iRow * 10), 
				            Quaternion.Euler(0, 180, 0));
			}
			if(!((Grid)indexedGrids[i]).SouthBroken)
			{
				Instantiate(wallPrefab, 
				            new Vector3 (((Grid)indexedGrids[i]).iCol * 10, 0, ((Grid)indexedGrids[i]).iRow * 10 + 10), 
				            Quaternion.identity);
			}
			if(!((Grid)indexedGrids[i]).WestBroken)
			{
				Instantiate(wallPrefab, 
				            new Vector3 (((Grid)indexedGrids[i]).iCol * 10 - 5, 0, ((Grid)indexedGrids[i]).iRow * 10 + 5), 
				            Quaternion.Euler(0, -90, 0));
			}
			if(!((Grid)indexedGrids[i]).EastBroken)
			{
				Instantiate(wallPrefab, 
				            new Vector3 (((Grid)indexedGrids[i]).iCol * 10 + 5, 0, ((Grid)indexedGrids[i]).iRow * 10 + 5), 
				            Quaternion.Euler(0, 90, 0));
			}
		}
	}

	// Utility: Check if the grid is at south, north, west, or east edge of the amazing maize maze
	private bool checkNorthEdge(int gridIndex) {
		if (((Grid)indexedGrids [gridIndex]).iRow == 0)
			return true;
		else
			return false;
	}
	private bool checkSouthEdge(int gridIndex) {
		if (((Grid)indexedGrids [gridIndex]).iRow == rowNum - 1)
			return true;
		else
			return false;
	}
	private bool checkWestEdge(int gridIndex) {
		if (((Grid)indexedGrids [gridIndex]).iCol == 0)
			return true;
		else
			return false;
	}
	private bool checkEastEdge(int gridIndex) {
		if (((Grid)indexedGrids [gridIndex]).iCol == colNum - 1)
			return true;
		else
			return false;
	}
}
