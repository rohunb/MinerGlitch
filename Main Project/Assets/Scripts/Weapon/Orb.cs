using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Orb : Weapon {

    const int BASE_SHOT_COUNT = 1;

    public float missileTurnSpeed;
    public Transform[] targets;
    
    List<GameObject> enemyList = new List<GameObject>();
	// Use this for initialization
	void Start () {
	
	}

    public void Initialize(float _missileTurnSpeed, float _shotSpeed, int _damage)
    {
        missileTurnSpeed = _missileTurnSpeed;
        projectileSpeed = _shotSpeed;
        damage = _damage;
    }

	// Update is called once per frame
	void Update () {

        currentTimer += Time.deltaTime;
	}

    private void AcquireTargets(int numShots)
    {
        enemyList.Clear();
        
        if (EnemyLayer == TagsAndLayers.PlayerShipLayer)
        {
            targets = new Transform[numShots];
            for (int i = 0; i < numShots; i++)
            {
                targets[i] = AIManager.Instance.PlayerShip.transform;
            }
        }
        else if (EnemyLayer == TagsAndLayers.EnemyShipLayer)
        {
            if (AIManager.Instance.Enemies.Count > 0)
            {
                enemyList = AIManager.Instance.Enemies;
            }
            
            targets = new Transform[numShots];

            if (enemyList.Count == 0)
            {
                for (int i = 0; i < numShots; i++)
                {
                    targets[i] = null;
                    //return;
                }
            }


            //Sort the list based on Magnitude Size
            enemyList = enemyList.OrderBy(s => (s.transform.position - transform.position).magnitude).ToList();

            int targetIndex = 0;

            for (int i = 0; i < enemyList.Count; i++)
            {
                if (targetIndex >= numShots)
                    break;

                Transform enemTrans = enemyList[i].transform;
                Transform parTrans = transform.parent.transform;
                //Check for equality
                if (enemTrans != parTrans)
                {
                    //Check if enemy is in front of ship
                    if (parTrans.right.x < 0.0f && enemTrans.position.x < parTrans.position.x ||
                        parTrans.right.x > 0.0f && enemTrans.position.x > parTrans.position.x)
                    {
                        targets[targetIndex] = enemyList[i].transform;
                        targetIndex++;
                    }
                }
            }
        }
    }

    public override void FireWeapon(float accuracy)
    {
        //Debug.Log("orb fire call");
        if (currentTimer >= reloadTimer)
        {
            AudioManager.Instance.PlayOneShotSound(Sound.Orb, 0.5f);
            int shotCount = BASE_SHOT_COUNT + WeaponLevel-1;

            AcquireTargets(shotCount);

            for (int i = 0; i < shotCount; i++)
            {
                GameObject bullet = (GameObject)GameObject.Instantiate(ProjectilePrefab,
                    transform.parent.position, transform.parent.rotation);

                Vector3 direction = Quaternion.Euler(0.0f, 0.0f,
                    (Random.Range(-accuracy, accuracy))) * ShotDirection;

                bullet.rigidbody2D.velocity = ShotDirection * ProjectileSpeed;

                bullet.AddComponent<TrackingProjectile>().Initialize(targets[i], ProjectileSpeed, missileTurnSpeed);
                bullet.GetComponent<Projectile>().OnCollision += OnCollision;
            }

            currentTimer = 0.0f;
        }
    }

    public override void OnCollision(GameObject other, GameObject projectile)
    {
        //Debug.Log("destroy part");
        //Check tags

        if (other.layer == TagsAndLayers.EnvironmentLayer)
        {
            Destroy(projectile);
            //projectile.SetActive(false);
        }
        if (other.layer == EnemyLayer)
        {
            //Debug.Log(damage + " Damage to " + other.name);
            WeakPoint weakPoint = other.GetComponent<WeakPoint>();
            if(weakPoint)
            {
                weakPoint.TakeDamage(damage);
            }
            else
            {
                Ship ship = other.GetComponent<Ship>();
                if(ship)
                {
                    ship.ApplyDamage(damage);
                }
            }
           
            Destroy(projectile);
            //projectile.SetActive(false);
        }
    }
}
