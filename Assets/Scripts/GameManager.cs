using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _round;
    private int _gold;
    // If true, the game is over.
    private bool _GameOver;
    private bool _InGame;

    void Awake()
    {
        init();
    }

    public void init(){
        _round = 0;
        _gold = 100;
        _GameOver = false;
        _InGame = false;
    }

    public int getRound(){
        if(!_GameOver)
            return _round;
        return -1;
    }
    public void increaseRound(){
        if(!_GameOver)
            _round += 1;
    }
    public int getGold(){
        if(!_GameOver)
            return _gold;
        return -1;
    }
    public bool changeGold(int change){
        _gold += change;
        if(_gold < 0){
            _gold -= change;
            return false;
        }
        return true;
    }
    public bool getGameOver(){
        return _GameOver;
    }
    public void setGameOver(){
        _GameOver = true;
    }
    public bool getInGame(){
        return _InGame;
    }
    public void setInGame(bool set){
        _InGame = set;
    }
}
