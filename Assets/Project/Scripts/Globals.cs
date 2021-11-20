using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals 
{
    private static float playerSpeed = 0;
    private static float sideSpeed = 20;
    private static float enemySpeedRatio = 0.7f;
    private static float enemySpeed = 15f;
    private static float trailerSmooth = 4;
    private static float vehicleSmooth = 15;
    private static float roadCreationLimit = 500;
    private static float enemySpawnDistance = 80;
    private static float playerShootDelay = 0.2f;
    private static float enemyShootDelay = 0.8f;


    public static float GetPlayerSpeed()
    {
        return playerSpeed;
    }

    public static float GetTrailerSmooth()
    {
        return trailerSmooth;
    }

    public static float GetRoadCreationLimit()
    {
        return roadCreationLimit;
    }

    public static float GetVehicleSmooth()
    {
        return vehicleSmooth;
    }

    public static float GetEnemySpawnDistance()
    {
        return enemySpawnDistance;
    }

    public static float GetPlayerSideSpeed()
    {
        return sideSpeed;
    }

    public static float GetPlayerShootDelay()
    {
        return playerShootDelay;
    }

    public static float GetEnemySpeedRatio()
    {
        return enemySpeedRatio;
    }

    public static float GetEnemyShootDelay()
    {
        return enemyShootDelay;
    }

    public static float GetEnemySpeed()
    {
        return enemySpeed;
    }
}
