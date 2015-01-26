using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipScaleEffect : Effect 
{
    public float scaleFactor { get; private set; }

    public void Init(float scaleFactor,float duration)
    {
        base.Init(duration);
        this.scaleFactor = scaleFactor;
    }

    public override void Execute(Ship ship)
    {
        Debug.Log("Scale Effect");
        ship.ModifyScale(scaleFactor);
        StartCoroutine(RemoveEffectAfterDuration(ship));
    }

    public override void DeActivate(Ship ship)
    {
        ship.SetDefaultScale();
    }
    public override IEnumerator RemoveEffectAfterDuration(Ship ship)
    {
        yield return new WaitForSeconds(duration);
        if(ship.gameObject)
        {
            DeActivate(ship);
        }
    }
   
}
