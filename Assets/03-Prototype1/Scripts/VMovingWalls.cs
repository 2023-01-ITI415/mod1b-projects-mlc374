using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VMovingWalls : MonoBehaviour
{
    [Header("Inscribed")]
    public float wallSpeed = 1f;
    public float topBound = 1f;
    public float bottomBound = 1f;
    // Start is called before the first frame update
   
    void Update()
    {
        //basic movement
        Vector3 wallPos = transform.position;
        wallPos.z += wallSpeed * Time.deltaTime;
        transform.position = wallPos;

        //changing direction
        if (wallPos.z < bottomBound){
            wallSpeed = Mathf.Abs(wallSpeed);
        }
        else if (wallPos.z > topBound){
            wallSpeed = -Mathf.Abs(wallSpeed);
        }
    }
}
