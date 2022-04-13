using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : Interface
{
    public void Start() {
        GameManager.Instance().addInterface("Door", this.GetComponent<Door>());
    }

    public void OnMouseEnter() {
        if(GameManager.Instance().getInterface("Key").getIsClicked()) {
            DOTween.Sequence()
            .Append(transform.DOScale(new Vector3(2.5f, 2.5f, 2.5f), 0.5f));
        }
    }

    public void OnMouseExit() {
        DOTween.Sequence()
        .Append(transform.DOScale(new Vector3(2f, 2f, 2f), 0.5f));
    }
}
