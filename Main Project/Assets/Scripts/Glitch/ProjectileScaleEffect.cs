using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileScaleEffect : Effect 
{
    public float projScaleFactor { get; private set; }
    public void Init(float projScaleFactor, float duration)
    {
        base.Init(duration);
        this.projScaleFactor = projScaleFactor;
    }

    public override void Execute(Ship ship)
    {
        //Debug.Log("Projectile Scale effect");
        ship.ModifyProjScale(projScaleFactor);
        StartCoroutine(RemoveEffectAfterDuration(ship));
    }

    public override void DeActivate(Ship ship)
    {
        ship.SetDefaultProjScale();
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
