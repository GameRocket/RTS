using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    /// <summary>
    /// How fast the camera can move back and forward across the map
    /// </summary>
    float speed = 0.06f;

    /// <summary>
    /// How fast the camera can scroll up and down
    /// </summary>
    float zoomSpeed = 10.0;

    /// <summary>
    /// How fast the camera can spin around on it's axis
    /// </summary>
    float rotateSpeed = 0;

    float maxHeight = 40f;      //  How high camera can go 
    float minHeight = 4f;       //  How low camera can go

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float hsp = speed * Input.GetAxis("Horizontal");
        float vsp = speed * Input.GetAxis("Vertical");
        float scrollSp = -zoomSpeed * Input.GetAxis("Mouse Scrollwheel");


        Vector3 verticalMove = new Vector3(0, scrollSp, 0);
        Vector3 lateralMove = hsp * transform.right;
        Vector3 forwardMove = transform.forward;
        forwardMove.y = 0;
        forwardMove.Normalize();
        forwardMove *= vsp;

        Vector3 move = verticalMove + lateralMove + forwardMove;

        transform.position += move;
    }
}
