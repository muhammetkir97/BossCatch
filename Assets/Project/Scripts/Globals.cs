using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals 
{
    private static float playerSpeed = 0;
    private static float sideSpeed = 20;
    private static float enemySpeedRatio = 0.7f;
    private static float enemySpeed = 55f;
    private static float trailerSmooth = 4;
    private static float vehicleSmooth = 15;
    private static float roadCreationLimit = 500;
    private static float enemySpawnDistance = 150;
    private static float playerShootDelay = 0.5f;
    private static float enemyShootDelay = 0.8f;
    private static float curveSmooth = 0.2f;
    private static int minBonusBullet = 2;
    private static int maxBonusBullet = 6;
    private static int enemyHealth = 3;
    private static float enemySpawnRate = 3;


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

    public static float GetCurveSmooth()
    {
        return curveSmooth;
    }

    public static int GetMinBonusBullet()
    {
        return minBonusBullet;
    }

    public static int GetMaxBonusBullet()
    {
        return maxBonusBullet;
    }

    public static int GetEnemyHealth()
    {
        return enemyHealth;
    }

    public static float GetEnemySpawnRate()
    {
        return enemySpawnRate;
    }
}
