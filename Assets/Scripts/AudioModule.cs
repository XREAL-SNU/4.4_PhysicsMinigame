using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioModule : MonoBehaviour {
    public static AudioModule main;
    public GameObject prefab;

    private void Awake() {
        main = this;
    }

    public void At(AudioClip clip, Vector3 pos) {
        AudioSource.PlayClipAtPoint(clip, pos);
    }

    public void At(AudioClip clip, Vector3 pos, float volume) {
        AudioSource.PlayClipAtPoint(clip, pos, volume);
    }

    public void At(AudioClip clip, Vector3 pos, float volume, float pitch) {
        At(clip, pos, volume, pitch, clip.length);
    }

    public void At(AudioClip clip, Vector3 pos, float volume, float pitch, float endTime) {
        AudioSource a = Instantiate(prefab, pos, Quaternion.identity).GetComponent<AudioSource>();
        a.clip = clip;
        a.volume = volume;
        a.pitch = pitch;
        a.Play();

        if(endTime > 0) {
            StartCoroutine(DestroyAfter(a.gameObject, Mathf.Min(endTime, clip.length)));
        }
    }

    IEnumerator DestroyAfter(GameObject go, float seconds) {
        yield return new WaitForSeconds(seconds);
        Destroy(go);
    }
}
