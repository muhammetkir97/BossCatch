using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{

    [SerializeField] private GameObject roadObject;
    [SerializeField] private GameObject targetObject;
    private float roadLength = 20.418f;
    private float lastRoadPosition;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(targetObject.transform.position.x + Globals.GetRoadCreationLimit() > lastRoadPosition )
        {
            Vector3 roadPos = new Vector3(lastRoadPosition,0,0);

            GameObject clone = Instantiate(roadObject, roadPos, roadObject.transform.rotation);

            lastRoadPosition += roadLength;
        }    
    }
}
