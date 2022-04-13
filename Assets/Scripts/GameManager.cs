using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance() {
        return _instance;
    }

    private GameBox gameBox;

    private void Awake() {
        if(_instance == null) {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            if(this != _instance) {
                Destroy(this.gameObject);
            }
        }   
    }

    public void addGameBox(GameBox _gameBox) {
        gameBox = _gameBox;
        Debug.Log(gameBox);
    }

    public GameBox getGameBox() {
        return gameBox;
    }
}
