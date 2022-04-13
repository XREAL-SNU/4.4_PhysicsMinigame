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
        if(collision.gameObject.name == "Brick") {
            Interface Brick = GameManager.Instance().getInterface("Brick");
            if(GameManager.Instance().getInterface("Brick").getIsEnoughHeight()) {
                GameBox gameBox = GameManager.Instance().getGameBox();
                gameBox.Shaking();
                Furniture furniture = gameBox.transform.Find("Furniture").gameObject.GetComponent<Furniture>();
                furniture.Jumping();
                Brick.onCollision();
                Debug.Log("Collision");
            }
        }
    }
}
