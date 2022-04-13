using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetected : MonoBehaviour
{
    private MeshRenderer _renderer;
    
    void Start() {
        _renderer = GetComponent<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.name == "Cube") {
            GameBox gameBox = GameManager.Instance().getGameBox();
            gameBox.Shaking();
            Furniture furniture = gameBox.transform.Find("Furniture").gameObject.GetComponent<Furniture>();
            furniture.Jumping();
            Debug.Log("Collision");
        }
    }
}
