using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private GameObject bulletTrail;
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
            meshRenderer.enabled = false;
            bulletTrail.SetActive(false);
            Destroy(gameObject,3);
        }

        
    }
}
