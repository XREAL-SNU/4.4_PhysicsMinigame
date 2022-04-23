using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMove : MonoBehaviour
{
    public bool isEnd;
    //�ٵ� ������ ���� ����, ��Ʈ���Ϸ��� GameManager �ϳ� ������ �� �� ����,
    //�׷��� �Ǹ� singleton�� �ĵ� ������ ���� ��.?


    public float magnetStrength = 5f;
    public float distanceStrentch = 10f;
    public int magnetDirection = 1;
    public bool looseMagnet = true;

    private Transform trans;
    private Rigidbody thisRb;
    private Transform magnetTrans;
    private bool magnetInZone;

    private void Awake()
    {
        trans = transform;
        thisRb = trans.GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 directionToMagnet = magnetTrans.position - trans.position;
        float distance = Vector3.Distance(magnetTrans.position, trans.position);
        float magnetDistanceStr = (distanceStrentch / distance) + magnetStrength;
        thisRb.AddForce(magnetDistanceStr * (directionToMagnet * magnetDirection), ForceMode.Force);
    }







    //���� ���. �ٵ� stay��.. �ణ �ָ��ѵ�, ȣ�� �ð� �ٲ㼭 �� ���� ������ ���� ����� ����
    //�̰� �ҷ��͵� ������ ���� �� ���� ��.
    //���� ���� �������� �ϴ�, ���� �ٵ� ���� ��� ������ ¥�߰ڳ�.
    private void OnTriggerStay(Collider other)
    {
        if (isEnd == true)
        {
            switch (other.name)
            {
                case "Circle4":
                    break;

                    //�������� �˾Ƽ� �߰�.

            }
        }

        
    }
}
