using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerShip : Ship
{

    private PlayerInput playerInput;
    private PlayerMove playerMove;
    private PlayerAttack playerAttack;
    [SerializeField]
    Slider healthBar;
    [SerializeField]
    float maxHP;

    public override void ApplyDamage(float damage)
    {
        
        Health -= damage;
        Debug.Log("taking damage: " + damage + " health: " + Health);
        healthBar.value -= damage / maxHP;
        if(Health<=0)
        {
            Application.LoadLevel(GameScene.GameOver.ToString());
        }
    }
    public override void ModifySpeed(float multiplier)
    {
        playerMove.ModifySpeed(multiplier);

    }
    public override void SetDefaultSpeed()
    {
        playerMove.SetDefaultSpeed();
    }
    protected override void Awake()
    {
        base.Awake();
        playerInput = gameObject.GetSafeComponent<PlayerInput>();
        playerMove = gameObject.GetSafeComponent<PlayerMove>();
        playerAttack = gameObject.GetSafeComponent<PlayerAttack>();
        playerAttack.Init(Weapons);
        Health = maxHP;
    }
  
}
