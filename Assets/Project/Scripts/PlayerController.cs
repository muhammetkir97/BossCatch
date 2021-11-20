using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform bulletStackParent;
    private int bulletStackCount;
    private int lastBulletStackCount = -1;
    private Transform trailer;
    private Transform vehicle;
    private float lastTrailerPos;
    private float targetTrailerPos;

    private float lastVehiclePos;
    private float targetVehiclePos;

    private float lastStackBend;
    private float targetStackBend;


    private int totalBullet = 0;

    private Transform gun;
    private Transform shootPosition;
    private GameObject bulletObject;
    private ParticleSystem muzzleParticle;
    private bool shootEnabled = true;

    private Vector3 lastSideSpeed = Vector3.zero;


    void Awake()
    {
        vehicle = transform.GetChild(0);
        trailer = vehicle.GetChild(0);
        bulletStackParent = trailer.GetChild(0);
        bulletStackCount = bulletStackParent.childCount * 6;

        gun = vehicle.GetChild(1);
        shootPosition = gun.GetChild(0).GetChild(0);
        bulletObject = gun.GetChild(1).gameObject;
        muzzleParticle = shootPosition.GetChild(0).GetComponent<ParticleSystem>();
    }


    void Start()
    {
        totalBullet = 40;
        SetBulletStack();
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.A)) GainBullet();

        //if(Input.GetKeyDown(KeyCode.B)) UseBullet();
    }

    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.C)) GainBullet();

        if(Input.GetKey(KeyCode.B)) UseBullet();

        if(Input.GetKey(KeyCode.X)) Shoot();

        Move();
        BendStack();
    }

    void BendStack()
    {
        targetStackBend = GetMovementInputVector().x;
        lastStackBend = Mathf.Lerp(lastStackBend,targetStackBend,Time.deltaTime * 3);

        int stackIndexCount = bulletStackCount / 6;

        for(int i=0; i<stackIndexCount; i++)
        {
            bulletStackParent.GetChild(i).localPosition = new Vector3(i * -0.5f * Mathf.Abs(lastStackBend),i * 0.7f,-i * lastStackBend / 3f);
            bulletStackParent.GetChild(i).localRotation = Quaternion.Euler(i * lastStackBend * -10,0,0);
        }

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
            rb.AddForce(Vector3.right * 150,ForceMode.Impulse);
            UseBullet();
        }
    }

    IEnumerator EnableShooting()
    {
        yield return new WaitForSeconds(Globals.GetPlayerShootDelay());
        shootEnabled = true;
    }


    void Move()
    {
        Vector3 forwardSpeed = Vector3.right * Globals.GetPlayerSpeed() * 1 * Time.deltaTime;
        Vector3 sideSpeed = Vector3.forward * Globals.GetPlayerSideSpeed() * GetMovementInputVector().x * Time.deltaTime;
        Vector3 smoothSideSpeed = Vector3.Lerp(lastSideSpeed, sideSpeed,Time.deltaTime * 5f);
        lastSideSpeed = smoothSideSpeed;
        transform.Translate(forwardSpeed + smoothSideSpeed);

        lastTrailerPos = transform.position.z;

        targetTrailerPos = Mathf.Lerp(targetTrailerPos, lastTrailerPos, Time.deltaTime * Globals.GetTrailerSmooth());

        lastVehiclePos = vehicle.position.z;

        targetVehiclePos = Mathf.Lerp(targetVehiclePos, lastVehiclePos, Time.deltaTime * Globals.GetVehicleSmooth());

        trailer.localRotation = Quaternion.Euler(0,-(lastTrailerPos - targetTrailerPos) * 5,0);
        vehicle.localRotation = Quaternion.Euler(0, -(lastVehiclePos - targetVehiclePos) * 13, 0);

        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.right, out hit, 500))
        {
            Shoot();
        }

    }

    Vector2 GetMovementInputVector()
    {
        Vector2 movementVector = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));

        return movementVector;
    }


    void GainBullet()
    {
        totalBullet++;
        SetBulletStack();
    }

    void UseBullet()
    {   
        totalBullet--;
        SetBulletStack();
    }

    void SetBulletStack()
    {
        int availableStack = (totalBullet - (totalBullet % 4)) / 4;
        availableStack++;

        if(availableStack != lastBulletStackCount)
        {
            if(totalBullet == 0) availableStack = 0;

            for(int i=0; i<bulletStackCount; i++)
            {
                bulletStackParent.GetChild((i - (i % 6)) / 6).GetChild(i % 6).gameObject.SetActive(i<availableStack);
            }
            lastBulletStackCount = availableStack;
        }
    }
}
