using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, Subject
{

    private static GameManager _instance;
    public static GameManager Instance()
    {
        return _instance;
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (this != _instance)
            {
                Destroy(this.gameObject);
            }
        }
    }


    public enum GameState
    {
        Track,
        Catch
    }
    private GameState gameState = GameState.Catch;
    private delegate void GameStateHandler(GameState gameState);
    private GameStateHandler _gameStateHandler;

    void Start()
    {
        InvokeRepeating("ChangeState", 0, 6);
    }

    private void ChangeState()
    {
        gameState = gameState == GameState.Track ? GameState.Catch : GameState.Track;
        GameStateNotify();
    }

    public void GameStateNotify()
    {

        _gameStateHandler(gameState);
    }

    public void AddMosquito(RandomFlyer randomFlyer)
    {
        _gameStateHandler += new GameStateHandler(randomFlyer.GameStateUpdate);
    }
    public void AddPlayer(Player player)
    {
        _gameStateHandler += new GameStateHandler(player.GameStateUpdate);
    }
}
