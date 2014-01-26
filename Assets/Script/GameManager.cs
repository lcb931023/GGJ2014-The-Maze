/*@GameManager
 * Keeps track of resources, important objects
 * Accesible by other classes, so they can talk to the objects kept track here
 * Generate Maze With the MazeGenerator Class
 */
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public MazeGenerator mazeGen;
	// Object Management
	public GameObject thePlayer;
	// GUI
	public Texture TexTitle;
	public Texture TexSmallTitle;
	public Texture TexCounter;
	// Game Variable
	public int PersonalityCount;
	//make GM accessible to other scripts
	private static GameManager thisClass;
	public static GameManager ThisClass {get{return thisClass;}}
	// crappy coding
	private bool alphaFadeStarted = false;
	// Use this for initialization
	void Start () {
		thisClass = this;//make the GameManager accessible to other scripts 
		mazeGen = this.GetComponent<MazeGenerator>();
		mazeGen.generateMaze();
		CameraFade.StartAlphaFade (new Color (.87f, .87f, .87f), false, 6f, 2f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI(){
		if (Time.time < 2)
		{
			CameraFade.SetScreenOverlayColor (new Color (0f, 0f, 0f));
		}else if (Time.time < 8)
		{
			if (alphaFadeStarted == false){
				CameraFade.StartAlphaFade (new Color (.87f, .87f, .87f), false, 3f);
				alphaFadeStarted = true;
			}
			Color oldC = GUI.color;
			GUI.color = new Color(oldC.r, oldC.g, oldC.b, (Time.time-2) / 3);
			GUI.DrawTexture(new Rect((Screen.width - 500) / 2, (Screen.height - 110) / 2, 500, 110), TexTitle, ScaleMode.ScaleToFit);
			GUI.color = oldC;
		}
	}
}
