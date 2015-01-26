using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeakPoint : MonoBehaviour 
{
    public delegate void TakeDamageEvent(int damage);
    public event TakeDamageEvent OnTakeDamage = new TakeDamageEvent((int damage) => { });

    //void OnTriggerEnter2D(Collider2D other)
    //{

    //}

    public void TakeDamage(int damage)
    {
        OnTakeDamage(damage);
    }
	
}
