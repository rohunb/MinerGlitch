using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnInfo : MonoBehaviour {

    public bool dontSpawn = false;

    public Vector2 releaseDirection = new Vector2(-1.0f, 0.0f);
    public float releaseSpeed = 5.0f;

    public float initialInterval = 1.0f;
    public float spawnInterval = 5.0f;
    public int numberToSpawn = 5;

    public List<GameObject> spawnSequence;
    public bool loopSequence = false;
    public int numberOfLoops = -1;

    public bool randomSequence = false;

    public float duration = -1.0f;

    public float managerSpeed = 2.0f;
    public Vector3 managerPosition = new Vector3(65.0f, 1.0f, 0.0f);

    public bool SinHorizontal = false;
    public bool SinVertical = false;

    public float Amplitude = 0.0f;
    public float Frequency = 0.0f;

}
