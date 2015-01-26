using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Effect : MonoBehaviour
{
    public float duration { get; private set; }
    public virtual void Init(float duration)
    {
        this.duration = duration;
    }
    public abstract void Execute(Ship ship);
    public abstract void DeActivate(Ship ship);
    public virtual IEnumerator RemoveEffectAfterDuration(Ship ship)
    {
        yield return new WaitForSeconds(duration);
    }
}
