using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject bulletObject;
    [SerializeField] private Transform shootPosition;
    [SerializeField] private ParticleSystem muzzleParticle;
    private float healthValue;
    private bool shootEnabled = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Vector3 forwardSpeed = Vector3.right * Globals.GetPlayerSpeed() * Globals.GetEnemySpeedRatio() * Time.deltaTime;
        transform.Translate(forwardSpeed);

        Shoot();
    }

    void LateUpdate()
    {

    }




    void Shoot()
    {
        if(shootEnabled)
        {
            StartCoroutine(EnableShooting());
            shootEnabled = false;
            muzzleParticle.Play();
            GameObject cloneBullet = Instantiate(bulletObject,shootPosition.position,Quaternion.identity);
            Rigidbody rb = cloneBullet.GetComponent<Rigidbody>();
            cloneBullet.SetActive(true);
            rb.AddForce(Vector3.right * -150,ForceMode.Impulse);
        }
    }

    IEnumerator EnableShooting()
    {
        yield return new WaitForSeconds(Globals.GetEnemyShootDelay());
        shootEnabled = true;
    }

}
