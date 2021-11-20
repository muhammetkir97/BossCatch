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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        CheckEnemy();
    }

    void CheckEnemy()
    {
        Vector3 detectPosition = new Vector3(lastEnemyPosition.x + Globals.GetEnemySpawnDistance(), 0, 0);

        if(playerObject.transform.position.magnitude > detectPosition.magnitude)
        {
            Vector3 spawnPosition = playerObject.transform.position;
            spawnPosition.x += Globals.GetEnemySpawnDistance();
            GameObject enemy = Instantiate(enemyObject, spawnPosition, Quaternion.identity);
            lastEnemyPosition = enemy.transform.position;


        }
    }

    




}
