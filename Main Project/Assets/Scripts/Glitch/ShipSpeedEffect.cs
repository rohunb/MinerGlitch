using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipSpeedEffect : Effect 
{
    public float speedFactor { get; private set; }

    public void Init(float speedFactor, float duration)
    {
        base.Init(duration);
        this.speedFactor = speedFactor;
    }

    public override void Execute(Ship ship)
    {
        Debug.Log("Speed Effect");
        ship.ModifySpeed(speedFactor);
        StartCoroutine(RemoveEffectAfterDuration(ship));
    }

    public override void DeActivate(Ship ship)
    {
        ship.SetDefaultSpeed();
        StartCoroutine(ship.SetGlitchEffect());
    }

    public override IEnumerator RemoveEffectAfterDuration(Ship ship)
    {
        yield return new WaitForSeconds(duration);
        DeActivate(ship);
    }
}
