using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    float _bulletSpeed = 60f;
    float delay = 0.5f; //총알 삭제 시간

    
    void Update()
    {
        float Move = _bulletSpeed * Time.deltaTime;

        transform.Translate(Vector3.forward * Move);

        delay -= Time.deltaTime;
        if (delay <= 0)
        {
            Destroy(gameObject);
        }
    }
}
