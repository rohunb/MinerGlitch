using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class LevelConfig : ScriptableObject 
{

    [SerializeField]
    private List<LevelInfo> levelInfoList;

    public static Dictionary<GameScene, LevelInfo> scene_levelInfo_Table;

    public static LevelInfo GetLevelInfo(GameScene gameScene)
    {
        return scene_levelInfo_Table[gameScene];
    }

    private void OnEnable()
    {
        scene_levelInfo_Table = levelInfoList.ToDictionary(l => l.gameScene, l =>l);
    }
	
}

[System.Serializable]
public struct LevelInfo
{
    public GameScene gameScene;
    public List<GameObject> ai_Prefabs;

    //True for enemies to move right to left, false for opposite
    public bool EnemyDirectionLeft;
}