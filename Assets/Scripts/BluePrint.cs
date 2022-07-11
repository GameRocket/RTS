using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BluePrint - object placement preview class (script)
/// </summary>
public class BluePrint : MonoBehaviour
{
    /// <summary>
    /// Hit object
    /// </summary>
    RaycastHit hit;

    /// <summary>
    /// Move point for object placement
    /// </summary>
    Vector3 movePoint;

    /// <summary>
    /// Game object that we will place after destroying
    /// </summary>
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        //  We do a raycast from our mouse position out in to the camera
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //  Than it hits terrain
        if ( Physics.Raycast(ray, out hit, 50000.0f, (1 << 8)))
        {
            //  We going to set our new blue print object position to be
            transform.position = hit.point;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //  We do a raycast from our mouse position out in to the camera
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //  Than it hits terrain
        if (Physics.Raycast(ray, out hit, 50000.0f, (1 << 8)))
        {
            //  We going to set our new blue print object position to be
            transform.position = hit.point;
        }

        //  Here we want to make sure, that than user press the mouse button
        if (Input.GetMouseButton(0))
        {
            //  We destroy blue print and we create real object instead
            Instantiate(prefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
