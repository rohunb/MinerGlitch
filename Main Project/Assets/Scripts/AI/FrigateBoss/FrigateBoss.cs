using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class FrigateBoss : MonoBehaviour
{
    [SerializeField]
    Material glitchMat;
    [SerializeField]
    Material fighterGlitchMat;
    [SerializeField]
    GameObject stage1Turrets;

    [SerializeField]
    GameObject stage2Turrets;
    [SerializeField]
    GameObject stage3Turrets;

    [SerializeField]
    SpriteRenderer frontSprite;
    [SerializeField]
    SpriteRenderer midSPrite;
    [SerializeField]
    SpriteRenderer backSprite;
    [SerializeField]
    Vector2 stage2Pos;
    [SerializeField]
    Vector2 stage3Pos;
    [SerializeField]
    float moveSpeed = 5.0f;
    [SerializeField]
    private float maxHP = 30;
    [SerializeField]
    private Vector2 transitionHealth = new Vector2(70.0f, 30.0f);
    [SerializeField]
    Slider healthBarSlider;
    [SerializeField]
    WeakPoint[] weakPoints;
    [SerializeField]
    private float fighterLaunchInterval = 10.0f;
    [SerializeField]
    private float fighterSpawnInterval = 0.5f;
    [SerializeField]
    GameObject fighterPrefab;

    [SerializeField]
    Transform hangar1;
    [SerializeField]
    Transform hangar2;
    [SerializeField]
    Transform hangar3;
    [SerializeField]
    int numFightersStage1 = 3;
    [SerializeField]
    int numFightersStage2 = 4;
    [SerializeField]
    int numFightersStage3 = 5;

    private AIManager ai_man;
    private float health;
    private int stage;
    Transform trans;

    private void Awake()
    {
        ai_man = AIManager.Instance;
        trans = transform;
    }
    private void Start()
    {
        AudioManager.Instance.PlayMainTrack(Sound.Level1BossTrack);
        health = maxHP;
        transitionHealth.x = (float)maxHP * transitionHealth.x / 100.0f;
        transitionHealth.y = (float)maxHP * transitionHealth.y / 100.0f;
        //LevelInfo levelInfo = ai_man.CurrentLevelInfo;
        //levelInfo.ai_Prefabs.Clear();
        //levelInfo.ai_Prefabs.Add(fighterPrefab);

        ai_man.transform.position = hangar1.position;
        ai_man.transform.rotation = hangar1.rotation;
        //ai_man.Direction = Vector2.zero;

        foreach (var item in weakPoints)
        {
            item.OnTakeDamage += TakeDamage;
            AIManager.Instance.RegisterNewAI(item.gameObject);
        }


        //AIManager.Instance.PlayerShip.GetComponent<PlayerShip>().FightingBoss(true);
        //StartCoroutine(Stage1());
        StartCoroutine(TransitionToStage(1));
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBarSlider.value -= damage / maxHP;
        //Debug.Log("Health: " + health);
        if (health <= 0)
        {
            Application.LoadLevel(GameScene.WinScreen.ToString());
        }
        //between 70 and 30
        if (stage != 2 && health < transitionHealth.x && health > transitionHealth.y)
        {
            stage = 2;
        }
        else if (stage != 3 && health < transitionHealth.y)
        {
            stage = 3;
        }
        else if (stage != 4 && health <= 0)
        {
            stage = 4;
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.G))
        {
            TakeDamage(10);
        }

       
        int numTurrets;
        switch (stage)
        {
            case 1:
                numTurrets = stage1Turrets.GetComponentsInChildren<AI_Turret>().Length;
                if (numTurrets == 0)
                {
                    weakPoints[0].gameObject.SetActive(true);
                }
                break;
            case 2:
                numTurrets = stage2Turrets.GetComponentsInChildren<AI_Turret>().Length;
                if (numTurrets == 0)
                {
                    weakPoints[1].gameObject.SetActive(true);
                }
                break;
            case 3:
                numTurrets = stage3Turrets.GetComponentsInChildren<AI_Turret>().Length;
                if (numTurrets == 0)
                {
                    weakPoints[2].gameObject.SetActive(true);
                }
                break;
            default:
                break;
        }
    }
    IEnumerator Stage1()
    {
        //ai_man.transform.position = hangar1.position;
        //ai_man.transform.rotation = hangar1.rotation;
        //ai_man.speed = Vector3.zero;
        stage = 1;
        //Debug.Log("Stage 1");
        while (stage == 1)
        {
            int numTurrets = stage1Turrets.GetComponentsInChildren<AI_Turret>().Length;
            if (numTurrets == 0)
            {
                weakPoints[0].gameObject.SetActive(true);
                yield return new WaitForSeconds(fighterLaunchInterval * .5f);
                yield return StartCoroutine(SpawnFighters(numFightersStage1 + 1, hangar1, false));
            }
            else
            {
                yield return new WaitForSeconds(fighterLaunchInterval);
                yield return StartCoroutine(SpawnFighters(numFightersStage1, hangar1, false));
            }

        }
        //yield return null;

        StartCoroutine(TransitionToStage(2));
    }
    IEnumerator Stage2()
    {
        stage = 2;
        //Debug.Log("Stage 2");

        //frontSprite.material = glitchMat;
        //weakPoints[0].gameObject.SetActive(false);
        //weakPoints[1].gameObject.SetActive(true);
        //weakPoints[2].gameObject.SetActive(false);

        //transform.position = stage2Pos.GetVector3();


        while (stage == 2)
        {
            int numTurrets = stage2Turrets.GetComponentsInChildren<AI_Turret>().Length;
            if (numTurrets == 0)
            {
                weakPoints[1].gameObject.SetActive(true);
                yield return new WaitForSeconds(fighterLaunchInterval * .5f);
                yield return StartCoroutine(SpawnFighters(numFightersStage2 + 1, hangar2, false));
            }
            else
            {
                yield return new WaitForSeconds(fighterLaunchInterval);
                yield return StartCoroutine(SpawnFighters(numFightersStage2, hangar2, false));
            }
        }
        StartCoroutine(TransitionToStage(3));
    }
    IEnumerator Stage3()
    {
        stage = 3;
        //Debug.Log("stage 3");


        while (stage == 3)
        {
            int numTurrets = stage3Turrets.GetComponentsInChildren<AI_Turret>().Length;
            if (numTurrets == 0)
            {
                weakPoints[2].gameObject.SetActive(true);
                yield return new WaitForSeconds(fighterLaunchInterval * .5f);
                yield return StartCoroutine(SpawnFighters(numFightersStage3 + 1, hangar3, false));
            }
            else
            {
                yield return new WaitForSeconds(fighterLaunchInterval);
                yield return StartCoroutine(SpawnFighters(numFightersStage3, hangar3, true));
            }
        }
        StartCoroutine(TransitionToStage(4));
        //StartCoroutine(Death());
    }
    IEnumerator TransitionToStage(int stage)
    {
        if (stage == 1)
        {
            weakPoints[0].gameObject.SetActive(false);
            weakPoints[1].gameObject.SetActive(false);
            weakPoints[2].gameObject.SetActive(false);
            StartCoroutine(Stage1());
            yield return null;
        }
        else if (stage == 2)
        {
            stage2Turrets.SetActive(true);
            foreach (var item in stage2Turrets.GetComponentsInChildren<AI_Turret>())
            {
                AIManager.Instance.RegisterNewAI(item.gameObject);

            }

            frontSprite.material = glitchMat;
            weakPoints[0].gameObject.SetActive(false);
            weakPoints[1].gameObject.SetActive(false);
            weakPoints[2].gameObject.SetActive(false);
            yield return StartCoroutine(MoveTo(stage2Pos.GetVector3()));
            StartCoroutine(Stage2());
        }
        else if (stage == 3)
        {
            stage3Turrets.SetActive(true);
            stage3Turrets.SetActive(true);
            foreach (var item in stage3Turrets.GetComponentsInChildren<AI_Turret>())
            {
                AIManager.Instance.RegisterNewAI(item.gameObject);

            }
            midSPrite.material = glitchMat;
            weakPoints[0].gameObject.SetActive(false);
            weakPoints[1].gameObject.SetActive(false);
            weakPoints[2].gameObject.SetActive(false);
            yield return StartCoroutine(MoveTo(stage3Pos.GetVector3()));
            StartCoroutine(Stage3());
        }
        else if(stage ==4)
        {
            backSprite.material = glitchMat;
            //weakPoints[2].gameObject.SetActive(false);
            StartCoroutine(Death());
        }

    }
    IEnumerator MoveTo(Vector3 dest)
    {
        Vector3 moveDir = dest - trans.position;
        while (Vector3.SqrMagnitude(moveDir) > 0.2f)
        {
            trans.position = Vector3.Lerp(trans.position, dest, moveSpeed * Time.deltaTime);
            moveDir = dest - trans.position;
            yield return null;
        }
        trans.position = dest;
    }
    IEnumerator Death()
    {
        Debug.Log("Death");
        //AIManager.Instance.PlayerShip.GetComponent<PlayerShip>().FightingBoss(true);
        yield return new WaitForSeconds(2.0f);
        //GameController.Instance.LevelComplete();
    }
    IEnumerator SpawnFighters(int numFighters, Transform hangar, bool glitch)
    {
        //Debug.Log("spawn");

        int i = 0;
        while (i < numFighters)
        {
            AIMove movement = null;
            GameObject fighterClone = Instantiate(fighterPrefab, hangar.position, hangar.rotation) as GameObject;
            if (glitch)
            {
                fighterClone.GetComponent<SpriteRenderer>().material = fighterGlitchMat;
            }
            AIShip fighter = fighterClone.GetComponent<AIShip>();
            //fighter.SetEquippedWeapon(Weapon.WeaponType.Shotgun);
            //fighter.EquippedWeapon.LevelDown();
            //fighter.EquippedWeapon.LevelDown();
            AIManager.Instance.RegisterNewAI(fighterClone);
            SinMove sinMove = fighterClone.AddComponent<SinMove>();
            //Debug.Log("Linear selected");
            if (sinMove)
            {
                sinMove.ApplySinHorizontal = false;
                sinMove.ApplySinVertical = false;
                sinMove.MoveDirection = transform.rotation * Vector3.right;
                movement = sinMove;
                movement.SetOwningShip(fighter);
                movement.MoveDirection = Vector2.right * -1.0f;
            }

            //fighterClone.AddComponent<AIAttack>().SetOwningShip(fighter);
            i++;
            yield return new WaitForSeconds(fighterSpawnInterval);
        }
        //ai_man.SpawnEnabled = true;
        //ai_man.SpawnInterval = fighterSpawnInterval;
        //StartCoroutine(ai_man.SpawnShip());
        //yield return new WaitForSeconds(numFighters * fighterSpawnInterval);
        //ai_man.SpawnEnabled = false;
    }
    //stage 1

    //stage 2

    //stage 3

}
