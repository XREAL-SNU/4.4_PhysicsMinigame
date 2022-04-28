using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int _hp;
    void Awake(){
        _hp = 100;
    }
    public int hit(int damage){
        _hp -= damage;
        return _hp;
    }
    
    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("Player")){
            int remain = hit(10);
            if(remain<=0)
                Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("Bullet")){
            int remain = hit(20);
            if(remain<=0)
                Destroy(gameObject);
        }
    }
}
