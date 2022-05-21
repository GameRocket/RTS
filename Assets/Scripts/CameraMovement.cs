using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    float speed;                //  How fast the camera can move back and forward across the map
    float zoomSpeed;            //  How fast the camera can scroll up and down
    float rotateSpeed;          //  How fast the camera can spin around on it's axis

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
    }
}
