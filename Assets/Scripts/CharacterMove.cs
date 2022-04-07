using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public float speed = 5;

    void Update()
    {
        //float hor = Input.GetAxis("Horizontal");        //왼쪽, 오른쪽 키 
        float ver = Input.GetAxis("Vertical");          //앞, 뒤 키

        transform.Translate(Vector3.forward * speed * ver * Time.deltaTime);      //이동
                                                                                  //transform.Rotate(Vector3.up * speed/5 * hor);

        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(0.0f, -1.0f, 0.0f);
        else if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(0.0f, 1.0f, 0.0f);

    }


}
