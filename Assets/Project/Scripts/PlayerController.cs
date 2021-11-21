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

    private float lastTrailerRot;
    private float targetTrailerRot;

    private float lastVehiclePos;
    private float targetVehiclePos;

    private float lastStackBend;
    private float targetStackBend;


    private int totalBullet = 0;

    private Transform gunParent;
    private bool shootEnabled = true;

    private Vector3 lastSideSpeed = Vector3.zero;

    private int totalGun = 5;


    void Awake()
    {
        vehicle = transform.GetChild(0);
        trailer = vehicle.GetChild(0);
        bulletStackParent = trailer.GetChild(0);
        bulletStackCount = bulletStackParent.childCount * 6;

        gunParent = vehicle.GetChild(1);

    }


    void Start()
    {
        totalBullet = 40;
        SetBulletStack();
        SetGun();
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
            bulletStackParent.GetChild(i).localPosition = new Vector3(i * -0.5f * Mathf.Abs(lastStackBend),i * 0.7f,i * lastStackBend / 3f);
            bulletStackParent.GetChild(i).localRotation = Quaternion.Euler(i * lastStackBend * 10,0,0);
        }
    }

    void Shoot()
    {
        if(shootEnabled)
        {
            StartCoroutine(EnableShooting());
            shootEnabled = false;
            StartCoroutine(ShootDelay());

        }
    }

    IEnumerator ShootDelay()
    {
        float delayTime = 0.1f;
        List<GunController> gunList = new List<GunController>();

        for(int i=0; i< gunParent.GetChild(totalGun - 1).childCount; i++)
        {
            gunList.Add(gunParent.GetChild(totalGun - 1).GetChild(i).GetComponent<GunController>());
        }

        switch(gunList.Count)
        {
            case 1:
                gunList[0].ShootGun();
                break;
            case 2:
                gunList[0].ShootGun();
                gunList[1].ShootGun();
                break;
            case 3:
                gunList[0].ShootGun();
                yield return new WaitForSeconds(delayTime);
                gunList[1].ShootGun();
                gunList[2].ShootGun();
                break;
            case 4:
                gunList[0].ShootGun();
                gunList[1].ShootGun();
                yield return new WaitForSeconds(delayTime);
                gunList[2].ShootGun();
                gunList[3].ShootGun();
                break;
            case 5:
                gunList[0].ShootGun();
                yield return new WaitForSeconds(delayTime);
                gunList[1].ShootGun();
                gunList[2].ShootGun();
                yield return new WaitForSeconds(delayTime);
                gunList[3].ShootGun();
                gunList[4].ShootGun();
                break;

        }

        UseBullet();
    }

    IEnumerator EnableShooting()
    {
        yield return new WaitForSeconds(Globals.GetPlayerShootDelay());
        shootEnabled = true;
    }


    void Move()
    {
        Vector3 forwardSpeed = Vector3.right * Globals.GetPlayerSpeed() * 1 * Time.deltaTime;
        Vector3 sideSpeed = Vector3.forward * Globals.GetPlayerSideSpeed() * -GetMovementInputVector().x * Time.deltaTime;
        Vector3 smoothSideSpeed = Vector3.Lerp(lastSideSpeed, sideSpeed,Time.deltaTime * 5f);
        lastSideSpeed = smoothSideSpeed;
        transform.Translate(forwardSpeed + smoothSideSpeed);

        lastTrailerPos = transform.position.z;

        targetTrailerPos = Mathf.Lerp(targetTrailerPos, lastTrailerPos, Time.deltaTime * Globals.GetTrailerSmooth());

        lastVehiclePos = vehicle.position.z;

        targetVehiclePos = Mathf.Lerp(targetVehiclePos, lastVehiclePos, Time.deltaTime * Globals.GetVehicleSmooth());

        lastTrailerRot = GetMovementInputVector().x;
        targetTrailerRot = Mathf.Lerp(targetTrailerRot, lastTrailerRot,Time.deltaTime * 3);



        trailer.localRotation = Quaternion.Euler(-(lastTrailerRot - targetTrailerRot) * -15,-(lastTrailerPos - targetTrailerPos) * 5,0);
        vehicle.localRotation = Quaternion.Euler(0, -(lastVehiclePos - targetVehiclePos) * 13, 0);

    

        RaycastHit hit;

        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        if(Physics.Raycast(transform.position,transform.right, out hit, 500,layerMask))
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


    void SetGun()
    {
        for(int i=0; i< gunParent.childCount; i++)
        {
            gunParent.GetChild(i).gameObject.SetActive((i + 1)==totalGun);



        }
    }
}
