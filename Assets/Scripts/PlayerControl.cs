using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static LevelGenerator;

public class PlayerControl : MonoBehaviour {
    public Rigidbody rigid;
    public Camera cam;

    void Awake() {
        rigid = GetComponent<Rigidbody>();
    }

    void Update() {
        if(transform.position.y < playerGroundy - (DESPAWNLAYER + 1) * LAYERDIST - 1f) {
            //ded
            Kill();
        }
        //todo playerthingy
    }

    public void Kill() {
        SceneManager.LoadScene("GameScene");
    }
}
