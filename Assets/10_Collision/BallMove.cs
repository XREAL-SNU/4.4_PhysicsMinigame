using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    private Rigidbody Banana;
    private float moveVelocity = 5f;
    void Start()
    {
        Banana = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            Banana.velocity = transform.up * moveVelocity;
    }
}
