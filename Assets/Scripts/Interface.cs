using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interface : MonoBehaviour
{
    private RaycastHit hitInfo;
    private Vector3 CurrentPosition;

    public virtual void Start() {
        
    }

    public virtual void Update() {

    }

    public virtual bool getIsEnoughHeight()
    {
        return false;
    }

    public virtual void OnMouseDrag() {
        Vector3 SpacePos = Camera.main.WorldToScreenPoint(transform.position);
        float distance = SpacePos.z;
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = correctingPosition(objPosition);
    }

    private Vector3 correctingPosition(Vector3 prevPosition) {
        Vector3 newPosition = prevPosition;
        newPosition.y = Mathf.Max(1f, newPosition.y);
        return newPosition;
    }

    public virtual void onCollision() {

    }

    public virtual bool getIsClicked() {
        return false;
    }
}
