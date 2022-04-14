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
    private SceneUI sceneUI;
    private bool isClear = false;

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

    public void Update() {
        if(isClear) {
            sceneUI.Clear();
        }
    }

    public void addGameBox(GameBox _gameBox) {
        gameBox = _gameBox;
    }

    public void addInterface(string InterfaceName, Interface _interface) {
        interfaceList.Add(InterfaceName, _interface);
    }

    public void addSceneUI(SceneUI _sceneUI) {
        sceneUI = _sceneUI;
        Debug.Log(sceneUI);
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

    public void setClear(bool clear) {
        isClear = clear;
    }
}
