using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelGenerator;

public class BorderUpdater : MonoBehaviour {
    void Update() {
        if (transform.position.y < playerGroundy - (DESPAWNLAYER + 1) * LAYERDIST - 1f) {
            Destroy(gameObject);
        }
    }
}
