using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetecter : MonoBehaviour
{
    private MeshRenderer _renderer;
    private Material _mat;

    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _mat = _renderer.material;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Stone1")
        {
            Debug.Log("Stone1 entered");
        }

        if (collision.gameObject.name == "Stone2")
        {
            Debug.Log("Stone2 entered");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Stone1")
        {
            Debug.Log("Stone1 went out");
        }

        if (collision.gameObject.name == "Stone2")
        {
            Debug.Log("Stone2 went out");
        }
    }
}