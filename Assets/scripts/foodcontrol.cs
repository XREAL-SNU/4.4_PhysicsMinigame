using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foodcontrol : MonoBehaviour
{
    public float damage=10.0f;
    public float foodpower=50.0f;

    private Rigidbody rigidbody1;
    
        // Start is called before the first frame update
    void Start()
    {
        rigidbody1=GetComponent<Rigidbody>();
        rigidbody1.AddForce(transform.up*foodpower);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
