using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameStatusText(string gameStauts)
    {
        GameObject gameText = GameObject.Find("GameStatusText");
        if (gameStauts == "Catch")
            gameText.GetComponentInChildren<Text>().text = "벽에 붙어있는 모기를 클릭해서 잡으세요!!";
        else
            gameText.GetComponentInChildren<Text>().text = "날아다니는 모기의 위치를 확인하세요!!";
    }

    public void GameEndText(bool isWin)
    {
        GameObject gameText = GameObject.Find("GameStatusText");
        if (isWin)
            gameText.GetComponentInChildren<Text>().text = "모든 모기를 잡았습니다!이제 잘 수 있어요~";
        else
            gameText.GetComponentInChildren<Text>().text = "모기에게 피가 다 빨려버렸어요...";
    }

    public void GameRoundText(int round)
    {
        GameObject roundText = GameObject.Find("RoundText");
        if (round <= 3)
            roundText.GetComponentInChildren<Text>().text = $"Round {round}/3";
        else
            roundText.GetComponentInChildren<Text>().text = "GameOver";
    }
}
