using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBuildingButton : MonoBehaviour
{
    public GameObject building_blueprint;

    public void spawn_building_blueprint()
    {
        Instantiate(building_blueprint);
    }
}
