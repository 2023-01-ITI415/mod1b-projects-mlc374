using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWalls : MonoBehaviour
{
    [Header("Inscribed")]
    public float wallSpeed = 1f;
    // Start is called before the first frame update
   
    void Update()
    {
        //basic movement
        Vector3 wallPos = transform.position;
        wallPos.x += wallSpeed * Time.deltaTime;
        transform.position = wallPos;

        //changing direction
        if (wallPos.x < -34.5){
            wallSpeed = Mathf.Abs(wallSpeed);
        }
        else if (wallPos.x > -23){
            wallSpeed = -Mathf.Abs(wallSpeed);
        }
    }
}
