using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Furniture : MonoBehaviour
{
    private float jumpingOffset = 1f;
    private float jumpNum;

    private void Start() {
        jumpNum = 0f;
    }

    public void Jumping() {
        jumpNum += 1f;
        Transform BigClosetTransform = transform.Find("BigCloset").gameObject.transform;
        DOTween.Sequence()
        .Join(transform.DOLocalMoveY(jumpingOffset, 0.7f))
        .SetLoops(2, LoopType.Yoyo);
        if(jumpNum > 1) {
            DOTween.Sequence()
            .Insert(0.7f, BigClosetTransform.DOLocalRotate(new Vector3(90, 180, 0), 2.5f)).SetEase(Ease.InSine)
            .Insert(0.7f, BigClosetTransform.DOLocalMoveY(1f, 2.5f)).SetEase(Ease.InSine);
        }
    }
}
