using UnityEngine;
using System.Collections;

public class AIMove : MonoBehaviour {

    public enum MoveTypes { Stationary, Linear, Sin, Rotation, Tracking, Count };

    public enum MoveFlags { 
        Stationary = 0,
        Linear = 1, 
        Sin = 1 << 1, 
        Rotation = 1 << 2, 
        Tracking = 1 << 3,
        All = 15 };

    [SerializeField]
    public Vector3 MoveDirection = new Vector3(1.0f, 1.0f, 0.0f);

    protected AIShip owningShip;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetOwningShip(AIShip newOwner)
    {
        owningShip = newOwner;
    }


}
