using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance()
    {
        return instance;
    }
    private void Awake()
    {
        if(instance==null)
        {
            instance=this;
            DontDestroyOnLoad(this.gameObject);//다른 씬으로 넘어갈 때 사라지지 않기
            DontDestroyOnLoad(canvas);
        }
        else
        {
            if(this != instance)
            {
                Destroy(this.gameObject);//여러개 존재한다면 삭제하기
            }
        }
    }

    private void OnEnable() 
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Init();
    }
    public Canvas canvas;
    public Slider powerSlider;
    public Text scoreText;
    public GameObject blank;

    private bool isClicked;
    public float clickedTime;
    private void Init() 
    {
        if(SceneManager.GetActiveScene().name=="GameScene")
        {
            powerSlider.gameObject.SetActive(true);
            scoreText.gameObject.SetActive(true);
            
            Button[] buttons=GameObject.FindObjectsOfType<Button>();
            for(int i=0; i<buttons.Length; i++)
            {
                buttons[i].gameObject.SetActive(false);
            }
        }
    }
    private void Update() 
    {
        //Check input
        if(Input.GetMouseButtonDown(0))
        {
            isClicked=true;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            isClicked=false;
            blank.SetActive(true);
        }

        //SliderBar Update
        if(isClicked) //가속도를 주고 싶은데 어떻게 함
        {
            clickedTime +=Time.deltaTime;
            UpdateSlider(clickedTime);
        }
        else
        {
            clickedTime -=Time.deltaTime;
            UpdateSlider(clickedTime);
            if(clickedTime<0)
                clickedTime=0f;
        }
    }

    private void UpdateSlider(float time)
    {
        if(SceneManager.GetActiveScene().name=="GameScene")
            powerSlider.value=time;
    }
    public void LoadNextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void ExitGame()
    {
       #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying=false;//play모드를 false로
        #elif UNITY_WEBPLAYER
            Application.OpenURL("http://google.com"); //구글 웹으로 전환
        #else
            Application.Quit();
        #endif
    }
}
