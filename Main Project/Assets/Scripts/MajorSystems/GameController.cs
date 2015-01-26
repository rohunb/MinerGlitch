using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum GameScene { MainMenu, GlitchDesktop, GlitchSpashScreen, GlitchBSOD, Level0, Level1, Level2, Level3,GameOver, WinScreen, MovementTest}

public class GameController : Singleton<GameController> 
{
    GameScene currentScene = GameScene.MainMenu;
    public GameScene CurrentScene
    {
        get { return currentScene; }
    }
    private GameScene gameSceneToLoad;
    private GameScene nextTransitionScene;

    string gameSceneToLoadName = "nextGameScene";
    string nextTransitionSceneName = "nextTransition";

    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.N))
        {
            LevelComplete();
        }
    }

    public void LevelComplete()
    {
        Debug.Log("Level complete");
        nextTransitionScene = GameConfig.GetNextScene(currentScene);
        ChangeScene(nextTransitionScene);
    }

    private void ChangeScene(GameScene nextScene)
    {
        Application.LoadLevel(nextScene.ToString());
    }
    //void Save()
    //{
    //    Debug.Log("save");
    //    PlayerPrefs.SetInt(gameSceneToLoadName, (int)gameSceneToLoad);
    //    PlayerPrefs.SetInt(nextTransitionSceneName, (int)nextTransitionScene);
    //}
    //void Load()
    //{
    //    Debug.Log("Load");
    //    gameSceneToLoad = (GameScene)PlayerPrefs.GetInt(gameSceneToLoadName);
    //    nextTransitionScene = (GameScene)PlayerPrefs.GetInt(nextTransitionSceneName);
    //}
    void Awake()
    {
        Debug.LogWarning("Loaded level " + Application.loadedLevelName);
        currentScene = (GameScene)Enum.Parse(typeof(GameScene), Application.loadedLevelName);
        if(currentScene == null)
        {
            Debug.LogError("Could not identify current scene " + Application.loadedLevelName);
        }
    }
 
}
