using UnityEngine;
using System.Collections;

public class Beam : Weapon
{

    [SerializeField]
    int beamLength = 30;

    Vector3 direction;
    float lineLength;
    float accuracy = 1.0f;

    float duration = 0.250f;
    float timePassed = 0.0f;

    bool coolDown = false;

    GameObject currLaser;

    public void Initialize(int _beamLength)
    {
        beamLength = _beamLength;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (coolDown)
        {
            timePassed += Time.deltaTime;
           
            if (timePassed > duration)
            {
                timePassed = 0;
                coolDown = false;
            }

        }
    }

    public override void FireWeapon(float accuracy)
    {
        if (!coolDown)
        {
            AudioManager.Instance.PlayOneShotSound(Sound.LaserBeam,0.5f);

            coolDown = true;

            float acc = Random.Range(-accuracy, accuracy);
            direction = Quaternion.Euler(0.0f, 0.0f,
               (acc)) * -ShotDirection;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), 360.0f);

            currLaser = (GameObject)GameObject.Instantiate(ProjectilePrefab,
                      transform.position, rotation);

            currLaser.GetComponentInChildren<ParticlePlayground.PlaygroundParticlesC>().lifetime = duration * 0.5f;

            Debug.DrawRay(transform.position, -direction*100.0f);
            currLaser.GetComponentInChildren<PlaygroundPresetLaserC>().laserMaxDistance = beamLength;
            RaycastHit2D info = Physics2D.Raycast(transform.position, -direction, beamLength, 1<<EnemyLayer);// | 1<<TagsAndLayers.BossLayer);
            if (info)
            {
                //if (info.distance == 0)
                //    info.distance = beamLength;
                if(info.collider.gameObject.layer == EnemyLayer)
                {
                    GameObject colObj = info.collider.gameObject;
                    WeakPoint weakPoint = colObj.GetComponent<WeakPoint>();
                    if (weakPoint)
                    {
                        weakPoint.TakeDamage(damage);
                    }
                    else
                    {
                        colObj.GetComponent<Ship>().ApplyDamage(damage);
                    }
                currLaser.GetComponentInChildren<PlaygroundPresetLaserC>().laserMaxDistance = info.distance;

                }
                //else if (EnemyLayer == TagsAndLayers.BossLayer && info.collider.gameObject.layer == TagsAndLayers.BossLayer)
                //{
                //   info.collider.gameObject.GetComponent<WeakPoint>().TakeDamage(damage);
                //currLaser.GetComponentInChildren<PlaygroundPresetLaserC>().laserMaxDistance = info.distance;

                //}
                Debug.Log(info.collider.name);
                //Modify laser size
            }
        }

    }
}
