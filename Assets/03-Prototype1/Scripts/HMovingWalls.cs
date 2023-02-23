using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HMovingWalls : MonoBehaviour
{
    [Header("Inscribed")]
    public float wallSpeed = 1f;
    public float leftBound = 1f;
    public float rightBound = 1f;
    // Start is called before the first frame update
   
    void Update()
    {
        //basic movement
        Vector3 wallPos = transform.position;
        wallPos.x += wallSpeed * Time.deltaTime;
        transform.position = wallPos;

        //changing direction
        if (wallPos.x < leftBound){
            wallSpeed = Mathf.Abs(wallSpeed);
        }
        else if (wallPos.x > rightBound){
            wallSpeed = -Mathf.Abs(wallSpeed);
        }
    }
}
