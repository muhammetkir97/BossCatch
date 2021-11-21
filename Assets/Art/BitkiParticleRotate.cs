using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitkiParticleRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(transform.right,2,Space.Self);
        
    }
}
