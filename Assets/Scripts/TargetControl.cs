using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetControl : MonoBehaviour {
    public Camera cam;
    public SpriteRenderer spriter, shadowr;
    public MissileControl mc;

    public Sprite normalSprite, holdSprite;
    public Color normalColor = Color.red;
    public Color holdColor = Color.yellow, holdEndColor = Color.red;

    void Update() {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit rinfo, 50f, 1 << 6)) {
            Vector3 d = rinfo.point;
            d.y = Mathf.Round(d.y / LevelGenerator.LAYERDIST) * LevelGenerator.LAYERDIST + 0.5f;
            transform.position = d;
        }

        if (!Input.GetMouseButton(0)) {
            spriter.sprite = normalSprite;
            shadowr.sprite = normalSprite;
            transform.localScale = Vector3.one * 1.2f;
            shadowr.color = normalColor;
        }
        else {
            spriter.sprite = holdSprite;
            shadowr.sprite = holdSprite;
            transform.localScale = Vector3.one * (1.3f + 0.4f * mc.charge / MissileControl.CHARGETIME);
            shadowr.color = Color.Lerp(holdColor, holdEndColor, mc.charge / MissileControl.CHARGETIME);
        }

        transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 80f * Time.deltaTime);
    }
}
