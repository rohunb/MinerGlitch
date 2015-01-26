using UnityEngine;
using System.Collections;

public class AIShip : Ship {
    void Update()
    {
        //UpdateWeaponDirection(new Vector2(-transform.right.x, -transform.right.y));
        //Debug.Log("New Direction: " + new Vector2(-transform.right.x, -transform.right.y));
    }

    public override void ApplyDamage(float damage)
    {
        if (ShieldOn)
        {
            damage -= ShieldPower;

            if (damage < 0.0f)
                damage = 0.0f;
        }
        Health -= damage;

        if (Health <= 0.0f)
        {
            Debug.Log(gameObject.name + "less than 0");
            AIManager.Instance.KillShip(gameObject);
        }
    }
	
}
