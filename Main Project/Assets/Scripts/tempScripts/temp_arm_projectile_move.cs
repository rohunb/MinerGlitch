using UnityEngine;
using System.Collections;

public class temp_arm_projectile_move : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        rigidbody2D.AddForce(new Vector2(-10, 0));
	}
}
