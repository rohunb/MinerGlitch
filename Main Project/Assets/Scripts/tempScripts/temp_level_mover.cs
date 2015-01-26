using UnityEngine;
using System.Collections;

public class temp_level_mover : MonoBehaviour {

    [SerializeField]
    public float speedFactor = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.Translate(new Vector2(-0.8f, 0)* speedFactor);
	}
}
