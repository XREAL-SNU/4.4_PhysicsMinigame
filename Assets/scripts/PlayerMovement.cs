using System.Collections;
using System.Threading;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    private InputActions inputActions;
    private Rigidbody rigidBody;
    private float ROTATION_VALUE = (float) 0.2;
    private Vector3 leftRotateVector = new Vector3((float) 2, 0, (float) 2);
    private Vector3 rightRotateVector = new Vector3((float) -2, 0, (float) -2);
    private string rotationMode = "left";

    private int totalScore = 0;
    [SerializeField] private float speed = 1000f;

    // Start is called before the first frame update
    void Awake()
    {
        inputActions = new InputActions();
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      
        Vector2 moveInput = inputActions.Newactionmap.Movement.ReadValue<Vector2>();
        rigidBody.velocity = moveInput * speed;

    }

    private void Update()
    {
        Debug.Log(transform.rotation);

        if(transform.rotation.x > ROTATION_VALUE && rotationMode == "left")
        {
            rotationMode = "right";
        }

        if(transform.rotation.x  < -1 * ROTATION_VALUE && rotationMode == "right")
        {
            rotationMode = "leftToOrigin";
        }

        if(Mathf.Abs(transform.rotation.x) < 0.1 && rotationMode == "leftToOrigin")
        {
            rotationMode = "left";
        }

        if(rotationMode == "left")
        {
            transform.Rotate(leftRotateVector * Time.deltaTime);
        } else if(rotationMode == "right")
        {
            transform.Rotate(rightRotateVector * Time.deltaTime);
        } else
        {
            transform.Rotate(leftRotateVector * Time.deltaTime);
        }

    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<CollidableBall>() != null)
        {
            Debug.Log("player collide with collidable ball");
            totalScore += collision.gameObject.GetComponent<CollidableBall>().getScore();
            Destroy(collision.gameObject);
        }

    }
}
