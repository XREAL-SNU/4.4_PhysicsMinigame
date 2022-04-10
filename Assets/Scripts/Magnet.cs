using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public Vector3 newPosition;
    private Transform trans;

    private void Awake()
    {
        trans = transform;
    }
    void Update()
    {
        trans.position = Vector3.Lerp(trans.position, newPosition, Time.deltaTime * 1.5f);

        if (Mathf.Abs(newPosition.x - trans.position.x) < 0.05)
        {
            trans.position = newPosition;
        }
    }


}
