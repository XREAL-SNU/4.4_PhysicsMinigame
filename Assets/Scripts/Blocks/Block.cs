using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelGenerator;

public class Block : MonoBehaviour {
    public float scl = 20f;
    public float thresh = 0.7f;

    protected virtual void Update() {
        if(transform.position.y < playerGroundy - (DESPAWNLAYER + 1) * LAYERDIST - 1f) Destroy(gameObject);
    }
}
