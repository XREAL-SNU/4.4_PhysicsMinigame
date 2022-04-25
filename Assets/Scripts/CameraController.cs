using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float yOffset = 5.42f;
    public PlayerControl pcon;

    void Update() {
        transform.position = new Vector3(transform.position.x, pcon.transform.position.y + yOffset, transform.position.z);
    }
}
