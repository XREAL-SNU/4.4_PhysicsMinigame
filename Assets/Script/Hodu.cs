using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hodu : MonoBehaviour
{


    public float jumpPower;
    public int itemCount;
    bool isJump;
    Rigidbody rigid;

    void Awake()
    {

        isJump = false;
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && !isJump ) {
            isJump = true;
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);

        }

    }




    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        rigid.AddForce(new Vector3(h, 0, v), ForceMode.Impulse);
        // y축은 점프여서 일단 0으로 세

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
            isJump = false;





    }



}
