using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionGlobal : MonoBehaviour
{
    id_dictionary id_table;
    selected_dictionary selected_table;
    RaycastHit hit;

    bool dragSelect;

    //  Colider variables
    Vector3 p1;
    Vector3 p2;

    // Start is called before the first frame update
    void Start()
    {
        id_table = GetComponent<id_dictionary>();
        selected_table = GetComponent<selected_dictionary>();
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
            if((p1 - Input.moousePosition).magnitude > 40)
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
        
    }
}
