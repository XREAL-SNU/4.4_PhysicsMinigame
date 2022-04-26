using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BholUpdater : MonoBehaviour {
    public float force = 400f;
    public float radius = 6f;
    public float upwardsModifier = 0f;
    public float duration = 4f;

    private Vector3 pos;
    private float time = 0f;
    private PlayerControl pcon;

    void Start() {
        pcon = LevelGenerator.pconInstance;
        pos = transform.position + Vector3.up * 0.5f;
        StartCoroutine(Spawn());
    }

    void Update() {
        time += Time.deltaTime;
        if (time >= duration) {
            StartCoroutine(Despawn());
        }
        else {
            pcon.rigid.AddExplosionForce(force, pos, radius, upwardsModifier);
        }
    }

    private IEnumerator Spawn() {
        float t = 0;
        while(t < 0.25f) {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, pos, t / 0.25f);
            transform.localScale = Vector3.one * (t / 0.25f * 0.7f);
            yield return null;
        }
        transform.localScale = Vector3.one * 0.7f;
        transform.position = pos;
    }

    private IEnumerator Despawn() {
        float t = 0;
        while (t < 0.5f) {
            t += Time.deltaTime;
            transform.localScale = Vector3.one * ((1f - t / 0.5f) * 0.7f);
            yield return null;
        }

        Destroy(gameObject);
    }
}
