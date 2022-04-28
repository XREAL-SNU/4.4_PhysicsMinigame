using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    private CharacterController characterController;
    private Animator animator;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        Vector3 currentMovement = new Vector3(hAxis, 0, vAxis) * moveSpeed * Time.deltaTime;
        characterController.Move(currentMovement);
        transform.LookAt(transform.position + currentMovement);
        animator.SetBool("isMove", currentMovement != Vector3.zero);
    }
}
