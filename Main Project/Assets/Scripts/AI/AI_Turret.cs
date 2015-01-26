using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI_Turret : AIShip 
{
    //private Weapon currentWeapon;
    private Transform playerTrans;
    //private Transform trans;
    private AIAttack ai_Attack;

    protected override void Awake()
    {
        base.Awake();
        trans = transform;
        playerTrans = AIManager.Instance.PlayerShip.transform;
        ai_Attack = GetComponent<AIAttack>();
        ai_Attack.SetOwningShip(this);
        
    }
    protected override void Start()
    {
        base.Start();

        //equippedWeapon = Instantiate(EquippedWeapon) as Weapon;
        equippedWeapon.EnemyLayer = TagsAndLayers.PlayerShipLayer;
    }
    
    void Update()
    {
        if(playerTrans)
        {
            Vector3 dir = playerTrans.position - trans.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            trans.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
            equippedWeapon.ShotDirection = trans.right;
        }
        else
        {
            Debug.LogError("No palyer");
        }
    }
    
	
}
