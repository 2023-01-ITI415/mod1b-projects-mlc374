using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Renderer))]
public class BallGoal : MonoBehaviour
{
    //a static field accesible by code anywhere
    static public bool goalMet = false;

    void OnTriggerEnter(Collider other){
        //when the trigger is hit by something
        //check to see if it's a projectile
        PlayerController proj = other.GetComponent<PlayerController>();
        if (proj != null) {
            //if so, set metgaol to true
            BallGoal.goalMet = true;
            //also set the alpha of the color to higher opacity
            Material mat = GetComponent<Renderer>().material;
            Color c = mat.color;
            c.a = 0.75f;
            mat.color = c;
        }
    }
}
