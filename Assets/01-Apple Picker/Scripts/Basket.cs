using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    public ScoreCounter scoreCounter;
    // Start is called before the first frame update
    void Start()
    {
        //find a gameobject named scorecounter in the scene hierarchy
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        //get the scorecounter script component of scorego
        scoreCounter = scoreGO.GetComponent<ScoreCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        //get current screen position of the mouse from input
        Vector3 mousePos2D = Input.mousePosition;

        //sets how far to push mouse into 3d
        mousePos2D.z = -Camera.main.transform.position.z;

        //convert the point from 2d screen space into 3d world space
        Vector3 mousePos3d = Camera.main.ScreenToWorldPoint(mousePos2D);

        //move the x position of this basket to the x position of the mouse
        Vector3 pos = this.transform.position;
        pos.x = mousePos3d.x;
        this.transform.position = pos;
    }

    void OnCollisionEnter(Collision coll)
    {
        //find out what hit the object
        GameObject collidedWith = coll.gameObject;
        if(collidedWith.CompareTag("Apple")){
            Destroy(collidedWith);
            scoreCounter.score += 100;
        }
    }
    
}
