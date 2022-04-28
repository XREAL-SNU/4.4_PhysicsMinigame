using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("arrow");
            // collision.gameObject.transform.rotation = Quaternion.Euler();
            //gameObject.transform.rotation.z ³»Àû
          //  Quaternion ball = gameObject.transform.rotation;
            //float x = Vector3.Dot(Vector3.right, new Vector3(ball.x, ball.y, ball.z));
           // float y = Vector3.Dot(Vector3.up, new Vector3(ball.x, ball.y, ball.z));

           // collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(x, y, 0);
            Destroy(gameObject);

        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
