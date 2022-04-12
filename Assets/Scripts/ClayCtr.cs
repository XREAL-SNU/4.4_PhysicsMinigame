using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClayCtr : MonoBehaviour
{
    float _craySpeed; //���üӵ�
    float rotZ; //���ð���
    float gravity; //�߶��ӵ�

    int[] dir = { -1, 1 };
    int dirX; //�̵�����

    void Start()
    {
        SetPosition();    
    }

    
    void Update()
    {
        gravity -= 1.3f * Time.deltaTime;//������ �̵��ӵ� ����
        //������ �̵������ �ӵ��� ����


        //���ø� ȸ���� �־� ������ǥ�� �̵�
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
        //�������� ���üӵ� �����ϱ�
        _craySpeed = Random.Range(3, 5f);

        gravity = 2f;

        dirX = dir[Random.Range(0, 2)];

        float posX = -8 * dirX; //ȭ���� �¿���ġ ����
        float posY = Random.Range(2.5f, 3);
        transform.position = new Vector3(posX, posY, 9);
        transform.eulerAngles = new Vector3(-50, 0, Random.Range(10, 20f) * dirX);
    }
}
