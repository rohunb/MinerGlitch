using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class activateboss : MonoBehaviour 
{
    public GameObject boss;
    public temp_level_mover levelMOver;

    void OnTriggerEnter2D(Collider2D other)
    {
        GameController.Instance.LevelComplete();
    }
	
}
