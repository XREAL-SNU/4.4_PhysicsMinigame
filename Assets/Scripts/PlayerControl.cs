using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelGenerator;

public class PlayerControl : MonoBehaviour {
    public Rigidbody rigid;
    public Camera cam;
    public ScoreModule score;

    void Awake() {
        rigid = GetComponent<Rigidbody>();
    }

    void Update() {
        if(transform.position.y < playerGroundy - (DESPAWNLAYER + 1) * LAYERDIST - 15f) {
            //ded
            Kill();
        }
    }

    public void Kill() {
        score.End();
    }

    private void OnCollisionEnter(Collision collision) {
        if(!collision.collider.isTrigger && rigid.velocity.y < 0.1f && transform.position.y >= 0.2f + collision.transform.position.y && collision.transform.position.y > playerGroundy) {
            playerGroundy = collision.transform.position.y;
        }
    }
}
