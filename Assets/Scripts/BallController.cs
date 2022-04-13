using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 mOffset;
    private float mzCoord;
    private Rigidbody rb;
    private bool isRolled;
    public GameObject fallArea;

    private int temp=0;
    private void OnMouseDown() 
    {
        mzCoord=Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset=gameObject.transform.position-GetMouseWorldPos();
    }
  
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint=Input.mousePosition;

        //z coordinate of game object on screen
        mousePoint.z =mzCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
      private void OnMouseDrag() 
    {
        transform.position=GetMouseWorldPos()+mOffset;
    }
    private void OnMouseUp() 
    {
        rb=GetComponent<Rigidbody>();
        rb.useGravity=true;
        
        isRolled=true;
    }

    private void Update() 
    {
        if(isRolled)
        {
            RollaBall();
        }
    }
    void RollaBall()
    {        
        Vector3 force =UIManager.Instance().clickedTime *transform.forward;
        rb.AddForce(force);
    }
    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.name == "Floor")
        {
            //GameManager.Instance().numCase=1;
            if(temp==0)
                GameManager.Instance().CallCoroutine();
            temp++;
        }
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.name == "FallArea")
        {
            if(temp==0)
                GameManager.Instance().CallCoroutine();  
        }
    }
}
