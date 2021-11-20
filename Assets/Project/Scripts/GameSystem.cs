using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance;

    [SerializeField] private GameObject enemyObject;
    private Vector3 lastEnemyPosition = Vector3.zero;

    [SerializeField] private GameObject playerObject;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        InvokeRepeating("SendEnemy",5f,5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {

    }

    void SendEnemy()
    {

        Vector3 spawnPosition = playerObject.transform.position;
        spawnPosition.x += Globals.GetEnemySpawnDistance();
        spawnPosition.z = Random.Range(-7f,7f);
        GameObject enemy = Instantiate(enemyObject, spawnPosition, Quaternion.identity);
        lastEnemyPosition = enemy.transform.position;

    }

    




}
