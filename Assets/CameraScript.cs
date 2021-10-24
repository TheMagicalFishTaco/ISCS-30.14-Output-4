using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject target;
    //offset is set to -10 on the Z axis since there's a strange issue in the tilemap and the player sprite needs to be set to -2
    //having the camera offset that far just makes sure that it's always going to be the "highest" object along the z axis.
    public Vector3 offset = new Vector3(0, 0, -10);

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target)
        {
            //just changes the camera's position to follow the player
            transform.position = new Vector3(
                target.transform.position.x + offset.x,
                target.transform.position.y + offset.y,
                target.transform.position.z + offset.z);
        }
    }
}
