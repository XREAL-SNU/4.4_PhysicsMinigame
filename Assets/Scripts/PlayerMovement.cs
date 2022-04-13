using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 5f;

    // public CharacterController characterController;
    public float speed;
    private Vector3 camRotation;
    private Transform cam;


    private int minAngle = 60;

    private int maxAngle = 100;

    public int sensitivity = 1000;

    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Vector3 m_Rotate;
    Quaternion m_Rotation = Quaternion.identity;

    private void Awake()
    {
        cam = Camera.main.transform;
        cam.localEulerAngles = new Vector3(80f, 90f, 180f);
    }

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(-vertical, 0f, horizontal);
        m_Movement.Normalize();
        m_Rotate.Set(horizontal, 0f, vertical);


        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;


        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Rotate, turnSpeed * Time.deltaTime, 0f);
        // keep pan upstraight
        m_Rotation = Quaternion.LookRotation(desiredForward) * Quaternion.Euler(0, 0, 270f);

        if (isWalking)
        {
            m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * 2F * Time.deltaTime);
            m_Rigidbody.MoveRotation(m_Rotation);

        }
        camRotation.x += Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        camRotation.x = Mathf.Clamp(camRotation.x, minAngle, maxAngle);

        camRotation.y = 90;
        camRotation.z = 180;
        cam.localEulerAngles = camRotation;
    }

}
