using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//SceneManager 장면을 관리하는 기본 클래스 

public class Hodu : MonoBehaviour
{


    public float jumpPower;
    public int bitcoinItemCount;
    public GameManagerLogic manager;
    bool isJump;
    Rigidbody rigid;
    AudioSource audio;




    void Awake()
    {
        isJump = false;
        rigid = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    private void Update()
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
        // y축은 점프여서 일단 0으로 세

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
            isJump = false;
    }


    private void OnTriggerEnter(Collider other)
    {



        //비트코인 카운트 추가

        if (other.name == "Bitcoin")
        {
            bitcoinItemCount++;
        }



        // 만약에 코인을 먹으면 소리 나오	

        if (other.tag == "Coins")
        {
            audio.Play();
            //플레이어가 아이템을 먹고나면 오디오 제생

            other.gameObject.SetActive(false);
            // SetActive(bool): 오브젝트 활성화 비활성, 여기서는 비활성화

        }

        else if (other.name == "Finish Point") {


            if (bitcoinItemCount == manager.TotalItemCount)
            {

                SceneManager.LoadScene("Sucess");
                //game clear

            }


            else {

                // restart
                SceneManager.LoadScene("Fail");

            }



            //Find함수는 CPU 함수 부하를 초래할 수 있어서 피하는 것을 권장 





        }







    }
}




