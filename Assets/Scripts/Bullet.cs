using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 yDirection = new Vector3(1.73205f, 1, 0);
    void FixedUpdate()
    {
        transform.position += yDirection*0.1f;
        if(transform.position.y > 30)
            Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("Enemy")){
            Destroy(gameObject);
        }
    }
}
