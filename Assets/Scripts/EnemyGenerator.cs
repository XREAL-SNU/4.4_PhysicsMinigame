using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemy;
    private GameObject[] e_array = new GameObject[5];
    private int enemy_num;
    private int enemy_count;
    float timer;
    int waitingTime;
    void Start(){
        enemy_num = 5;
        enemy_count = 0;
        timer = 0.0f;
        waitingTime = 1;
    }
    void Update()
    {
        if(GameManager.Instance.getInGame()){
            timer += Time.deltaTime;
            if(timer > waitingTime){
                createEnemy();
                timer = 0;
            }
        }
    }
    private void createEnemy(){
        if(enemy_count < enemy_num){
            e_array[enemy_count] = Instantiate(enemy);
            e_array[enemy_count++].transform.position = transform.position;
        }
    }
}
