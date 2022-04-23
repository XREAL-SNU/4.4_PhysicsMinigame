using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//SceneManager 장면을 관리하는 기본 클래스 

public class Hodu : MonoBehaviour
{


    public float firstJumpPower;// 뛰는 높이
    public float secondJumpPower;
    bool isGround;
    bool firstJump;
    bool secondJump;

    public int bitcoinItemCount;// 먹은 비트코인 개수
    public GameManagerLogic manager;


    Rigidbody rigid;
    AudioSource audio;


    //Animator animator;


    void Awake()
    {
        isGround = true;
        firstJump = false;
        secondJump = false;
        rigid = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        //animator = GetComponentInChildren<Animator>();

    }


   


    private void Update()
    {
        if (Input.GetButtonDown("Jump") && isGround==true)
        {
            rigid.AddForce(new Vector3(0, firstJumpPower, 0), ForceMode.Impulse);
            firstJump = true;
            secondJump = false;
            isGround = false;
        }




        if (Input.GetButtonDown("Jump") && isGround == false && firstJump == true) 
        {
            rigid.AddForce(new Vector3(0, secondJumpPower, 0), ForceMode.Impulse);
            secondJump = true;
            firstJump = false;
        }

    }



    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        rigid.AddForce(new Vector3(h, 0, v), ForceMode.Impulse);
        // y축은 점프여서 일단 0으로 세기



        //animator.SetBool("isRun", rigid.AddForce != Vector3.zero);

    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
            isGround = true;
        Debug.Log("grounded!!!!!!!!");


    }








    private void OnTriggerEnter(Collider other)
    {
        //비트코인 카운트 추가
        if (other.name == "Bitcoin")
        {
            bitcoinItemCount++;
        }



        // 만약에 코인을 먹으면 소리 
        if (other.tag == "Coins")
        {
            audio.Play();
            //플레이어가 아이템을 먹고나면 오디오 제생

            other.gameObject.SetActive(false);
            // SetActive(bool): 오브젝트 활성화 비활성, 여기서는 비활성화
        }




        if (other.tag == "Finish")
        {



            if (bitcoinItemCount == manager.TotalItemCount)
            {
                SceneManager.LoadScene("Main" + (manager.stage + 1).ToString());

                //문자열인지 아닌지 걱정될때 ToString()더하면 됨 
                //game clear
            }


            else
            {
                // restart
                SceneManager.LoadScene("Main" + manager.stage);
            }



            //Find함수는 CPU 함수 부하를 초래할 수 있어서 피하는 것을 권장


        }







    }
}




