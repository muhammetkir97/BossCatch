using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private Transform shootPosition;
    [SerializeField] private GameObject bulletObject;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShootGun()
    {
        muzzleFlash.Play();
        GameObject cloneBullet = Instantiate(bulletObject,shootPosition.position,Quaternion.identity);
        Rigidbody rb = cloneBullet.GetComponent<Rigidbody>();
        cloneBullet.SetActive(true);
        rb.AddForce(cloneBullet.transform.right * 150,ForceMode.Impulse);
    }
}
