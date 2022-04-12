using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float speed = 1f;
    float mouseX;
    float mouseY;

    CharacterController CC;
    private void Awake()
    { 
        CC = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float xSpeed = Input.GetAxisRaw("Horizontal");
        float ySpeed = Input.GetAxisRaw("Vertical");
        mouseX += Input.GetAxis("Mouse X") * 10f;
        mouseY += Input.GetAxis("Mouse Y") * 5f;
        mouseY = Mathf.Clamp(mouseY, -55.5f, 55.5f);
        Vector3 dir = new Vector3(xSpeed, 0, ySpeed);

        CC.Move(transform.TransformDirection(dir) * Time.deltaTime * speed);
        transform.eulerAngles = new Vector3(0, mouseX, 0);
        transform.localEulerAngles = new Vector3(-mouseY, 0, 0);
    }


}
