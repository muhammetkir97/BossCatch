using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBonusController : MonoBehaviour
{
    bool moveStatus = false;
    bool isTaken = false;
    Vector3 playerPos;
    PoolController poolController;
    Transform targetObject;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Move();
        if(isTaken) TakeEffect();
    }

    void Move()
    {
        if(moveStatus)
        {
            transform.Translate(-Vector3.right * Globals.GetEnemySpeed() / 3f * Time.deltaTime,Space.Self);
            if(transform.position.x < playerPos.x - 20f)
            {
                DisableBonus();
            }
        }
    }

    void TakeEffect()
    {
        transform.position = Vector3.Lerp(transform.position,targetObject.position,Time.deltaTime * 45);
    }

    public void GetBonus(Transform target)
    {
        targetObject = target;
        moveStatus = false;
        iTween.Stop(transform.GetChild(0).gameObject);
        //iTween.ScaleTo(gameObject,Vector3.zero,0.3f);
        //iTween.MoveTo(gameObject,iTween.Hash("position",target,"time",0.3f));
        isTaken = true;
        Invoke("DisableBonus",0.3f);
    }

    void DisableBonus()
    {
        iTween.Stop(transform.GetChild(0).gameObject);
        iTween.Stop(gameObject);
        moveStatus = false;
        poolController.AddToPool(gameObject);
        isTaken = false;
    }

    public void Init(Vector3 pos, PoolController pool)
    {
        iTween.Stop(transform.GetChild(0).gameObject);
        iTween.Stop(gameObject);
        transform.GetChild(0).localPosition = Vector3.zero;
        iTween.MoveTo(transform.GetChild(0).gameObject,iTween.Hash("position", new  Vector3(0,1.5f,0),"time",0.6f,"looptype",iTween.LoopType.pingPong,"islocal",true,"easetype",iTween.EaseType.linear));
        //iTween.RotateTo(transform.GetChild(0).gameObject,iTween.Hash("rotation",new Vector3(0,180,0),"time",1f,"looptype",iTween.LoopType.pingPong,"islocal",true));
        poolController = pool;
        playerPos = pos;
        moveStatus = true;
    }
}
