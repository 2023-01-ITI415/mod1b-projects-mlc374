using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode{
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour
{
    static private MissionDemolition S; //a private singleton

    [Header("Inscribed")]
    public Text                     uitLevel; //the UIText_Level Text
    public Text                     uitShots; //the UIText_Shots Text
    public Vector3                  castlePos; //the place to put castles
    public GameObject[]             castles;  //an array of the castcles

    [Header("Dynamic")]
    public int                      level;      //the current level
    public int                      levelMax;   //the number of levels
    public int                      shotsTaken;
    public GameObject               castle;     //the current castle
    public GameMode                 mode = GameMode.idle;
    public string                   showing = "Show Slingshot"; //FollowCam mode

    // Start is called before the first frame update
    void Start()
    {
        S = this; //define the singleton

        level = 0;
        shotsTaken = 0;
        levelMax = castles.Length;
        StartLevel();
    }

    void StartLevel(){
        //get rid of the old castle if one exists
        if (castle != null) {
            Destroy (castle);
        }
        
        //destroy old projectiles if they exist (the method is not yet written)
        Projectile.DESTROY_PROJECTILES(); //this will be underlined in red

        //Instantiate the new castle
        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;

        //reset the goal
        Goal.goalMet = false;

        UpdateGUI();

        mode = GameMode.playing;

        //zoom out to show both
        FollowCam.SWITCH_VIEW(FollowCam.eView.both);
    }

    void UpdateGUI(){
        //show the data in the guitexts
        uitLevel.text = "Level: "+(level+1)+" of "+levelMax;
        uitShots.text = "Shots Taken: "+shotsTaken;
    }

    // Update is called once per frame
    void Update()
    {
     UpdateGUI();

     //check for level end
     if ((mode == GameMode.playing)&& Goal.goalMet){
        //change mode to stop checking for level end
        mode = GameMode.levelEnd;
        //zoom out to show both
        FollowCam.SWITCH_VIEW(FollowCam.eView.both);

        //start the next level in 2 seconds
        Invoke("NextLevel", 2f);
     }   
    }

    void NextLevel(){
        level++;
        if (level == levelMax){
            level = 0;
            shotsTaken = 0;
        }
        StartLevel();
    }

    //static method that allows code anywhere to increment shotsTaken
    static public void SHOT_FIRED() {
        S.shotsTaken++;
    }

    //static method that allows code anywhere to get a reference to S.castle
    static public GameObject GET_CASTLE() {
        return S.castle;
    }
}
