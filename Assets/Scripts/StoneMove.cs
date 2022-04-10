using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMove : MonoBehaviour
{
    public bool isEnd;
    //근데 어차피 스톤 복제, 컨트롤하려면 GameManager 하나 만들어야 될 것 같고,
    //그렇게 되면 singleton을 파도 나쁘지 않을 듯.?


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







    //점수 계산. 근데 stay라서.. 약간 애매한데, 호출 시간 바꿔서 턴 끝난 다음에 점수 계산할 때만
    //이거 불러와도 나쁘지 않을 것 같긴 해.
    //각자 팀의 점수여야 하니, 으음 근데 점수 계산 로직도 짜야겠네.
    private void OnTriggerStay(Collider other)
    {
        if (isEnd == true)
        {
            switch (other.name)
            {
                case "Circle4":
                    break;

                    //나머지도 알아서 추가.

            }
        }

        
    }
}
