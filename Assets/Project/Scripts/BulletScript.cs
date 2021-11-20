using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private Rigidbody rb;
    private bool isExploded = false;
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        if(!isExploded)
        {
            explosionParticle.Play();
            isExploded = true;
            rb.isKinematic = true;
        }

        
    }
}
