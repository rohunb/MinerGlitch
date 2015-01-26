using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalVars : ScriptableObject 
{
    //Editor Exposed
    [SerializeField]
    private float lerpDistanceEpsilon = 0.2f;
   
    

    //static vars for easy access
    public static float LerpDistanceEpsilon;
    
    private void OnEnable()
    {
        LerpDistanceEpsilon = lerpDistanceEpsilon;
        
    
    }
}
