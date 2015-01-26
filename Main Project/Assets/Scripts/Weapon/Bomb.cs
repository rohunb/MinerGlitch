using UnityEngine;
using System.Collections;

public class Bomb : Weapon {

    public float explosionRadius = 5.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public override void FireWeapon(float accuracy)
    {
        GameObject explosion = (GameObject)Instantiate(ProjectilePrefab, transform.position, transform.rotation);

        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (var collider in collisions)
        {
            OnCollision(collider.gameObject, explosion);
        }

        AIManager.Instance.KillShip(transform.parent.gameObject);
    }

    public override void OnCollision(GameObject gameObject, GameObject projectile)
    {
        if (gameObject.layer == EnemyLayer)
        {
            gameObject.GetComponent<Ship>().ApplyDamage(damage); 
        }
    }
}
