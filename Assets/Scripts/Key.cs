using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Key : Interface
{
    private bool isDoorOpen = false;

    public void Start() {
        GameManager.Instance().addInterface("Key", this.GetComponent<Key>());
    }

    public void OnMouseEnter() {
        if(transform.parent.tag == "Furniture") {
            DOTween.Sequence()
            .Append(transform.DOScale(new Vector3(7, 7, 7), 0.5f));
        }
    }

    public void OnMouseExit() {
        if(transform.parent.tag == "Furniture") {
            DOTween.Sequence()
            .Append(transform.DOScale(new Vector3(5, 5, 5), 0.5f));
        }
    }

    public void OnMouseDown() {
        if(transform.parent.tag == "Furniture") {
            transform.SetParent(GameObject.FindWithTag("Interface").transform);
            DOTween.Sequence()
            .Append(transform.DOMove(new Vector3(transform.position.x, 1, -27), 1f));
        }
    }

    public override void OnMouseDrag() {
        if(transform.parent.tag == "Interface") {
            base.OnMouseDrag();
            RaycastHit hit;
            GameObject target = null;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int layerMask = (1 << 9);
            if(Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, layerMask)) {
                target = hit.collider.gameObject;
                if(target == GameManager.Instance().getInterface("Door").gameObject) {
                    Transform doorTransform = GameManager.Instance().getInterface("Door").gameObject.transform;
                    DOTween.Sequence()
                    .Append(doorTransform.DOScale(new Vector3(2.2f, 2.2f, 2.2f), 0.5f));
                    isDoorOpen = true;
                } else {
                    isDoorOpen = false;
                }
            }
        }
    }

    public void OnMouseUp() {
        if(isDoorOpen) {
            GameManager.Instance().setClear(true);
        }
        if(transform.parent.tag == "Interface") {
            DOTween.Sequence()
            .Append(transform.DOMove(new Vector3(transform.position.x, 1, -27), 0.5f));
        }
    }
}
