using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClayCtr : MonoBehaviour
{
    float _craySpeed; //접시속도
    float rotZ; //접시각도
    float gravity; //추락속도

    int[] dir = { -1, 1 };
    int dirX; //이동방향

    void Start()
    {
        SetPosition();    
    }

    
    void Update()
    {
        gravity -= 1.3f * Time.deltaTime;//접시의 이동속도 감소
        //접시의 이동방향과 속도를 설정


        //접시를 회전을 주어 월드좌표로 이동
        Vector3 move = new Vector3(_craySpeed * dirX, gravity, 0) * Time.deltaTime;
        transform.Translate(move, Space.World);

        if(Mathf.Abs(transform.position.x)>8 || transform.position.y < -3)
        {
            Destroy(gameObject);
            //Gun.miss++;
        }
    }

    void SetPosition()
    {
        //랜덤으로 접시속도 설정하기
        _craySpeed = Random.Range(3, 5f);

        gravity = 2f;

        dirX = dir[Random.Range(0, 2)];

        float posX = -8 * dirX; //화면의 좌우위치 설덩
        float posY = Random.Range(2.5f, 3);
        transform.position = new Vector3(posX, posY, 9);
        transform.eulerAngles = new Vector3(-50, 0, Random.Range(10, 20f) * dirX);
    }
}
