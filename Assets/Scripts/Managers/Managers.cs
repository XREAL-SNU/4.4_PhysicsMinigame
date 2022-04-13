using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public static SoundManager Sound { get { return Instance._sound; }}
    public static UIManager UI { get { return Instance._ui; } }

    Text volumeText;
    public Plane[] _planes = new Plane[10];
    int curVolume = 0;

    AudioSource audioSource;

    void Start()
    {
        s_instance = this;
        _input.Clear();
        string[] objects = Enum.GetNames(typeof(Define.Objects));
        GameObject parent;
        foreach (string obj in objects)
        {
            parent = new GameObject(obj+"s");
            for(int i = 0; i < 10; i++)
            {
                GameObject child = _resource.Instantiate(obj, parent.transform);
                child.transform.position = new Vector3(UnityEngine.Random.Range(-5, 5), 
                     0.01f, UnityEngine.Random.Range(-5, 5));
            }

        }

        parent = new GameObject("Planes");
        for (int i = 0; i < 10; i++)
        {
            GameObject child = _resource.Instantiate("Plane", parent.transform);
            child.transform.position = new Vector3(0.0f, i+1.5f, 0.0f);
            child.name += i.ToString();
            _planes[i] = child.GetComponent<Plane>();
        }

        UI_Scene volumeUI = _ui.ShowSceneUI<UI_Scene>("VolumeUI");
        volumeText = Util.FindChild<Text>(volumeUI.gameObject, "curVolume");
        SoundManager.SetSystemVolume(0, SoundManager.VolumeUnit.Scalar);

        audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        audioSource.volume = 0.0f;
    }

    void Update()
    {
        _input.OnUpdate();


        int i = 0;
        for (; i < 10; i++)
        {
            if (!_planes[i]._isContact)
                break;
        }
        if (curVolume != i)
        {
            SoundManager.SetSystemVolume((double)0.1 * i, SoundManager.VolumeUnit.Scalar);
            curVolume = i;
            volumeText.text = $"Volume: {i*10}";
            audioSource.volume = (float)0.1 * i;
        }
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
