using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CameraMovement : MonoBehaviour
{
    public float speed = 1f; // starting speed
    public float acceleration = 0.05f; // acceleration rate
    public Vector3 direction = new Vector3(0,0,1); // movement direction

    void Update()
    {
        // set the camera's position to the new position
        transform.position += (direction * speed * Time.deltaTime);
        //Debug.Log("Camera speed: " + speed);
        // increase the speed based on the acceleration rate
        if (speed < 5)
            speed += acceleration * Time.deltaTime;

    }
}
