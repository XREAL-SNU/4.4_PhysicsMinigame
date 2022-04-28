using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    public Vector3 initialImpulse;
    Rigidbody m_Rigidbody;
    // Start is called before the first frame update
    void OnEnable()
    {
        m_Rigidbody = GetComponent<Rigidbody>();

        transform.position = new Vector3(0, 10.43f, -3.65f);
        m_Rigidbody.AddForce(initialImpulse, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
