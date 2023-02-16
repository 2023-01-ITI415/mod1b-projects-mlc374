using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Inscribed")]
    // Prefab for instantiating apples
    public GameObject applePrefab;

    // speed at which appletree moves
    public float speed = 1f;

    //distance where appletree turns around
    public float leftAndRightEdge = 10f;

    //chance that appletree changes directions
    public float changeDirChance = 0.1f;

    //seconds between apples instantiations
    public float appleDropDelay = 1f;

    void Start()
    {
        //start dropping apples
        Invoke ("DropApple", 2f);
    }

    void DropApple() {
        GameObject apple = Instantiate(applePrefab);
        apple.transform.position = transform.position;
        Invoke("DropApple", appleDropDelay);
    }

    // Update is called once per frame
    void Update()
    {
        //basic movement
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        //changing direction
        if (pos.x < -leftAndRightEdge){
            speed=Mathf.Abs(speed); //move right
        }
        else if (pos.x > leftAndRightEdge){
            speed=-Mathf.Abs(speed); //move left
        }
        //else if (Random.value < changeDirChance){
        //    speed *= -1; //change direction randomly
        //}
    }

    void FixedUpdate(){
        //make dirchanges time-baased instead of frame based
        if (Random.value < changeDirChance) {
            speed *= -1;
        }
    }
}
