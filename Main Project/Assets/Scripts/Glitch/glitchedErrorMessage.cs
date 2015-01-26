using UnityEngine;
using System.Collections;

public class glitchedErrorMessage : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        //transform.position = Input.mousePosition;
        Vector3 temp = Input.mousePosition;
        temp.z = transform.position.z - Camera.main.transform.position.z;
        transform.position = Camera.main.ScreenToWorldPoint(temp);
	}
}
