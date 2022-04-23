using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Transform playerTransform;
    Vector3 Offset;
    Vector3 Offset_1;
    Vector3 Offset_2;

    float timer;
    public double waitingTime;

    void Start()
    {
        timer = 0.0F;
        waitingTime = 0.2;
    }


    // Start is called before the first frame update
    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //to load 'player' to use later
        //the last '.transform': to match the type of data

        Offset = transform.position - playerTransform.position;
    }

    void Update()
    {
        Offset_1 = transform.position - playerTransform.position;

        timer += Time.deltaTime;
        if (timer > waitingTime)
        {
            Offset_2 = transform.position - playerTransform.position;
            transform.position = playerTransform.position + Offset_1 - Offset_2;
            timer = 0;
        }
    }

}
