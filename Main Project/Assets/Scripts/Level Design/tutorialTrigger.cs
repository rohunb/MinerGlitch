using UnityEngine;
using System.Collections;

public class tutorialTrigger : MonoBehaviour 
{
    public temp_level_mover levelScript;

    public string uiMessage;
    public int UI_trigger_Num;
    public GameObject UI_1;
    public GameObject UI_2;
    public GameObject UI_3;
    public GameObject UI_4;

    void OnTriggerEnter2D(Collider2D c)
    {
        
        Debug.Log(uiMessage);
        Time.timeScale = 0;
        levelScript.speedFactor = 0;
        switch (UI_trigger_Num)
        {
            case 1:
                UI_1.SetActive(true);
                break;
            case 2:
                UI_2.SetActive(true);
                break;
            case 3:
                UI_3.SetActive(true);
                break;
            case 4:
                UI_4.SetActive(true);
                break;
            default:
                break;
        }
        gameObject.SetActive(false);
    }

    void OnTriggerExit2D(Collider2D c)
    {
        Debug.Log("got here");
    }

    public void HideUI()
    {
        Time.timeScale = 1;
        levelScript.speedFactor = 0.5f;
        UI_1.SetActive(false);
        UI_2.SetActive(false);
        UI_3.SetActive(false);
        UI_4.SetActive(false);


    }
}
