using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    //fields set in the unity inspector pane
    [Header("Inscribed")]
    public GameObject           projectilePrefab;
    public float                velocityMult = 10f;
    public GameObject           projLinePrefab;

    //fields set dynamically
    [Header("Dynamic")]
    public GameObject           launchPoint;
    public Vector3              launchPos;
    public GameObject           projectile;
    public bool                 aimingMode;


    void Awake(){
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;
    }

    void OnMouseEnter() {
        //print( "Slingshot:OnMouseEnter()");
        launchPoint.SetActive(true);
    }

    void OnMouseExit() {
        //print("Slingshot:OnMouseExit()");
        launchPoint.SetActive(false);
    }

    void OnMouseDown() {
        //the player has pressed the mouse button while over slingshot
        aimingMode = true;
        //instantiate a projectile
        projectile = Instantiate(projectilePrefab) as GameObject;
        //Start it at the launchPoint
        projectile.transform.position = launchPos;
        //set it to isKinematic for now
        projectile.GetComponent<Rigidbody>().isKinematic = true;
    }

    void Update() {
        //if slingshot is not in aimingMode, don't run this code
        if(!aimingMode) return;

        //get the current mouse position in 2d screen coords
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        //find the delta from the launchPos to the mousePos3d
        Vector3 mouseDelta = mousePos3D -launchPos;
        //limit mouseDelta to the radius of the Slingshot Sphere Collider
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude > maxMagnitude) {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }
        //move the projectile to this new position
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;

        if (Input.GetMouseButtonUp(0)){
            //the mouse has been released
            aimingMode = false;
            Rigidbody projRB = projectile.GetComponent<Rigidbody>();
            projRB.isKinematic = false;
            projRB.collisionDetectionMode = CollisionDetectionMode.Continuous;
            projRB.velocity = -mouseDelta * velocityMult;
            FollowCam.POI = projectile; //set the _MainCamera poi
            Instantiate<GameObject>(projLinePrefab, projectile.transform);
            projectile = null;
        }
    }
}
