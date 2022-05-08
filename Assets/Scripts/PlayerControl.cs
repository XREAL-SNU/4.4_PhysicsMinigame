using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelGenerator;

public class PlayerControl : MonoBehaviour {
    public Rigidbody rigid;
    public new AudioSource audio;
    public Camera cam;
    public ScoreModule score;

    public GameObject killFx;
    public AudioClip killSound;
    private bool dead = false;

    void Awake() {
        rigid = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        dead = false;
    }

    void Update() {
        if(transform.position.y < playerGroundy - (DESPAWNLAYER + 1) * LAYERDIST - 15f) {
            //ded
            Kill();
        }
    }

    public void Kill() {
        if (dead) return;
        dead = true;
        if(killSound != null) audio.PlayOneShot(killSound, 0.6f);
        if (killFx != null) {
            Instantiate(killFx, transform.position, Quaternion.identity);
            GetComponent<MeshRenderer>().enabled = false;
        }

        Invoke("End", 1.2f);
    }

    private void End() {
        score.End();
    }

    private void OnCollisionEnter(Collision collision) {
        //Debug.Log("Col: "+collision.gameObject.name);
        //Debug.Log("Vel: " + rigid.velocity.y);
        //Debug.Log("Dst: "+(transform.position.y - collision.transform.position.y));
        if(!collision.collider.isTrigger && (rigid.velocity.y < 0.8f || (rigid.velocity.y < 1.3f && collision.gameObject.CompareTag("Platform"))) && transform.position.y >= 0.2f + collision.transform.position.y && collision.transform.position.y > playerGroundy) {
            playerGroundy = collision.transform.position.y;
        }

        if (!collision.collider.isTrigger && ((!collision.gameObject.CompareTag("Platform") || collision.relativeVelocity.y > 0.1f) && Mathf.Abs(collision.relativeVelocity.y) > 0.2f)) {
            Block b = collision.transform.GetComponentInParent<Block>();
            //Debug.Log(collision.relativeVelocity.y);
            if (b != null && b.sound != null) audio.PlayOneShot(b.sound, Mathf.Clamp(collision.relativeVelocity.magnitude / 4f, 0f, 0.9f) * b.volume);
        }
    }
}
