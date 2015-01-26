using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMove : MonoBehaviour 
{
    [SerializeField]
    private float defaultMoveForce = 40.0f;
    public float DefaultMoveForce
    {
        get { return defaultMoveForce; }
    }
    [SerializeField]
    private float defaultMaxSpeed = 40.0f;
    public float DefaultMaxSpeed
    {
        get { return defaultMaxSpeed; }
    }
    
    private float maxSpeed=40.0f;
    public float MaxSpeed
    {
        get { return maxSpeed; }
        set { maxSpeed = value; }
    }
    [SerializeField]
    private float brakeTime = 1.75f;

    private float moveForce;
    public float MoveForce
    {
        get { return moveForce; }
        set { moveForce = value; }
    }

    private Transform trans;
    private Rigidbody2D rbody;

    private float maxSpeedSq;
    private float currentVelX;
    private float currentVelY;

    
    public void Move(Vector2 direction)
    {

        //Debug.Log("speed " + rbody.velocity.magnitude);
        //Debug.Log(direction);
        if (direction == Vector2.zero)
        {
            Brake();
        }
        else
        {
            rbody.velocity = Vector2.zero;
            rbody.AddRelativeForce(direction * moveForce, ForceMode2D.Impulse);

            Vector2 currentVel = rbody.velocity;
            if (currentVel.sqrMagnitude > maxSpeedSq)
            {
                currentVel.Normalize();
                currentVel *= maxSpeed;
            }
            rbody.velocity = currentVel;
        }
    }
    public void Brake()
    {
        float velX = Mathf.SmoothDamp(rbody.velocity.x, 0.0f, ref currentVelX, brakeTime );
        float velY = Mathf.SmoothDamp(rbody.velocity.y, 0.0f, ref currentVelY, brakeTime );
        rbody.velocity = new Vector2(velX, velY);
    }
    public  void ModifySpeed(float multiplier)
    {
        float newSpeed = moveForce * multiplier;
        if(newSpeed < defaultMoveForce)
        {
            if(defaultMoveForce/newSpeed > 3.0f)
            {
                moveForce = defaultMoveForce / 3.0f;
                maxSpeed = DefaultMaxSpeed / 3.0f;
            }
            else
            {
                moveForce = newSpeed;
                maxSpeed *= multiplier;
            }
        }
        else
        {
            if(newSpeed /defaultMoveForce>3.0f)
            {
                moveForce = defaultMoveForce * 3.0f;
                maxSpeed = DefaultMaxSpeed * 3.0f;
            }
            else
            {
                moveForce = newSpeed;
                maxSpeed *= multiplier;
            }
        }


    }
    public  void SetDefaultSpeed()
    {
        maxSpeed = defaultMoveForce;
        maxSpeed = DefaultMaxSpeed;
    }
	private void Awake()
    {
        trans = transform;
        rbody = rigidbody2D;
    }
    private void Start()
    {
        maxSpeed = defaultMaxSpeed;
        maxSpeedSq = maxSpeed * maxSpeed;
        moveForce = defaultMoveForce;
    }
}
