using UnityEngine;
using System.Collections;
using System.Collections.Generic;


enum EffectType { ShipSpeed, ShipScale, ErraticShots ,ProjectileScale }//, , , WeaponUpgrade}

public class GlitchManager : MonoBehaviour
{
    [SerializeField]
    private GameObject glitchPrefab;
    [SerializeField]
    private float initialWaitTime = 5.0f;
    [SerializeField]
    private float glitchSpawnInterval = 10.0f;
    [SerializeField]
    private float spawnIntervalRange = 2.0f;
    [SerializeField]
    private float distAwayFromLevelToSpawn = 2.0f;
    [SerializeField]
    private float glitchMoveSpeed = 10.0f;
    [SerializeField]
    private float glitchTrackDuration = 0.5f;
    [SerializeField]
    private Vector2 pitchRange = new Vector2(0.4f, 1.4f);
    [SerializeField]
    private Vector2 changeIntervalRange = new Vector2(0.1f, 0.3f);
    [SerializeField]
    private Vector2 scaleFactorRange = new Vector2(1.0f/3.0f, 3.0f);
    [SerializeField]
    private Vector2 scaleDurationRange = new Vector2(2.0f, 5.0f);
    
    [SerializeField]
    private Vector2 speedFactorRange = new Vector2(1.0f/3.0f, 3.0f);
    [SerializeField]
    private Vector2 speedDurationRange = new Vector2(2.0f, 5.0f);

    [SerializeField]
    private Vector2 accuracyFactorRange = new Vector2(-30.0f, 30.0f);
    [SerializeField]
    private Vector2 accuracyDurationRange = new Vector2(2.0f, 5.0f);

    [SerializeField]
    private Vector2 projScaleFactorRange = new Vector2(1.0f / 3.0f, 3.0f);
    [SerializeField]
    private Vector2 projScaleDurationRange = new Vector2(2.0f, 5.0f);

    private System.Array effectTypes;
    private List<EffectType> effectTypesForPlayer;
    private List<EffectType> effectTypesForEnemy;

    private Transform trans;
    private Sprite[] sprites;

    private void SpawnGlitch()
    {

        GameObject glitchClone = Instantiate(glitchPrefab, GetSpawnPosition(), glitchPrefab.transform.rotation) as GameObject;

        glitchClone.rigidbody2D.velocity = Vector2.right * -glitchMoveSpeed;
        Glitch glitch = glitchClone.GetSafeComponent<Glitch>();
        glitch.SetSprite(sprites[Random.Range(0, sprites.Length)]);
        glitch.OnCollision += GlitchCollision;
    }

    void GlitchCollision(GameObject gameObject)
    {
        //Debug.Log("Collided with " + gameObject.name);
        EffectType effectType;
        if(gameObject.layer == TagsAndLayers.PlayerShipLayer)
        {
            effectType = GetRandomEffectForPlayer();
        }
        else if(gameObject.layer == TagsAndLayers.EnemyShipLayer)
        {
            effectType = GetRandomEffectForEnemy();
        }
        else
        {
            effectType = GetRandomEffectType();
        }
        Effect effect = null;
        //effect = new ShipScaleEffect(Random.Range(scaleFactorRange.x, scaleFactorRange.y), Random.Range(scaleDurationRange.x, scaleDurationRange.y));
        GameObject effectObj = new GameObject(effectType.ToString());
        

        switch (effectType)
        {
            case EffectType.ShipScale:
                effect = effectObj.AddComponent<ShipScaleEffect>();
                ((ShipScaleEffect)effect).Init(Random.Range(scaleFactorRange.x, scaleFactorRange.y), Random.Range(scaleDurationRange.x, scaleDurationRange.y));
                break;
            case EffectType.ShipSpeed:
                effect = effectObj.AddComponent<ShipSpeedEffect>();
                ((ShipSpeedEffect)effect).Init(Random.Range(speedFactorRange.x, speedFactorRange.y), Random.Range(speedDurationRange.x, speedDurationRange.y));
                break;
            case EffectType.ErraticShots:
                effect = effectObj.AddComponent<ErraticShotsEffect>();
                ((ErraticShotsEffect)effect).Init(Random.Range(accuracyFactorRange.x, accuracyFactorRange.y), Random.Range(accuracyDurationRange.x, accuracyDurationRange.y));
                break;
            case EffectType.ProjectileScale:
                effect = effectObj.AddComponent<ProjectileScaleEffect>();
                ((ProjectileScaleEffect)effect).Init(Random.Range(projScaleFactorRange.x, projScaleFactorRange.y), Random.Range(projScaleDurationRange.x, projScaleDurationRange.y));
                break;

            
            
            //case EffectType.WeaponUpgrade:
            //    break;
            default:
                break;
        }
        if (gameObject && gameObject.GetComponent<Ship>() != null)
        {
            gameObject.GetComponent<Ship>().AddEffect(effect);
            StartCoroutine(GlitchTrack());
            GlitchTrack();
        }
    }
    private IEnumerator GlitchTrack()
    {
        AudioSource source = AudioManager.Instance.MainTrack;
        if (source != null)
        {

            //float defaultPitch = source.pitch;
            float currentTime = 0.0f;

            do
            {
                source.pitch = Random.Range(pitchRange.x, pitchRange.y);
                float waitTime = Random.Range(changeIntervalRange.x, changeIntervalRange.y);
                currentTime += waitTime;
                yield return new WaitForSeconds(waitTime);

            } while (currentTime <= glitchTrackDuration);

            source.pitch = 1.0f;
        }
        else
            yield return null;
    }
    private Vector3 GetSpawnPosition()
    {
        bool spawnUp = Random.value > 0.5f;
        RaycastHit2D hit;
        if (spawnUp)
        {
            hit = Physics2D.Raycast(trans.position, Vector2.up, 1000.0f, 1 << TagsAndLayers.EnvironmentLayer);
        }
        else
        {
            hit = Physics2D.Raycast(trans.position, Vector2.up * -1.0f, 1000.0f, 1 << TagsAndLayers.EnvironmentLayer);
        }
        float maxDist = Vector2.Distance(hit.point, trans.position);
        float distToSpawn = Random.Range(0.0f, maxDist - distAwayFromLevelToSpawn);
        if(spawnUp)
        {
            return trans.position + (Vector2.up * distToSpawn).GetVector3();
        }
        else
        {
            return trans.position - (Vector2.up * distToSpawn).GetVector3();
        }
    }

    private IEnumerator SpawnGlitchAfterInterval()
    {
        while (true)
        {
            SpawnGlitch();

            float interval = Random.Range(glitchSpawnInterval - spawnIntervalRange, glitchSpawnInterval + spawnIntervalRange);
            yield return new WaitForSeconds(interval);

        }

    }
   
    EffectType GetRandomEffectForPlayer()
    {
        int effectIndex = Random.Range(0, effectTypesForPlayer.Count);
        return effectTypesForPlayer[effectIndex];
    }
    EffectType GetRandomEffectForEnemy()
    {
        return effectTypesForEnemy[Random.Range(0, effectTypesForEnemy.Count)];
    }
    EffectType GetRandomEffectType()
    {
        return (EffectType)effectTypes.GetValue(Random.Range(0, effectTypes.Length));
    }
    private void Init()
    {
        effectTypes = System.Enum.GetValues(typeof(EffectType));
        effectTypesForPlayer = new List<EffectType>();
        effectTypesForEnemy = new List<EffectType>();

        foreach (EffectType effectType in effectTypes)
        {
            switch (effectType)
            {
                case EffectType.ShipScale:
                    effectTypesForPlayer.Add(effectType);
                    break;

                case EffectType.ShipSpeed:
                    effectTypesForPlayer.Add(effectType);
                    effectTypesForEnemy.Add(effectType);
                    break;


                case EffectType.ErraticShots:
                    effectTypesForPlayer.Add(effectType);
                    effectTypesForEnemy.Add(effectType);
                    break;

                case EffectType.ProjectileScale:
                    effectTypesForPlayer.Add(effectType);
                    effectTypesForEnemy.Add(effectType);
                    break;
                
               
                //case EffectType.WeaponUpgrade:
                //    effectTypesForPlayer.Add(effectType);
                //    break;
                default:
                    break;
            }
        }
        trans = transform;
        sprites = Resources.LoadAll<Sprite>("Sprites");
    }
    private IEnumerator Start()
    {
        Init();
        yield return new WaitForSeconds(initialWaitTime);
        StartCoroutine(SpawnGlitchAfterInterval());
    }

}
