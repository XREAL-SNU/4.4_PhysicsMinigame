using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private bool _InGame;
    private Vector3 yDirection = new Vector3(1.73205f, 1, 0);
    private Vector3 xDirection = new Vector3(0, 0, 1);
    private float ySpeed = 0.1f;
    private float xSpeed = 0.1f;
    public void CameraInGame(){
        transform.position = new Vector3(0, 5, 5);
        transform.rotation = Quaternion.Euler(20, 90, 0);
        _InGame = true;
    }
    public void CameraOutGame(){
        transform.position = new Vector3(5, 7.88f, 5);
        transform.rotation = Quaternion.Euler(60, 90, 0);
        _InGame = false;
    }

    void Awake(){
        CameraOutGame();
    }

    void FixedUpdate(){
        if(!_InGame){
            Vector3 ViewPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            if(ViewPosition.x<0.1 && transform.position.z < 7){
                transform.position += (xDirection*xSpeed);
            }
            else if(ViewPosition.x>0.9 && transform.position.z > 3){
                transform.position -= (xDirection*xSpeed);
            }
            else if(ViewPosition.y<0.1 && transform.position.y > 7){
                transform.position -= (yDirection*ySpeed);
            }
            else if(ViewPosition.y>0.9 && transform.position.y < 12){
                transform.position += (yDirection*ySpeed);
            }
        }
    }
}
