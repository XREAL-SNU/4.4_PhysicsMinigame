using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Furniture : MonoBehaviour
{
    private float jumpingOffset = 1f;

    private void Start() {

    }

    public void Jumping() {
        DOTween.Sequence()
        .Join(transform.DOLocalMoveY(jumpingOffset, 0.7f))
        .SetLoops(2, LoopType.Yoyo);
    }
}
