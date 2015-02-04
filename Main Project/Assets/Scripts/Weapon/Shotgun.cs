using UnityEngine;
using System.Collections;
using System;

public class Shotgun : Weapon {

    const uint BASE_SHOT_COUNT = 5;
    [SerializeField]
    private float shotSpread = 20.0f;

	// Use this for initialization
	void Start () {
	
	}

    public void Initialize(float _shotSpread, float _shotSpeed, int _damage)
    {
        shotSpread = _shotSpread;
        projectileSpeed = _shotSpeed;
        damage = _damage;
    }

	// Update is called once per frame
    
	void Update () {

        currentTimer += Time.deltaTime;
        //Debug.Log(privateTimer);
	}

    public override void FireWeapon(float accuracy)
    {
        if (currentTimer >= reloadTimer)
        {
            AudioManager.Instance.PlayOneShotSound(Sound.Shotgun,0.15f);
            float angle = -shotSpread * 0.5f;
            for (int i = 0; i < BASE_SHOT_COUNT + WeaponLevel-1; i++)
            {
                GameObject bullet = (GameObject)GameObject.Instantiate(ProjectilePrefab,
                    transform.parent.position, transform.parent.rotation);
                bullet.GetComponent<Projectile>().OnCollision += Collision;
                //Debug.Log("Shotgun spread " + shotSpread);
                //Debug.Log("Shotgun acc " + accuracy);
                Vector3 direction = //ShotDirection;
                    //Quaternion.Euler(0.0f, 0.0f,(UnityEngine.Random.Range(-shotSpread - accuracy, shotSpread + accuracy))) * ShotDirection;
                    Quaternion.Euler(0.0f, 0.0f,angle) * ShotDirection;
                angle += shotSpread * 0.25f;
                //Debug.Log("Direction: " + direction.ToString());
                bullet.rigidbody2D.AddForce(new Vector2(direction.x,
                    direction.y) * ProjectileSpeed, ForceMode2D.Impulse);
            }
            currentTimer = 0.0f;
        }
    }

    void Collision(GameObject other, GameObject projectile)
    {

        if(other.layer == EnemyLayer)
        {
            //Debug.Log(damage + " Damage to " + other.name);
            WeakPoint weakPoint = other.GetComponent<WeakPoint>();
            if (weakPoint)
            {
                weakPoint.TakeDamage(damage);
            }
            else if (other.GetComponent<Ship>())
            {
                //Debug.Log("in'");

                other.GetComponent<Ship>().ApplyDamage(damage);
            }
            //Debug.Log("s'");
            Destroy(projectile);
            //projectile.SetActive(false);
        }
        //else if (EnemyLayer == TagsAndLayers.BossLayer && other.layer == TagsAndLayers.BossLayer)
        //{
        //    other.GetComponent<WeakPoint>().TakeDamage(damage);
        //    Destroy(projectile);
        //    //projectile.SetActive(false);
        //}
        else if(other.layer == TagsAndLayers.EnvironmentLayer)
        {
            Destroy(projectile);
        }
    }

}
