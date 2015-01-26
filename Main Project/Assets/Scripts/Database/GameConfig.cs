using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class GameConfig : ScriptableObject
{
    [SerializeField]
    private List<GameScene> sceneOrder;

    private static List<GameScene> SceneOrder;

    public static GameScene GetNextScene(GameScene currentScene)
    {
        int index = SceneOrder.IndexOf(currentScene);
        return SceneOrder[index + 1];
    }
    void OnEnable()
    {
        SceneOrder = sceneOrder;
    }
}

