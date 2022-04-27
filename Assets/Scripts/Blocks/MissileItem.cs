using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileItem : Block {
    public Missile missile;

    public Vector3 initRotation;
    public float spin = 60f;
    public GameObject hitFx = null;

    protected virtual void Start() {
        transform.rotation = Quaternion.Euler(initRotation);
    }

    protected override void Update() {
        base.Update();

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + Vector3.up * spin * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.CompareTag("Player")) {
            if (hitFx != null) Instantiate(hitFx, transform.position, Quaternion.identity);
            MissileControl.instance.AddMissile(missile, 1);
            Destroy(gameObject);
        }
    }
}
