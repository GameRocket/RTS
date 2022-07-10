using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupLeader : MonoBehaviour
{
    public GameObject target;               //  object children will follow
    public GameObject child_prefab;         //  that members of a squad are
    public List<GameObject> children;       //  list of all our prefabs to track

    // Start is called before the first frame update
    void Start()
    {
        //  We create a list to check all childrens and track them
        children = new List<GameObject>();

        //  Here we instantiate new child units
        for( int i = 0; i < 12; i++ )
        {
            //  Spawn positions (trying to make it in columns)
            Vector3 relative_spawn = new Vector3(i % 4, 0.33f, i / 4);

            //  Instatiate this game object and make sure it spawn at relative transform of parent object plus offset
            GameObject temp = Instantiate(child_prefab, transform.position + (relative_spawn * 6.0f), transform.rotation);

            //  Set the target to parent object to make childrens objects follow the parent object

            ///  (!) NEED TO UNCOMMENT THIS LATER
            //temp.GetComponent<base_behavior>().target = gameObject;

            //  Adding new object
            children.Add(temp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //  Make sure the parent object always moving throught some sort of target
        transform.position += (target.transform.position - transform.position).normalized * Time.deltaTime * 5.0f;
    }
}
