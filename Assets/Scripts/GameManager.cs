using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance()
    {
        return instance;
    }
    private void Awake()
    {
        if(instance==null)
        {
            instance=this;
            DontDestroyOnLoad(this.gameObject);//다른 씬으로 넘어갈 때 사라지지 않기
        }
        else
        {
            if(this != instance)
            {
                Destroy(this.gameObject);//여러개 존재한다면 삭제하기
            }
        }
    }
  

    private Vector3 mOffset;
    private float mzCoord;
    
    private void OnMouseDown() 
    {
        mzCoord=Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset=gameObject.transform.position-GetMouseWorldPos();
    }
    private void OnMouseDrag() 
    {
        transform.position=GetMouseWorldPos()+mOffset;
    }
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint=Input.mousePosition;

        //z coordinate of game object on screen
        mousePoint.z =mzCoord;

        return Camera.main.ScreenToViewportPoint(mousePoint);
    }

    public GameObject ball;
    private GameObject ballInstant;
    private GameObject pinsInstant;

    private GameObject pins;
    public int score;
    // Start is called before the first frame update
    private Transform startPos;
    public int numCase=0;

    void Start()
    {
        score=0;
        startPos =GameObject.FindWithTag("StartPos").transform;
        ball=Resources.Load<GameObject>("Soccer Ball");
        pins=Resources.Load<GameObject>("Pins");
        StartCoroutine(SetNewGame());

    }

   
    public void CallCoroutine()
    {
        StartCoroutine(ResetGame());

    }
    
    IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(7.0f);

        StartCoroutine(SetNewGame());
        Destroy(ballInstant);
        Destroy(pinsInstant);
  
    }

    IEnumerator SetNewGame()
    {
        yield return new WaitForSeconds(2.0f);
     
        UIManager.Instance().blank.SetActive(false);
        ballInstant=Instantiate(ball);
        pinsInstant=Instantiate(pins);
    }

}
