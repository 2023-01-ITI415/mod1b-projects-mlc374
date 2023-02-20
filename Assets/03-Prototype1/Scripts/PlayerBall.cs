using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    [Header("Inscribed")]
    public float velocityMult = 10f;
    [Header("Dynamic")]
    public GameObject playerBall;
    public Vector3 ballPos;
    public bool aimingMode;


    void Awake(){
        Transform playerBallTrans = transform.Find("Halo");
        playerBall = playerBallTrans.gameObject;
        playerBall.SetActive(false);
        ballPos = playerBallTrans.position;
    }

    void OnMouseEnter() {
        //print("hovering");
        playerBall.SetActive(true);
    }

    void OnMouseExit() {
        //print("not hovering");
        playerBall.SetActive(false);
    }

    void onMouseDown(){
        //player has pressed the mouse button while over ball
        aimingMode = true;
        playerBall.transform.position = ballPos;
        playerBall.GetComponent<Rigidbody>().isKinematic = true;
    }

    void Update(){
        //if ball is not in aimingMode, don't run this code
        if(!aimingMode) return;

        //get the current mouse position
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.y = -Camera.main.transform.position.y;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint( mousePos2D);

        //find delta from ballPos to mousepos3d
        Vector3 mouseDelta = mousePos3D - ballPos;
        //limit mouseDelta to the sphere collider
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude > maxMagnitude){
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        //move the playerball to this new position
        Vector3 newPos = ballPos + mouseDelta;
        playerBall.transform.position = newPos;

        if(Input.GetMouseButtonUp(0)){
            //the mouse has been released
            aimingMode = false;
            Rigidbody playerBallRB = playerBall.GetComponent<Rigidbody>();
            playerBallRB.isKinematic = false;
            playerBallRB.velocity = -mouseDelta * velocityMult;
            playerBall = null;
        }
    }


}
