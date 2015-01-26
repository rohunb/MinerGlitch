using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ErraticShotsEffect : Effect 
{
    public float accuracyFactor { get; private set; }

    public void Init(float accuracyFactor, float duration)
    {
        base.Init(duration);
        this.accuracyFactor = accuracyFactor;
    }

    public override void Execute(Ship ship)
    {
        Debug.Log("Erratic effect");
        ship.ModifyAccuracy(accuracyFactor);
        StartCoroutine(RemoveEffectAfterDuration(ship));
    }

    public override void DeActivate(Ship ship)
    {
        ship.SetDefaultAccuracy();
    }

    public override IEnumerator RemoveEffectAfterDuration(Ship ship)
    {
        yield return new WaitForSeconds(duration);
        if(ship)
        {
            DeActivate(ship);
        }
    }
}
