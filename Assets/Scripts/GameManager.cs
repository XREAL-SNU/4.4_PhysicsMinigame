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
        Catch,
    }
    private GameState gameState = GameState.Catch;
    private delegate void GameStateHandler(GameState gameState);
    private GameStateHandler _gameStateHandler;

    private int _mosquitoCount;
    private int _round;
    private delegate void RoundHandler(int round);
    private RoundHandler _roundHandler;

    private delegate void GameEndHandler();
    private GameEndHandler _gameEndHandler;

    void Start()
    {
        InvokeRepeating("ChangeState", 0, 6);
        _mosquitoCount = 5;
        _round = 0;
    }

    private void ChangeState()
    {
        gameState = gameState == GameState.Track ? GameState.Catch : GameState.Track;
        GameStateNotify();
        if (gameState == GameState.Track)
        {
            _round++;
            RoundNotify();
        }
    }


    public void GameStateNotify()
    {
        _gameStateHandler(gameState);

        var su = new SceneUI();
        su.GameStatusText(gameState.ToString());
    }

    public void RoundNotify()
    {
        _roundHandler(_round);
        var su = new SceneUI();
        su.GameRoundText(_round);
    }

    public void GameEndNotify(bool isWin)
    {
        _gameEndHandler();

        var su = new SceneUI();
        su.GameEndText(isWin);
    }

    public void AddMosquito(RandomFlyer randomFlyer)
    {
        _gameStateHandler += new GameStateHandler(randomFlyer.GameStateUpdate);
        _roundHandler += new RoundHandler(randomFlyer.RoundUpdate);
        _gameEndHandler += new GameEndHandler(randomFlyer.IsEndUpdate);
    }
    public void AddPlayer(Player player)
    {
        _gameStateHandler += new GameStateHandler(player.GameStateUpdate);
        _roundHandler += new RoundHandler(player.RoundUpdate);
    }
}
