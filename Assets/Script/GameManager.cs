/*@GameManager
 * Keeps track of resources, important objects
 * Accesible by other classes, so they can talk to the objects kept track here
 * Generate Maze With the MazeGenerator Class
 */
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	MazeGenerator mazeGen;
	// Object Management
	public GameObject Player;
	//make GM accessible to other scripts
	private static GameManager thisClass;
	public static GameManager ThisClass {get{return thisClass;}}

	// Use this for initialization
	void Start () {
		thisClass = this;//make the GameManager accessible to other scripts 
		mazeGen = this.GetComponent<MazeGenerator>();
		mazeGen.generateMaze();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
