using UnityEngine;
using System.Collections;

public class Debris_Rotation : MonoBehaviour 
{
    public float rotationfactor = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.Rotate(new Vector3(0, 0, 1.0f * rotationfactor), Space.Self);
	}
}
