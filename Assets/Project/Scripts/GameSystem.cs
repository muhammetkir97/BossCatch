using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance;

    [SerializeField] private GameObject enemyObject;
    private Vector3 lastEnemyPosition = Vector3.zero;

    [SerializeField] private GameObject playerObject;
    List<Material> sceneMaterials;

    float curveX = 0;
    float targetCurveX = 0;
    float curveY = 0;
    float targetCurveY = 0;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
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
        InvokeRepeating("SendEnemy",0f,5f);
        InvokeRepeating("SetRandomCurve",0,5f);
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

    




}
