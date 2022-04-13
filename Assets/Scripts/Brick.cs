using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Brick : Interface
{
    private bool isEnoughHeight = false;
    private bool isInterface = false;
    private int clickedNum = 0;

    public override void Start() {
        GameManager.Instance().addInterface("Brick", this.GetComponent<Brick>());
    }

    public override void Update() {
        
    }

    public void OnMouseDown() {
        clickedNum += 1;
        if(clickedNum == 1) {
            DOTween.Sequence()
            .Append(transform.DORotate(new Vector3(0f, 0f, 10f), 0.5f))
            .Insert(0f, transform.DOLocalMove(new Vector3(7.7f, 10f, -25f), 0.5f));
        } if (clickedNum >= 2) {
            Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
            rigidbody.useGravity = true;
        }
    }

    public override void OnMouseDrag() {
        if(clickedNum > 2) {
            base.OnMouseDrag();
        }
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
