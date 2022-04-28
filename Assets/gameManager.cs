using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class gameManager : MonoBehaviour
{

    int stageNum = 0;
    int chanceNum =0 ;
    public GameObject[] stages;
    public GameObject gameover,clear
        ;
    GameObject ball,stage;
    public int[] chances;
    public Text chanceUI;


    public static gameManager Instance;
    // Start is called before the first frame update
    void Start()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this);
                

        gameInit();
    }

     public void useChance()
     {
        chanceNum--;

        if(chanceNum < 0)
        {
            gameOver();
        }
        else
        {
            chanceUI.text = chanceNum.ToString();
        }
     



     }
    
   public void gameInit()
    {

        

        chanceNum = chances[stageNum];
        chanceUI.text = chanceNum.ToString();

        Time.timeScale = 1;

        if(ball != null)
        {
            Destroy(ball);
        }
        if (stage != null)
        {
            Destroy(stage);
        }

        stage = (GameObject)Instantiate(Resources.Load("Prefab/stage"+ (stageNum+1).ToString() ));
        ball = (GameObject)Instantiate(Resources.Load("Prefab/Player Ball"));
    }

   


    void gameOver()
    {
        gameover.SetActive(true);

        Time.timeScale = 0;
        Destroy(ball);

    }

    public void Clear()
    {
        stageNum++;

        if (stageNum == chances.Length)
        {
            gameClear();
        }
        else
        gameInit();
    }

    void gameClear()
    {
        clear.SetActive(true);
        Time.timeScale = 0;
        Destroy(ball);
    }

}
