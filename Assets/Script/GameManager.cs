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
	public GameObject dieBody;
	public bool died = false;
	public Transform trans;
	public GameObject Chaser;
	public float tx, ty, tz;
	public bool win = false;
	// GUI
	public Texture TexTitle;
	public Texture TexSmallTitle;
	public Texture TexCounter;
	public Texture TexInstruction;
	public Texture TexGameover;
	bool gameover = false;
	// Game Variable
	public int PersonalityCount;
	//make GM accessible to other scripts
	private static GameManager thisClass;
	public static GameManager ThisClass {get{return thisClass;}}
	// crappy coding
	private int alphaFadeCounter = 0;
	// Use this for initialization
	void Start () {
		thisClass = this;//make the GameManager accessible to other scripts 
		mazeGen = this.GetComponent<MazeGenerator>();
		mazeGen.generateMaze();
		CameraFade.StartAlphaFade (new Color (.87f, .87f, .87f), false, 6f, 2f);
	}
	
	// Update is called once per frame
	void Update () {
		if (died && PersonalityCount > 0) {
				PersonalityCount --;
				if (PersonalityCount > 0) {
					Instantiate (dieBody, new Vector3(tx, ty, tz), trans.rotation);
				}
				if (PersonalityCount <= 0) {
					trans.Rotate(Vector3.forward * 90);
					trans.Rotate(Vector3.left * 48);
					Instantiate (dieBody, new Vector3(tx, ty - 1.5f, tz), trans.rotation);
					died = true;
					if (gameover == false) gameover = true;
				}
		}
	}

	void OnGUI(){
		if (gameover) {
			GUI.DrawTexture(new Rect(0, 0, 800, 600), TexGameover, ScaleMode.ScaleToFit);
			return;
		}
		if (Time.time < 2)
		{
			CameraFade.SetScreenOverlayColor (new Color (.87f, .87f, .87f));
		}else if (Time.time < 10)
		{
			if (Time.time < 3)
			{
				if (alphaFadeCounter == 0){
					CameraFade.StartAlphaFade (new Color (.87f, .87f, .87f), true, 3f);
					alphaFadeCounter = 1;
				}
			}
			Color oldC = GUI.color;
			// Fade in/out the title
			if (Time.time < 7)
			{
				GUI.color = new Color(oldC.r, oldC.g, oldC.b, (Time.time-2) / 1);
				GUI.DrawTexture(new Rect((Screen.width - 500) / 2, (Screen.height - 110) / 2, 500, 110), TexTitle, ScaleMode.ScaleToFit);
				GUI.color = oldC;
			}else{
				GUI.color = new Color(oldC.r, oldC.g, oldC.b, 8 - Time.time);
				GUI.DrawTexture(new Rect((Screen.width - 500) / 2, (Screen.height - 110) / 2, 500, 110), TexTitle, ScaleMode.ScaleToFit);
				GUI.color = oldC;
			}
			// Fade in/out the small title
			if (Time.time < 9)
			{
				GUI.color = new Color(oldC.r, oldC.g, oldC.b, (Time.time-4) / 1);
				GUI.DrawTexture(new Rect((Screen.width - 537) / 2, (Screen.height - 33) / 2 + 110, 537, 33), TexSmallTitle, ScaleMode.ScaleToFit);
				GUI.color = oldC;
			}else{
				GUI.color = new Color(oldC.r, oldC.g, oldC.b, 10-Time.time);
				GUI.DrawTexture(new Rect((Screen.width - 537) / 2, (Screen.height - 33) / 2 + 110, 537, 33), TexSmallTitle, ScaleMode.ScaleToFit);
				GUI.color = oldC;
			}
		} else {
			GUI.DrawTexture(new Rect(Screen.width - 480, Screen.height - 100, 480, 100), TexInstruction, ScaleMode.ScaleToFit);
			for (int i = 0; i < PersonalityCount; i++)
			{
				GUI.DrawTexture(new Rect(20 + 38 * i, Screen.height - 100 - 20, 38, 100), TexCounter, ScaleMode.ScaleToFit);
			}
		}
	}
}
