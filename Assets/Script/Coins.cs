using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{



    public float rotateSpeed;



    void Awake()
    {
    }


    void Update()
    {
        transform.Rotate(Vector3.up  * rotateSpeed * Time.deltaTime );
        
    }


    }

