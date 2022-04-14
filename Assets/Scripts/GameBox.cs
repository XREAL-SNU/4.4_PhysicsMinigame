using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBox : MonoBehaviour
{
    private Vector3 CurrentPosition;
    private Vector3 ShakingOffsetRightUpward = new Vector3(0.1f, 0.1f, 0f);
    private Vector3 ShakingOffsetLeftUpward = new Vector3(-0.1f, 0.1f, 0f);
    private Vector3 ShakingOffsetRightDownward = new Vector3(0.1f, -0.1f, 0f);
    private Vector3 ShakingOffsetLeftDownward = new Vector3(-0.1f, -0.1f, 0f);

    private Sequence shakingSequence;

    private void Start() {
        CurrentPosition = transform.position;
        GameManager.Instance().addGameBox(this.GetComponent<GameBox>());
        shakingSequence = DOTween.Sequence()
        .SetAutoKill(false)
        .Join(transform.DOLocalMove(CurrentPosition + ShakingOffsetRightUpward, 0.1f))
        .Append(transform.DOLocalMove(CurrentPosition + ShakingOffsetLeftUpward*2, 0.1f))
        .Append(transform.DOLocalMove(CurrentPosition + ShakingOffsetLeftDownward, 0.1f))
        .Append(transform.DOLocalMove(CurrentPosition + ShakingOffsetRightDownward*2, 0.1f))
        .Append(transform.DOLocalMove(CurrentPosition + ShakingOffsetRightUpward, 0.1f))
        .Append(transform.DOLocalMove(CurrentPosition + ShakingOffsetRightDownward*2, 0.1f))
        .Append(transform.DOLocalMove(CurrentPosition + ShakingOffsetLeftUpward*2, 0.1f))
        .Append(transform.DOLocalMove(CurrentPosition + ShakingOffsetRightUpward, 0.1f))
        .Append(transform.DOLocalMove(CurrentPosition + ShakingOffsetLeftUpward, 0.1f));
        shakingSequence.SetLoops(2).Pause();
    }

    public void Shaking() {
        shakingSequence.Rewind();
        shakingSequence.Play();
    }
}
