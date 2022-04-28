using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreModule : MonoBehaviour {
    public static int highScore = 0;
    public static int highAltitude = 0;

    public float currentHighAlt = 0f;
    public PlayerControl pcon;
    public Canvas canvas;
    public GameObject highAltBorder;

    void Awake() {
        currentHighAlt = 0f;
        Load();
    }

    private void Start() {
        if (highAltitude >= 5) {
            highAltBorder.SetActive(true);
            highAltBorder.transform.position = Vector3.up * (highAltitude + 0.65f);
        }
        else highAltBorder.SetActive(false);
    }

    void Update() {
        if(pcon.transform.position.y > currentHighAlt) currentHighAlt = pcon.transform.position.y;
    }

    public void End() {
        int score = CalculateScore();
        
        if(Mathf.FloorToInt(currentHighAlt) > highAltitude) highAltitude = Mathf.FloorToInt(currentHighAlt);
        if(score > highScore) highScore = score;

        Save();
        DisplayUI(Mathf.FloorToInt(LevelGenerator.playerGroundy), Mathf.FloorToInt(currentHighAlt), MissileControl.missilesUsed, score);
        SceneManager.LoadScene("GameScene");
    }

    public int CalculateScore() {
        int sc = Mathf.FloorToInt(LevelGenerator.playerGroundy + Mathf.Max(0, currentHighAlt - LevelGenerator.playerGroundy) / 3f);
        sc = Mathf.Max(sc / 2, sc - MissileControl.missilesUsed * 3);
        return sc;
    }

    private void Save() {
        PlayerPrefs.SetInt("tb-hs", highScore);
        PlayerPrefs.SetInt("tb-ha", highAltitude);

        PlayerPrefs.Save();
    }

    private void Load() {
        highScore = PlayerPrefs.GetInt("tb-hs", 0);
        highAltitude = PlayerPrefs.GetInt("tb-ha", 0);

        Debug.Log("HighScore: " + highScore);
        Debug.Log("HighAlt: " + highAltitude + "m");
    }

    private void DisplayUI(int reached, int altitude, int shots, int score) {
        //todo
    }
}
