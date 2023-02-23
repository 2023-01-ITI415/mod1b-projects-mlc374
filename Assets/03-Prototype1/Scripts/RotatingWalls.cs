using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingWalls : MonoBehaviour
{
    [Header("Inscribed")]
    public float rotation = 1f;
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate (new Vector3 ( 0, rotation, 0) * Time.deltaTime);
    }
}
