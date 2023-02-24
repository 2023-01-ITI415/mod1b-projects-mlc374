using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ProGameMode
{
    idle,
    playing,
    levelEnd
}

public class Prototype : MonoBehaviour
{
    static private Prototype S; //a private singleton

    [Header("Inscribed")]
    public Text uitLevel; //the UIText_Level Text
    public Text uitWin;
    public Vector3 mapPos; //the place to put the map
    public GameObject[] maps; //an array of the maps

    [Header("Dynamic")]
    public int level; //the current level
    public int levelMax; //the number of levels total
    public GameObject map; //the current level
    public ProGameMode mode = ProGameMode.idle;

    // Start is called before the first frame update
    void Start()
    {
        S = this; //define the singleton

        level = 0;
        levelMax = maps.Length;
        StartLevel();
    }

    void StartLevel()
    {
        //get rid of the old level if it exists
        if (map != null)
        {
            Destroy(map);
        }

        //instantiate new level
        map = Instantiate<GameObject>(maps[level]);
        map.transform.position = mapPos;

        //reset the goal
        BallGoal.goalMet = false;

        UpdateGUI();

        mode = ProGameMode.playing;

    }

    void UpdateGUI()
    {
        //show the data in the GUITexts
        uitLevel.text = "Level: " + (level + 1) + " of " + levelMax;
    }

    void Update()
    {
        UpdateGUI();
        //check for level end
        if ((mode == ProGameMode.playing) && BallGoal.goalMet)
        {
            //change mode to stop checking for level end
            mode = ProGameMode.levelEnd;

            //start the next level in 2 seconds
            Invoke("NextLevel", 2f);
        }
    }

    void NextLevel()
    {
        level++;
        if (level == levelMax)
        {
            Destroy(map);
            uitWin.text = "Levels Complete!";
            uitLevel.text = " ";
        }
        StartLevel();
    }

    //static method that allows code anywhere to get a reference to S.map
    static public GameObject GET_MAP()
    {
        return S.map;
    }

}
