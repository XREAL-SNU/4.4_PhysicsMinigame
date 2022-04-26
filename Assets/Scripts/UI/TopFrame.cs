using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TopFrame : MonoBehaviour {
    public TextMeshProUGUI taltitude, treached;
    public GameObject player;

    private int lasta = 0, lastr = 0;
    public float deltar = 0f;

    void Update() {
        deltar = Mathf.Lerp(deltar, LevelGenerator.playerGroundy, 0.1f * 60f * Time.deltaTime);
        if(lasta != Mathf.FloorToInt(player.transform.position.y)) {
            lasta = Mathf.FloorToInt(player.transform.position.y);
            taltitude.text = lasta + "m";
        }

        if(lastr != Mathf.FloorToInt(deltar)) {
            lastr = Mathf.FloorToInt(deltar);
            treached.text = lastr + "m";
        }
    }
}
