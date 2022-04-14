using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour

{
    // Start is called before the first frame update
    

    public float speed=8;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 direction= Vector3.down;
        transform.position+=direction*speed*Time.deltaTime;
    }
}
