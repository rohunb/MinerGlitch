using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WinScreenController : MonoBehaviour 
{
    [SerializeField]
    GameObject yayButton;
    [SerializeField]
    SpriteRenderer winScreenSprite;
    [SerializeField]
    Material glitchMat;
    [SerializeField]
    Material bossGlitch;
    [SerializeField]
    GameObject armin;
    [SerializeField]
    SpriteRenderer arminRenderer;
    [SerializeField]
    GameObject arminGlitch;
    public GameObject tobeCont;

    public float transitionDelay = 4.0f;

    public void Yay()
    {
        Debug.Log("Yay");
        yayButton.SetActive(false);
        winScreenSprite.material = glitchMat;
        StartCoroutine(TransitionToArmin());
    }
    IEnumerator TransitionToArmin()
    {
        Debug.Log("transition");

        yield return new WaitForSeconds(transitionDelay);
        winScreenSprite.gameObject.SetActive(false);
        armin.SetActive(true);
        yield return new WaitForSeconds(transitionDelay);
        //armin.SetActive(false);
        //arminGlitch.SetActive(true);
        arminRenderer.material = bossGlitch;
        yield return new WaitForSeconds(transitionDelay);
        tobeCont.SetActive(true);
        yield return new WaitForSeconds(transitionDelay);
        Application.LoadLevel(GameScene.MainMenu.ToString());
    }

	
}
