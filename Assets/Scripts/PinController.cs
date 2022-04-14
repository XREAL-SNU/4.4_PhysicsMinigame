using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinController : MonoBehaviour
{
    private CapsuleCollider collider;
    private int temp=0;
    private void Start() 
    {
        collider=this.gameObject.GetComponent<CapsuleCollider>();
    }
    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.name == "Floor")
        {
            if(temp==0)
            {
                collider.height=1;
                collider.center=new Vector3(0, 0.5f, 0);
                temp++;
            }
            else if(temp==1)
            {
                Debug.Log("CollisionEnter");
                collider.enabled=false;
                GameManager.Instance().score += 100;
                UIManager.Instance().scoreText.text="  Score: "+GameManager.Instance().score;     
            }
        }
    }
}
