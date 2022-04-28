using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject GameManager;
    
    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("Enemy")){
            Debug.Log("GameOver!!");
            GameManager.GetComponent<GameManager>().setGameOver();
        }
    }
}
