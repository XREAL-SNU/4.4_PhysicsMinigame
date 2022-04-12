using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance; // 유일성이 보장된다
    static Managers Instance { get { Init(); return s_instance; } } // 유일한 매니저를 갖고온다

    InputManager _input = new InputManager();
    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    SoundManager _sound = new SoundManager();
    UIManager _ui = new UIManager();

    public static InputManager Input { get { return Instance._input; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static UIManager UI { get { return Instance._ui; } }

    void Start()
    {
        s_instance = this;
        _input.Clear();
        string[] objects = Enum.GetNames(typeof(Define.Objects));
        foreach(string obj in objects)
        {
            GameObject parent = new GameObject(obj+"s");
            for(int i = 0; i < 10; i++)
            {
                GameObject child = _resource.Instantiate(obj, parent.transform);
                child.transform.position = new Vector3(UnityEngine.Random.Range(-5, 5), 
                     0.0f, UnityEngine.Random.Range(-5, 5));
            }

        }
             
    }

    void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {	
	}

    public static void Clear()
    {
        //Input.Clear();
        //Sound.Clear();
        //Scene.Clear();
        //UI.Clear();
        //Pool.Clear();
    }
}
