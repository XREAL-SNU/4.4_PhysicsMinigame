using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{


    Transform playerTransform;
    Vector3 Offset;


    void Awake()
    {
        playerTransform = GameObject.Find("Hodu").transform;
        Offset = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
        //보통 ui 또는 카메라는 LateUpdate 에서 이루어짐 
    {


        transform.position = playerTransform.position + Offset;


    }
}
