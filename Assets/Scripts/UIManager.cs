using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    
    public Text scoretxt;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
       
    public void ScoreSetting(int num)
    {
        scoretxt.text = $"Score : {num}";
    }
    
}
