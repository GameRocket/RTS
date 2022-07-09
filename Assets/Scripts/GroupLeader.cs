using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupLeader : MonoBehaviour
{
    public GameObject target;
    public GameObject child_prefab;
    public List<GameObject> children;

    // Start is called before the first frame update
    void Start()
    {
        children = new List<GameObject>();

        for( int i = 0; i < 12; i++ )
        {
            Vector3 relative_spawn = new Vector3(i % 4, 0.33f, i / 4);
            GameObject temp = Instatiate(child_prefab, transform.position + (relative_spawn * 6.0f), transform.rotation);
            temp.GetComponent<base_behavior>().target = gameObject;
            children.Add(temp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (target.transform.position - transform.position).normalized * Time.deltaTime * 5.0f;
    }
}
