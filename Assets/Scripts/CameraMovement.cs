using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that controls how the move the camera in game
/// </summary>
public class CameraMovement : MonoBehaviour
{
    /// <summary>
    /// How fast the camera can move back and forward across the map
    /// </summary>
    float speed = 0.06f;

    /// <summary>
    /// How fast the camera can scroll up and down
    /// </summary>
    float zoomSpeed = 10.0f;

    /// <summary>
    /// How fast the camera can spin around on it's axis
    /// </summary>
    float rotateSpeed = 0.0f;

    /// <summary>
    /// Controls how high camera can go
    /// </summary>
    float maxHeight = 40.0f;

    /// <summary>
    /// Controls how low camera can go
    /// </summary>
    float minHeight = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Trying to implement fast mode
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 0.06f;
            zoomSpeed = 20.0f;
        }
        else
        {
            speed = 0.035f;
            zoomSpeed = 10.0f;
        }

        float hsp = speed * Input.GetAxis("Horizontal");                        // Horizontal movement speed variable
        float vsp = speed * Input.GetAxis("Vertical");                          // Vertical movement speed variable
        float scrollSp = -zoomSpeed * Input.GetAxis("Mouse Scrollwheel");       // Scrollwheel movement speed variable

        //  Vectors for moving in each direction
        Vector3 verticalMove = new Vector3(0, scrollSp, 0);
        Vector3 lateralMove = hsp * transform.right;
        Vector3 forwardMove = transform.forward;

        forwardMove.y = 0.0f;                                                   // We don't want to move down
        forwardMove.Normalize();
        forwardMove *= vsp;

        Vector3 move = verticalMove + lateralMove + forwardMove;                // Add all vectors all together to one move vector

        transform.position += move;                                             // Add this one vector to our position
    }
}
