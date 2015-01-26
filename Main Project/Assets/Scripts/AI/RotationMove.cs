using UnityEngine;
using System.Collections;

public class RotationMove : AIMove {

    private float rotation = 1.0f;

    public float minAngle = -20.0f;
    public float maxAngle = 20.0f;

    private bool invert = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
       
        if (invert)
            rotation -= owningShip.RotationSpeed;
        else
            rotation += owningShip.RotationSpeed;

        if (rotation < minAngle)
        {
            rotation = minAngle;
            invert = false;
        }
        else if (rotation > maxAngle)
        {
            rotation = maxAngle;
            invert = true;
        }

        transform.rotation = Quaternion.Euler(0.0f, 0.0f, -rotation);
	}
}
