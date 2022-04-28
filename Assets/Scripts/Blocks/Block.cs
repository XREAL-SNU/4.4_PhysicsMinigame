using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelGenerator;

public class Block : MonoBehaviour {
    public float scl = 5f;
    public float thresh = 0.7f;
    public GameObject destroyFx = null;

    protected virtual void Update() {
        if (transform.position.y < playerGroundy - (DESPAWNLAYER + 1) * LAYERDIST - 1f) {
            Destroy(gameObject);
        }
    }

    public void Kill() {
        if (destroyFx != null) Instantiate(destroyFx, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
