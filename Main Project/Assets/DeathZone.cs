using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeathZone : MonoBehaviour 
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == TagsAndLayers.SpawnLayer)
            return;

        if (other.gameObject.layer == TagsAndLayers.EnemyShipLayer)
        {
            AIManager.Instance.KillShip(other.gameObject);
        }
        else
        { Destroy(other.gameObject); }
    }
	
}
