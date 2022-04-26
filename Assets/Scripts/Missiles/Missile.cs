using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMissile", menuName = "Missile", order = 0)]
public class Missile : ScriptableObject {
    public float force = 1f;
    public float radius = 5f;
    public float upwardsModifier = 0.1f;

    public GameObject explosionFx = null, smokeFx = null;
    public GameObject missileObject = null;
    public float missileDuration = 0.5f;

    public Sprite sprite;

    public void Fire(PlayerControl player, Vector3 pos, float str) {
        if (missileObject == null) {
            Impact(player.rigid, pos, str);
        }
        else {
            player.StartCoroutine(MissileShoot(player, pos, str));
        }
    }

    private void Impact(Rigidbody player, Vector3 pos, float str) {
        player.AddExplosionForce(force * str, pos, radius, upwardsModifier);
        if (explosionFx != null) Instantiate(explosionFx, pos, Quaternion.identity);
        if (smokeFx != null) Instantiate(smokeFx, pos, Quaternion.identity);
    }

    private IEnumerator MissileShoot(PlayerControl player, Vector3 pos, float str) {
        float t = 0f;
        Vector3 svec = player.cam.transform.position + player.cam.transform.forward * -1f + new Vector3(Random.Range(-2f, 2f), -1.5f, 0f);
        GameObject mo = Instantiate(missileObject, svec, Quaternion.LookRotation(pos - svec) * Quaternion.Euler(90f, 0, 0));
        while (t < missileDuration) {
            t += Time.deltaTime;
            float f = t / missileDuration;
            mo.transform.position = Vector3.Lerp(svec, pos, f*f);
            yield return null;
        }
        mo.GetComponent<MissileUpdater>().Kill();
        Impact(player.rigid, pos, str);
    }
}
