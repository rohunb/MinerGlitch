using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TagsAndLayers : ScriptableObject
{
    //Layers
    [SerializeField]
    private int playerShipLayer = 8;
    [SerializeField]
    private int enemyShipLayer = 9;
    [SerializeField]
    private int environmentLayer = 10;
    [SerializeField]
    private int bossLayer = 11;
    [SerializeField]
    private int spawnLayer = 12;

    public static int PlayerShipLayer;
    public static int EnemyShipLayer;
    public static int EnvironmentLayer ;
    public static int BossLayer;
    public static int SpawnLayer;

    //Tags
    public string enemyShipTag = "EnemyShip";

    public string spawnBeaconTag = "SpawnBeacon";

    public static string EnemyShipTag ;

    public static string SpawnBeaconTag;


    public static bool GlitchCanCollide(int layer)
    {
        return (layer == PlayerShipLayer || layer == EnemyShipLayer);
    }
    private void OnEnable()
    {
        PlayerShipLayer = playerShipLayer;
        EnemyShipLayer = enemyShipLayer;
        EnvironmentLayer = environmentLayer;
        BossLayer = bossLayer;
        SpawnLayer = spawnLayer;

        EnemyShipTag = enemyShipTag;
        SpawnBeaconTag = spawnBeaconTag;
    }
}
