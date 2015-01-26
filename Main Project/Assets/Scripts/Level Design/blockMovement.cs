using UnityEngine;
using System.Collections;


public enum MovementAxis { X_AXIS, Y_AXIS}
public class blockMovement : MonoBehaviour 
{

    public MovementAxis axis;
    public float movementSpeedFactor = 1.0f;
    private bool fwdDirection = true;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        switch (axis)
        {
            case MovementAxis.X_AXIS:
                transform.Translate(new Vector3(0.8f + 1 * movementSpeedFactor,0,0));
                break;
            case MovementAxis.Y_AXIS:
                if (transform.position.y > 200)
                {
                    fwdDirection = false;
                }
                if (transform.position.y < 0)
                {
                    fwdDirection = true;
                }

                if (fwdDirection)
                {
                    transform.Translate(new Vector3(0, 1 * movementSpeedFactor, 0));
                }
                else
                {
                    transform.Translate(new Vector3(0, -1 * movementSpeedFactor, 0));
                }
                

                break;
            default:
                break;
        }
	}
}
