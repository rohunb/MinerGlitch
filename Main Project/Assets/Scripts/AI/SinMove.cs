using UnityEngine;
using System.Collections;

public class SinMove : AIMove {

    public float amplitude = 1.0f;
    public float frequency = 1.0f;
    public float phase = 0.0f;

    public bool ApplySinVertical = false;
    public bool ApplySinHorizontal = true;

    private float elapsedTime = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        elapsedTime += Time.deltaTime;
        if (elapsedTime > 36000.0f)
            elapsedTime = 0.0f;

        Rigidbody2D rigid = gameObject.GetComponent<Rigidbody2D>();

        if (!rigid)
            return;

        float sinResult = amplitude * Mathf.Sin(frequency * elapsedTime) + phase;

        float verticalSin = 1.0f;
        float horizontalSin = 1.0f;

        if (ApplySinHorizontal)
            horizontalSin = sinResult;

        if (ApplySinVertical)
            verticalSin = sinResult;

        rigid.velocity = new Vector3(MoveDirection.x * owningShip.MoveSpeed.x * verticalSin,
            owningShip.MoveSpeed.y * MoveDirection.y * horizontalSin, 0.0f);
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Layer: " + collision.gameObject.layer);
        if (collision.gameObject.layer == TagsAndLayers.EnvironmentLayer)
        {
            Debug.Log("Assign Velocity");
            Vector2 oldVel = rigidbody2D.velocity;
            rigidbody2D.AddForce(new Vector2(oldVel.x, -oldVel.y * 200.0f), ForceMode2D.Impulse);

        }
    }
}
