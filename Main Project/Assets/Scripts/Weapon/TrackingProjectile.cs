using UnityEngine;
using System.Collections;

public class TrackingProjectile : MonoBehaviour {

    private Transform target;
    private float projectileSpeed;
    private float turnSpeed;

    public float damping = 20.0f;

	// Use this for initialization
	void Start () {
	
	}

    public void Initialize(Transform _target, float _projectileSpeed, float _turnSpeed)
    {
        target = _target;
        projectileSpeed = _projectileSpeed;
        turnSpeed = _turnSpeed;
    }

	// Update is called once per frame
	void Update () {

        if (target)
        {
            Vector3 dir = target.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            //if(angle>150.0f)
            //{
            //    Debug.Log("Angle: " + angle);

            //}
            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.Euler(0, 0, angle), 15 * Time.deltaTime);
        }
        rigidbody2D.AddForce(new Vector2(transform.right.x,
           transform.right.y) * projectileSpeed, ForceMode2D.Force);
	}
}
