using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : Interface
{
    private bool isEnoughHeight = false;

    public override void Start() {
        GameManager.Instance().addInterface("Brick", this.GetComponent<Brick>());
    }

    public override void Update() {
        
    }

    public override void OnMouseDrag() {
        base.OnMouseDrag();
        if(transform.position.y > 6) {
            isEnoughHeight = true;
        }
    }

    public override bool getIsEnoughHeight() {
        return isEnoughHeight;
    }

    public override void onCollision() {
        isEnoughHeight = false;
    }
}
