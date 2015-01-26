using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;



public class MainMenuController : MonoBehaviour 
{
    [SerializeField]
    private GameObject mainMenuPanel;
    [SerializeField]
    private GameObject optionsPanel;
    [SerializeField]
    private GameObject creditsPanel;
    [SerializeField]
    private Toggle soundToggle;


    //Main Menu
    public void NewGame()
    {
        //Debug.Log("New Game"); //vestigial
        Application.LoadLevel("GlitchDesktop");
    }

    public void Options()
    {
        Debug.Log("Options");
        ClearGUI();
        optionsPanel.SetActive(true);
    }

    public void Credits()
    {
        Debug.Log("Credits");
        ClearGUI();
        creditsPanel.SetActive(true);
    }
	public void BackToMainMenu()
    {
        Debug.Log("Back to Main");
        ClearGUI();
        mainMenuPanel.SetActive(true);
    }
    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #endif
    }

    //Options
    public void ToggleSound()
    {
        Debug.Log("Sounds: "+ soundToggle.isOn);
        AudioManager.Instance.TogglesSounds(soundToggle.isOn);
    }
    void Start()
    {
        soundToggle.isOn = AudioManager.Instance.SoundsOn;
    }
    private void ClearGUI()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

}
