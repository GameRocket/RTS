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

    Vector2 p1;
    Vector2 p2;

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

        //  Horizontal movement speed variable
        float hsp = transform.position.y * speed * Input.GetAxis("Horizontal");

        //  Vertical movement speed variable
        float vsp = transform.position.y * speed * Input.GetAxis("Vertical");

        //  ScrollWheel movement speed variable
        float scrollSp = Mathf.Log(transform.position.y) * -zoomSpeed * Input.GetAxis("Mouse ScrollWheel");

        //  To check if we are above our maximum height
        if ((transform.position.y >= maxHeight) && (scrollSp > 0))
        {
            scrollSp = 0;
        }
        //  To check if we are below our minimum height
        else if ((transform.position.y <= minHeight) && (scrollSp < 0))
        {
            scrollSp = 0;
        }

        //  To check if our current position and scrollspeed is greater than maximum height
        if ((transform.position.y + scrollSp) > maxHeight)
        {
            scrollSp = maxHeight - transform.position.y;
        }
        //  To check if our current position and scrollspeed is lower than minimum height
        else if((transform.position.y + scrollSp) < minHeight)
        {
            scrollSp = minHeight - transform.position.y;
        }

        //  Vectors for moving in each direction
        Vector3 verticalMove = new Vector3(0, scrollSp, 0);
        Vector3 lateralMove = hsp * transform.right;
        Vector3 forwardMove = transform.forward;

        //  We don't want to move down
        forwardMove.y = 0.0f;
        forwardMove.Normalize();
        forwardMove *= vsp;

        //  Add all vectors all together to one move vector
        Vector3 move = verticalMove + lateralMove + forwardMove;

        //  Add this one vector to our position
        transform.position += move;
    }

    /// <summary>
    /// Function that takes arguments and returns void
    /// </summary>
    void getCameraRotation()
    {
        //  Check if the middle mouse button is pressed
        if (Input.GetMouseButtonDown(2))
        {
            p1 = Input.mousePosition;
        }

        //  Check if the middle mouse button is being held down
        if (Input.GetMouseButtonDown(2))
        {
            p2 = Input.mousePosition;

            float dx = (p2 - p1).x * rotateSpeed;
            float dy = (p2 - p1).y * rotateSpeed;

            //  Y rotation
            transform.rotation *= Quaternion.Euler(new Vector3(0, dx, 0));
            transform.GetChild(0).transform.rotation *= Quaternion.Euler(new Vector3(-dy, 0, 0));

            p1 = p2;
        }
    }
}
