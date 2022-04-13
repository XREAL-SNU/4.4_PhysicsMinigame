using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    [SerializeField]
    public bool _isContact = false;
    Collider collider;

    void OnTriggerExit(Collider other)
    {
        if (collider == other)
        {
            _isContact = false;
            collider = null;
        }
        //sound change event
    }
    void OnTriggerEnter(Collider other)
    {
        if (!collider)
        {
            _isContact = true;
            collider = other;
        }
            
        // ���ϴ� �ڵ� �ۼ�
    }
}
