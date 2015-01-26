using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Projectile : MonoBehaviour 
{
    //public bool noParent { get; set; }
    public delegate void CollisionEvent(GameObject gameObject, GameObject projectile);
    public event CollisionEvent OnCollision = new CollisionEvent((GameObject gameObject, GameObject projectile) => { });

    void OnTriggerEnter2D(Collider2D other)
    {
        //if (noParent)
        {
            OnCollision(other.gameObject, gameObject);
        }
        //else
        //{
        //    Destroy(gameObject);
        //}
    }
	
}
