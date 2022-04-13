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
    private Dictionary<string, Interface> interfaceList = new Dictionary<string, Interface>();

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

    public void addInterface(string InterfaceName, Interface _interface) {
        interfaceList.Add(InterfaceName, _interface);
    }

    public Interface getInterface(string interfaceName) {
        Interface interfaceOfName = null;
        if(interfaceList.ContainsKey(interfaceName)) {
            interfaceOfName = interfaceList[interfaceName];
        }
        return interfaceOfName;
    }

    public GameBox getGameBox() {
        return gameBox;
    }
}
