using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    public float m_Thrust = 2000f;
    private bool shouldShoot = false;
    private bool didShoot = false;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (shouldShoot)
        {
            m_Rigidbody.AddForce(transform.up * m_Thrust);
            didShoot = true;
        }
        else if (didShoot)
        {
            m_Rigidbody.AddForce(-m_Thrust * transform.up);
        }

        Ray ray = new(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hitData))
        {
            if (hitData.collider.CompareTag("TargetBall"))
            {
                Debug.Log("Hit!");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        shouldShoot = false;
    }

    private void OnMouseDrag()
    {        
        transform.RotateAround(transform.position, Vector3.up, 80 * Time.deltaTime);
    }

    private void OnMouseUp()
    {
        shouldShoot = true;
    }
}
