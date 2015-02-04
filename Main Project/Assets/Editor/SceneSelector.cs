using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class SceneSelector : Editor 
{
    const string menuName = "Open Scene";
    [MenuItem(menuName + "/Splash")]
    public static void Splash()
    {
        OpenScene("GlitchSpashScreen");
    }

    [MenuItem(menuName + "/GameOver")]
    public static void GameOver()
    {
        OpenScene("GameOver");
    }
    [MenuItem(menuName + "/Desktop")]
    public static void Desktop()
    {
        OpenScene("GlitchDesktop");
    }
    [MenuItem(menuName + "/BSOD")]
    public static void BSOD()
    {
        OpenScene("GlitchBSOD");
    }
    [MenuItem(menuName + "/Win")]
    public static void Win()
    {
        OpenScene("WinScreen");
    }

        [MenuItem(menuName + "/BossTest")]
    public static void BossTest()
    {
        OpenScene("BossTest");
    }
    [MenuItem(menuName + "/Main Menu")]
    public static void OpenMainMenu()
    {
        OpenScene("MainMenu");
    }
    [MenuItem(menuName + "/Level0")]
    public static void Level0()
    {
        OpenScene("Level0");
    }
    [MenuItem(menuName + "/Level1")]
    public static void Level1()
    {
        OpenScene("Level1");
    }
    [MenuItem(menuName + "/Level2")]
    public static void Level2()
    {
        OpenScene("Level2");
    }
    [MenuItem(menuName + "/Level3")]
    public static void Level3()
    {
        OpenScene("Level3");
    }
    [MenuItem(menuName + "/Test Scene")]
    public static void OpenTestScene()
    {
        OpenScene("TestScene");
    }
    [MenuItem(menuName + "/Movement Test")]
    public static void MovementTest()
    {
        OpenScene("MovementTest");
    }
    static void OpenScene(string name)
    {
        if(EditorApplication.SaveCurrentSceneIfUserWantsTo())
        {
            EditorApplication.OpenScene("Assets/Scenes/" + name + ".unity");
        }
    }
}
