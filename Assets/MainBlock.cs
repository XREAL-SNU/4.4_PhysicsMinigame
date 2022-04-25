using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainBlock : MonoBehaviour
{


    float Max = 5.5f;
    float sensitivityX = 1f;
    float positionX = 0;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        Update_MousePosition();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {

        }
    }

    private void Update_MousePosition()
    {
        //Vector2 mousePos = Input.mousePosition;
        positionX += Input.GetAxis("Mouse X") * sensitivityX;

        if (positionX >= Max)
            positionX = Max;
        else if(positionX <= -Max)
            positionX = -Max;

        gameObject.transform.position = new Vector3(positionX, gameObject.transform.position.y, gameObject.transform.position.z);

     
    }
}
