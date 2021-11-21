using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject bulletObject;
    [SerializeField] private Transform shootPosition;
    [SerializeField] private ParticleSystem muzzleParticle;
    [SerializeField] private GameObject crosshairObject;
    [SerializeField] private GameObject explosionParticle;
    private int healthValue;
    private bool isDied = false;
    private bool shootEnabled = true;
    private bool isFirstShoot = true;

    void Awake()
    {
        healthValue = Globals.GetEnemyHealth();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Movement();
        PlayerDetection();
        TargetDetection();
    }

    void Movement()
    {
        Vector3 forwardSpeed = -Vector3.right * Globals.GetEnemySpeed() * Time.deltaTime;
        transform.Translate(forwardSpeed);
    }

    void PlayerDetection()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,-transform.right, out hit, 200))
        {
            /*
            if(isFirstShoot)
            {
                isFirstShoot = false;
                StartCoroutine(EnableShooting());
            }
            */

            if(hit.transform.name == "Player")
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        if(shootEnabled && !isDied)
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

    public void TakeDamage()
    {
        iTween.PunchScale(gameObject,Vector3.one * 0.3f,0.2f);
        healthValue--;
        if(healthValue <= 0 && !isDied) EnemyDie();
    }

    void EnemyDie()
    {
        isDied = true;
        GameObject cloneParticle = Instantiate(explosionParticle,transform.position,Quaternion.identity);
        cloneParticle.GetComponent<ParticleSystem>().Play();
        iTween.ScaleTo(gameObject,Vector3.zero,0.3f);
        GameSystem.Instance.EnemyKilled();
    }

    void TargetDetection()
    {
        bool status = Mathf.Abs(transform.position.z - GameSystem.Instance.GetPlayerObject().transform.position.z) < 2f;
        crosshairObject.SetActive(status);
    }

}
