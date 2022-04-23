using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBall : MonoBehaviour
{
    public float jumpPower;
    public int itemCount;
    public GameManagerLogic manager;
    bool isJump;
    Rigidbody rigid;

    void Awake()
    {
        isJump = false;
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !isJump)
        {
            isJump = true;
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        rigid.AddForce(new Vector3(h, 0, v), ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
            isJump = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            itemCount++;
            GetComponent<AudioSource>().Play();
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "Finish")
        {
            if (itemCount == manager.totalItemCount)
            {
                SceneManager.LoadScene("Example 1_"+(manager.stage+1).ToString());
            }

            else
            {
                SceneManager.LoadScene("Example 1_"+manager.stage);
            }

        }
    }
}



/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Transform playerTransform;
    Vector3 Offset;
    Vector3 Offset_1;
    Vector3 Offset_2;

    float timer;
    int waitingTime;

    void Start()
    {
        timer = 0.0F;
        waitingTime = 2;
    }


    // Start is called before the first frame update
    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //to load 'player' to use later
        //the last '.transform': to match the type of data

        Offset = transform.position - playerTransform.position;
    }

    void Update()
    {
        Offset_1 = transform.position - playerTransform.position;

        timer += Time.deltaTime;
        if (timer > waitingTime)
        {
            Offset_2 = transform.position - playerTransform.position;
            transform.position = playerTransform.position + Offset_1 - Offset_2;
            timer = 0;
        }
    }

}

*/