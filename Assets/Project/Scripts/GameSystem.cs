using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance;

    [SerializeField] private GameObject enemyObject;
    private Vector3 lastEnemyPosition = Vector3.zero;

    [SerializeField] private GameObject playerObject;
    [SerializeField] private PoolController bulletPool;
    List<Material> sceneMaterials;

    float curveX = 0;
    float targetCurveX = 0;
    float curveY = 0;
    float targetCurveY = 0;

    float killedEnemyCount = 0;
    int currentWeaponCount = 1;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        bulletPool.InitPool();
        sceneMaterials = new List<Material>();
        List<Material> armat = new List<Material>();
        Renderer[] arrend = (Renderer[])Resources.FindObjectsOfTypeAll(typeof(Renderer));
        foreach (Renderer rend in arrend) 
        {
            foreach (Material mat in rend.sharedMaterials) 
            {
                if (!armat.Contains (mat)) 
                {
                    armat.Add (mat);
                }
            }
        }

        foreach (Material mat in armat) 
        {
            if (mat != null && mat.shader != null && mat.shader.name != null && mat.shader.name.Contains("Curve")) 
            {
                sceneMaterials.Add(mat);
            }
        }

        SetCurve(0,0.3f);
        //Application.targetFrameRate = 30;
        InvokeRepeating("SendEnemy",0f,Globals.GetEnemySpawnRate());
        InvokeRepeating("SetRandomCurve",0,5f);
        InvokeRepeating("SendBulletBonus",0,10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        curveX = Mathf.Lerp(curveX, targetCurveX, Time.deltaTime * Globals.GetCurveSmooth());
        curveY = Mathf.Lerp(curveY, targetCurveY, Time.deltaTime * Globals.GetCurveSmooth());
        SetCurve(curveX,curveY);
    }

    void SendEnemy()
    {

        Vector3 spawnPosition = playerObject.transform.position;
        spawnPosition.x += Globals.GetEnemySpawnDistance();
        spawnPosition.z = Random.Range(-7f,7f);
        GameObject enemy = Instantiate(enemyObject, spawnPosition, Quaternion.identity);
        lastEnemyPosition = enemy.transform.position;

    }


    void SetRandomCurve()
    {
        float yValue = Random.Range(-0.1f,0.1f);
        float xValue = Random.Range(-0.3f,0.3f);

        targetCurveX = xValue;
        targetCurveY = yValue;
    }

    void SetCurve(float x, float y)
    {
        foreach(Material mat in sceneMaterials)
        {
            mat.SetFloat("X_Axis",x);
            mat.SetFloat("Y_Axis",y);
        }
    }


    void SendBulletBonus()
    {
        
        StartCoroutine(CreateBulletBonusDelay());
 
    }

    IEnumerator CreateBulletBonusDelay()
    {
        int bonusCount = Random.Range(Globals.GetMinBonusBullet(),Globals.GetMaxBonusBullet());
        Debug.Log(bonusCount);

        for(int i=0; i<bonusCount; i++)
        {
            Debug.Log(i);
            GameObject bulletBonusClone = bulletPool.GetFromPool();
            bulletBonusClone.transform.position = playerObject.transform.position + new Vector3(80 + (i *6),3,0);
            bulletBonusClone.GetComponent<BulletBonusController>().Init(playerObject.transform.position,bulletPool);
            yield return new WaitForEndOfFrame();
        }
    }

    public GameObject GetPlayerObject()
    {
        return playerObject;
    }

    public void EnemyKilled()
    {
        killedEnemyCount++;

        if(((killedEnemyCount - (killedEnemyCount % Globals.GetWeaponKillCount())) / Globals.GetWeaponKillCount()) + 1 != currentWeaponCount)
        {
            currentWeaponCount++;
            if(currentWeaponCount < 6)  playerObject.GetComponent<PlayerController>().GainGun();
            
        }
    }

    




}
