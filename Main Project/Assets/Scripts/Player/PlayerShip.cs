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
        healthBar.value -= damage / maxHP;
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
