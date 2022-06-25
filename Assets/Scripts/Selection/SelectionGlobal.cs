using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionGlobal : MonoBehaviour
{
    SelectedDictionary selected_table;
    RaycastHit hit;

    bool dragSelect;

    //Collider variables
    //=======================================================//

    MeshCollider selectionBox;
    Mesh selectionMesh;

    Vector3 p1;
    Vector3 p2;

    //the corners of our 2d selection box
    Vector2[] corners;

    //the vertices of our meshcollider
    Vector3[] verts;
    Vector3[] vecs;

    // Start is called before the first frame update
    void Start()
    {
        selected_table = GetComponent<SelectedDictionary>();
        dragSelect = false;
    }

    // Update is called once per frame
    void Update()
    {
        //  1. When left mouse button clicked (but not released)
        if ( Input.GetMouseButtonDown(0) )
        {
            p1 = Input.mousePosition;
        }

        //  2. While left mouse button held
        if ( Input.GetMouseButton(0) )
        {
            if((p1 - Input.mousePosition).magnitude > 40)
            {
                dragSelect = true;
            }
        }

        //  3. When mouse button comes up
        if ( Input.GetMouseButtonUp(0) )
        {
            if( dragSelect == false )
            {
                Ray ray = Camera.main.ScreenPointToRay(p1);

                if(Physics.Raycast(ray, out hit, 50000.0f))
                {
                    //  Inclusive select
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        selected_table.addSelected(hit.transform.gameObject);
                    }
                    //  Exclusive select
                    else
                    {
                        selected_table.deselectAll();
                        selected_table.addSelected(hit.transform.gameObject);
                    }    
                }
            }
            //  If we didn't hit something
            else
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    //  do nothing
                }
                else
                {
                    selected_table.deselectAll();
                }
            }    
        }
        else //marquee select
        {
            verts = new Vector3[4];
            vecs = new Vector3[4];
            int i = 0;
            p2 = Input.mousePosition;
            corners = getBoundingBox(p1, p2);

            foreach (Vector2 corner in corners)
            {
                Ray ray = Camera.main.ScreenPointToRay(corner);

                if (Physics.Raycast(ray, out hit, 50000.0f, (1 << 8)))
                {
                    verts[i] = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                    vecs[i] = ray.origin - hit.point;
                    Debug.DrawLine(Camera.main.ScreenToWorldPoint(corner), hit.point, Color.red, 1.0f);
                }
                i++;
            }

            //generate the mesh
            selectionMesh = generateSelectionMesh(verts, vecs);

            selectionBox = gameObject.AddComponent<MeshCollider>();
            selectionBox.sharedMesh = selectionMesh;
            selectionBox.convex = true;
            selectionBox.isTrigger = true;

            if (!Input.GetKey(KeyCode.LeftShift))
            {
                selected_table.deselectAll();
            }

            Destroy(selectionBox, 0.02f);

        }
        //end marquee select

        dragSelect = false;
    }

    private void OnGUI()
    {
        if (dragSelect == true)
        {
            var rect = ScreenUtility.GetScreenRect(p1, Input.mousePosition);
            ScreenUtility.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            ScreenUtility.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }
    
    //create a bounding box (4 corners in order) from the start and end mouse position
    Vector2[] getBoundingBox(Vector2 p1, Vector2 p2)
    {
        // Min and Max to get 2 corners of rectangle regardless of drag direction.
        var bottomLeft = Vector3.Min(p1, p2);
        var topRight = Vector3.Max(p1, p2);
        
        // 0 = top left; 1 = top right; 2 = bottom left; 3 = bottom right;
        Vector2[] corners =
        {
            new Vector2(bottomLeft.x, topRight.y),
            new Vector2(topRight.x, topRight.y),
            new Vector2(bottomLeft.x, bottomLeft.y),
            new Vector2(topRight.x, bottomLeft.y)
        };
        
        return corners;
    }
    
    //generate a mesh from the 4 bottom points
    Mesh generateSelectionMesh(Vector3[] corners, Vector3[] vecs)
    {
        Vector3[] verts = new Vector3[8];
        int[] tris = { 0, 1, 2, 2, 1, 3, 4, 6, 0, 0, 6, 2, 6, 7, 2, 2, 7, 3, 7, 5, 3, 3, 5, 1, 5, 0, 1, 1, 4, 0, 4, 5, 6, 6, 5, 7 }; //map the tris of our cube
        
        for (int i = 0; i < 4; i++)
        {
            verts[i] = corners[i];
        }
        
        for (int j = 4; j < 8; j++)
        {
            verts[j] = corners[j - 4] + vecs[j - 4];
        }
        
        Mesh selectionMesh = new Mesh();
        selectionMesh.vertices = verts;
        selectionMesh.triangles = tris;
        
        return selectionMesh;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        selected_table.addSelected(other.gameObject);
    }
}