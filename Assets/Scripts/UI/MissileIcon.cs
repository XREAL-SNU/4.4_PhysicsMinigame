using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MissileIcon : MonoBehaviour {
    public TextMeshProUGUI tkey, tamount;
    public RawImage image;
    public Button button;

    public Color selectColor, deselectColor;

    public Missile missile;
    private MissileControl mc;
    private int lastAmount = -1;

    void Start() {
        button.onClick.AddListener(Clicked);
    }

    void Update() {
        if(mc.current == missile) {
            tkey.color = selectColor;
            image.color = Color.white;
        }
        else {
            tkey.color = deselectColor;
            image.color = new Color(1, 1, 1, 0.5f);
        }

        if(missile != mc.defaultMissile && lastAmount != mc.inventory[missile]) {
            lastAmount = mc.inventory[missile];
            tamount.text = "x" + lastAmount;
        }
    }

    public void Set(MissileControl mc, Missile missile, int i) {
        this.mc = mc;
        this.missile = missile;
        image.texture = missile.sprite.texture;
        tkey.text = "[" + i + "]";
        if(missile == mc.defaultMissile) {
            tamount.gameObject.SetActive(false);
        }
        else {
            gameObject.SetActive(false);
        }
    }

    private void Clicked() {
        if(mc.IsValid(missile)) mc.current = missile;
    }
}
