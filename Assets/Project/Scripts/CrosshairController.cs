using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    [SerializeField] private Transform outerCircle;
    [SerializeField] private Transform outerSpikes;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float counter = 0f;
    float sign = 1;

    void FixedUpdate()
    {
        counter += sign * 0.05f;

       
        transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 1.2f,counter);

        if(counter > 1)
        {
            counter = 1;
            sign = -1;
        }

        if(counter < 0)
        {
            counter = 0;
            sign = 1;
        }
        //outerSpikes.transform.localScale
    }
}
