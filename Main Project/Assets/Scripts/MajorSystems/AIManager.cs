using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AIManager : Singleton<AIManager> {
    
    public float InitialWaitTime = 1.0f;

    [SerializeField]
    private GameObject ShipPrefab;
    [SerializeField]
     private GameObject playerShip;
    public UnityEngine.GameObject PlayerShip
    {
        get { return playerShip; }
    }

    [SerializeField]
    private Vector3 direction = new Vector3(0.0f, 0.0f, 0.0f);
    // [SerializeField]
    //private Vector3 minEdge = new Vector3(0.0f, -5.0f, 0.0f);
    // [SerializeField]
    //private Vector3 maxEdge = new Vector3(0.0f, 5.0f, 0.0f);
    
    public float speed = 1.0f;

    public List<GameObject> Enemies;

    public SpawnInfo currentSpawnData;

    private int spawnSequenceIndex = 0;

    private float timePassedWithSpawnData = 0.0f;
    private float totalTimeWithSpawnData = 0.0f;

    private int numberSpawned = 0;

    private int loopsCompleted = 0;
    private bool stopSpawning = false;

    private LevelInfo currentLevelInfo;
    public LevelInfo CurrentLevelInfo
    {
        get { return currentLevelInfo; }
        set { currentLevelInfo = value; }
    }
	// Use this for initialization
	private IEnumerator Start () {
        currentLevelInfo = LevelConfig.GetLevelInfo(GameScene.MovementTest);

        Physics2D.IgnoreLayerCollision(TagsAndLayers.EnemyShipLayer, TagsAndLayers.EnemyShipLayer, true);
        Physics2D.IgnoreLayerCollision(TagsAndLayers.SpawnLayer, TagsAndLayers.EnvironmentLayer, true);

        //Physics2D.IgnoreLayerCollision(TagsAndLayers.SpawnLayer, TagsAndLayers.EnemyShipLayer, true);
        //Physics2D.IgnoreLayerCollision(TagsAndLayers.SpawnLayer, TagsAndLayers.PlayerShipLayer, true);
        //Physics2D.IgnoreLayerCollision(TagsAndLayers.SpawnLayer, TagsAndLayers.BossLayer, true);

        if (currentSpawnData == null)
        {
            Debug.LogError("No Spawn Data for AI Manager, no spawning will take place");
            yield return null;
        }

        direction = currentSpawnData.transform.right;
        speed = currentSpawnData.managerSpeed;
        stopSpawning = currentSpawnData.dontSpawn;

        if (currentSpawnData.dontSpawn == false)
        {
            yield return null;
           // yield return new WaitForSeconds(InitialWaitTime);
           // StartCoroutine("SpawnShip");
        }
        else
        {
            Enemies = new List<GameObject>(GameObject.FindObjectsOfType<AIShip>().Select(s=>s.gameObject));
            yield return null;
        }
	}
    	
    public IEnumerator SpawnShip()
    {
        while (true)
        {
            if (stopSpawning == false)
            {
                GameObject newShip = GetPrefabClone();


                //while using path manager, there is no need for this
                ConfigureAI(newShip);
                Enemies.Add(newShip);

                stopSpawning = CheckStopSpawning();
            }
            

            yield return new WaitForSeconds(currentSpawnData.spawnInterval);

            //yield return null;
        }
    }

    /// <summary>
    /// Finds the prefab to instantiate and positions and rotates accordingly
    /// </summary>
    /// <returns>The instantiated prefab</returns>
    private GameObject GetPrefabClone()
    {
        int indexToUse = spawnSequenceIndex;
       
        if(currentSpawnData.randomSequence)
            indexToUse = Random.Range(0, currentSpawnData.spawnSequence.Count);
        
        GameObject retVal = (GameObject)GameObject.Instantiate(
            currentSpawnData.spawnSequence[indexToUse], transform.position, 
            Quaternion.RotateTowards(Quaternion.identity, Quaternion.Euler(currentSpawnData.releaseDirection), 10000));
            
        Debug.Log("indexToUse: " + indexToUse);
       
        ++spawnSequenceIndex;
        ++numberSpawned;

        //if the index exceeds the number of objects
        if (spawnSequenceIndex >= currentSpawnData.spawnSequence.Count)
        {
            //if we are looping
            if (currentSpawnData.loopSequence)
            {
                loopsCompleted++;
                spawnSequenceIndex = 0;
            }
        }

        return retVal;
    }

    private bool CheckStopSpawning()
    {
        if (numberSpawned >= currentSpawnData.numberToSpawn && currentSpawnData.numberToSpawn > 0)
            return true;

        if (loopsCompleted >= currentSpawnData.numberOfLoops && currentSpawnData.numberOfLoops > 0)
            return true;

        if (totalTimeWithSpawnData >= currentSpawnData.duration && currentSpawnData.duration > 0)
            return true;

        if (spawnSequenceIndex >= currentSpawnData.spawnSequence.Count && currentSpawnData.loopSequence == false
                && currentSpawnData.randomSequence == false)
            return true;

        return false;
    }

    private void ConfigureAI(GameObject ship)
    {
        AIShip aiShipComponent = ship.GetComponent<AIShip>();
        EnemyConfig config = ship.GetComponent<EnemyConfig>();

        //if we can't find required components, debug and break
        if (aiShipComponent == null || config == null)
        {
            Debug.Log("AI Ship Configuration Components Missing");
            return;
        }
        //Add basic AI attack because bosses aren't randomly generated
        //Create before weapon, because weapon equips onto attack
        ship.AddComponent<AIAttack>().SetOwningShip(aiShipComponent);

        ConfigureWeapon(config, ship, aiShipComponent);

        ConfigureMovement(config, ship, aiShipComponent);
    }

    private void ConfigureWeapon(EnemyConfig config, GameObject ship, AIShip shipComponent)
    {
        if (config.UseNoWeapons())
            return;

        //Invalid option
        Weapon.WeaponType weaponToLoad = Weapon.WeaponType.WeaponCount;

        bool validOption = false;
        int maxIters = 10;

        while (validOption == false)
        {
            int ranVal = Random.Range(1, (int)Weapon.WeaponType.WeaponCount);
            validOption = config.ValidWeaponOption((Weapon.WeaponType)ranVal);

            if (validOption)
            {
                weaponToLoad = (Weapon.WeaponType)ranVal;
                break;
            }
            if (maxIters <= 0)
            {
                if (config.UseOrb())
                    weaponToLoad = Weapon.WeaponType.Orb;
                else if (config.UseBeam())
                    weaponToLoad = Weapon.WeaponType.Beam;
                else if (config.UseShotgun())
                    weaponToLoad = Weapon.WeaponType.Shotgun;
                else if (config.UseBomb())
                    weaponToLoad = Weapon.WeaponType.Bomb;
                break;
            }
            maxIters--;
        }
        Weapon weapon = null;

        switch (weaponToLoad)
        {
            case Weapon.WeaponType.Orb:
                weapon = ship.GetComponentsInChildren<Orb>(true)[0];

                break;
            case Weapon.WeaponType.Beam:
                weapon = ship.GetComponentsInChildren<Beam>(true)[0];

                break;
            case Weapon.WeaponType.Shotgun:
                weapon = ship.GetComponentsInChildren<Shotgun>(true)[0];

                break;
            case Weapon.WeaponType.Bomb:
                weapon = ship.GetComponentsInChildren<Bomb>(true)[0];

                break;
            case Weapon.WeaponType.WeaponCount:
                break;
            default:
                break;
        }

        weapon.gameObject.SetActive(true);
        weapon.EnemyLayer = TagsAndLayers.PlayerShipLayer;
        //Indicate which weapon to use
        shipComponent.SetEquippedWeapon(weaponToLoad - 1);

    }

    private void ConfigureMovement(EnemyConfig config, GameObject ship, AIShip aiShip)
    {
        //At the moment, there is no implementation for stationaries (why would there be)
        if (config.UseStationaryMove())
            return;

        //Invalid option
        AIMove.MoveTypes moveScriptToLoad = AIMove.MoveTypes.Count;

        bool validOption = false;
        int maxIters = 10;

        while (validOption == false)
        {
            int ranVal = Random.Range(0, (int)AIMove.MoveTypes.Count);
            Debug.Log(((AIMove.MoveTypes)ranVal).ToString());
  Debug.Log("Ran Val: " + ranVal);
            validOption = config.ValidMoveOption((AIMove.MoveTypes)ranVal);

            if (validOption)
            {
                Debug.Log("Ran Val As Enum: " + (AIMove.MoveTypes)ranVal);
                moveScriptToLoad = (AIMove.MoveTypes)ranVal;
                break;
            }

            if(maxIters <=0)
                --maxIters;
        }

        AIMove movement = null;

        switch (moveScriptToLoad)
        {
            case AIMove.MoveTypes.Stationary:
                //Do nothing in this case
                Debug.Log("Stationary selected");
                break;
            case AIMove.MoveTypes.Linear:
                {
                    SinMove sinMove = ship.AddComponent<SinMove>();
                    Debug.Log("Linear selected");
                    if (sinMove)
                    {
                        sinMove.ApplySinHorizontal = false;
                        sinMove.ApplySinVertical = false;
                        sinMove.MoveDirection = transform.rotation * Vector3.right;
                        movement = sinMove;
                    }
                }

                break;
            case AIMove.MoveTypes.Sin:
                {
                    SinMove sinMove = ship.AddComponent<SinMove>();

                    if (sinMove)
                    {
                        //0 both false, 1 horizontal, 2 vertical, 3 both
                        int sinDirection = 0;
                        //Apply random factor if sin is allowed to move on horizontal and vertical
                        if (sinMove.ApplySinVertical && sinMove.ApplySinHorizontal)
                            sinDirection = Random.Range(1, 4);
                        else if (sinMove.ApplySinHorizontal && !sinMove.ApplySinVertical)
                            sinDirection = 1;
                        else if (sinMove.ApplySinVertical && !sinMove.ApplySinHorizontal)
                            sinDirection = 2;

                        sinMove.ApplySinHorizontal =
                            (sinDirection == 1 || sinDirection == 3) ? true : false;
                        sinMove.ApplySinVertical =
                            (sinDirection == 2 || sinDirection == 3) ? true : false;

                        sinMove.MoveDirection = ship.transform.rotation * Vector3.right;
                        sinMove.amplitude = Random.Range(config.minSinAmplitude, config.maxSinAmplitude);
                        sinMove.frequency = Random.Range(config.minSinFrequency, config.maxSinFrequency);
                        movement = sinMove;
                    }
                }
                break;
            case AIMove.MoveTypes.Rotation:
                {
                    RotationMove rotMove = ship.AddComponent<RotationMove>();

                    if (rotMove)
                    {
                        rotMove.MoveDirection = ship.transform.rotation * Vector3.right;
                        rotMove.minAngle = config.minRotationAngle;
                        rotMove.maxAngle = config.maxRotationAngle;
                        movement = rotMove;
                    }
                }
                break;
            case AIMove.MoveTypes.Tracking:
                Debug.LogError("Configure Movement: Tracking Not Implemented");
                break;
            case AIMove.MoveTypes.Count:
                //do nothing here homeboys
                break;
            default:
                break;
        }



       // if (movement != null)
       // {
            movement.SetOwningShip(aiShip);
            //Set move direction
            movement.MoveDirection = config.moveDirection;
       // }
    }

    public void RegisterNewAI(GameObject aiShip)
    {
        Enemies.Add(aiShip);
    }

	// Update is called once per frame
	void Update () {

        if (stopSpawning == false)
        {
            bool spawnAnother = (numberSpawned == 0 && timePassedWithSpawnData >= currentSpawnData.initialInterval) ||
                (timePassedWithSpawnData >= currentSpawnData.spawnInterval);

            if (spawnAnother) 
            {
                Debug.Log("Spawn");
                GameObject newShip = GetPrefabClone();

                newShip.rigidbody2D.velocity = currentSpawnData.releaseDirection * currentSpawnData.releaseSpeed;
                
                Enemies.Add(newShip);

                timePassedWithSpawnData = 0.0f;
            }

            timePassedWithSpawnData += Time.deltaTime;
            totalTimeWithSpawnData += Time.deltaTime;

            stopSpawning = CheckStopSpawning();
        }

        MoveManager();

	}

    public void KillShip(GameObject ai_ship)
    {
        if(Enemies.Remove(ai_ship))
            Destroy(ai_ship);
    }

    private void MoveManager()
    {
        if (currentSpawnData.SinHorizontal == false && currentSpawnData.SinVertical == false)
        {
            Vector3 displacement = direction * speed;
            //transform.position += displacement;
            rigidbody2D.velocity = (displacement);
        }
        else
        {
            float sinResult = currentSpawnData.Amplitude * Mathf.Sin(currentSpawnData.Frequency * Time.realtimeSinceStartup);

            float verticalSin = 1.0f;
            float horizontalSin = 1.0f;

            if (currentSpawnData.SinHorizontal)
                horizontalSin = sinResult;

            if (currentSpawnData.SinVertical)
                verticalSin = sinResult;

            rigidbody2D.velocity = new Vector3(direction.x * speed * verticalSin,
                direction.y * speed * horizontalSin, 0.0f);
        }
        
        
        //Vector3 position = transform.position;
        //if (position.x > maxEdge.x)
        //{
        //    direction.x *= -1.0f;
        //    transform.SetPositionX(maxEdge.x);
        //}
        //else if (position.x < minEdge.x)
        //{
        //    direction.x *= -1.0f;
        //    transform.SetPositionX(minEdge.x);
        //}

        //if (position.y > maxEdge.y)
        //{
        //    direction.y *= -1.0f;
        //    transform.SetPositionY(maxEdge.y);
        //}
        //else if (position.y < minEdge.y)
        //{
        //    direction.y *= -1.0f;
        //    transform.SetPositionY(minEdge.y);
        //}
    }

    void SetNewSpawnData(SpawnInfo spawnData)
    {
        currentSpawnData = spawnData;

        timePassedWithSpawnData = 0.0f;
        loopsCompleted = 0;
        numberSpawned = 0;
        spawnSequenceIndex = 0;
        transform.position = spawnData.managerPosition;
        totalTimeWithSpawnData = 0.0f;

        stopSpawning = spawnData.dontSpawn;

        if (spawnData.spawnSequence.Count == 0)
            stopSpawning = true;

        speed = spawnData.managerSpeed;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == TagsAndLayers.SpawnBeaconTag)
        {
            SpawnInfo info = collider.gameObject.GetComponentInChildren<SpawnInfo>();
            SetNewSpawnData(info);
            direction = info.gameObject.transform.right;
        }
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.DrawLine(new Vector3(-1000.0f, maxEdge.y, 0.0f), new Vector3(1000.0f, maxEdge.y, 0.0f));
    //    Gizmos.DrawLine(new Vector3(-1000.0f, minEdge.y, 0.0f), new Vector3(1000.0f, minEdge.y, 0.0f));
    //    Gizmos.DrawLine(new Vector3(maxEdge.x, -1000.0f, 0.0f), new Vector3(maxEdge.x, 1000.0f, 0.0f));
    //    Gizmos.DrawLine(new Vector3(minEdge.x, -1000.0f, 0.0f), new Vector3(minEdge.x, 1000.0f, 0.0f));
    //}
}
