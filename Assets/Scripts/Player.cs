using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, Observer
{

    protected string gameState;
    protected int gameRound;
    protected int mosquitoCount = 5;
    public void GameStateUpdate(GameManager.GameState gameState)
    {
        this.gameState = gameState.ToString();
        Debug.Log($"{this.name} recognizes {gameState}");
    }

    public void RoundUpdate(int gameround)
    {
        this.gameRound = gameround;
    }

    public bool IsDead()
    {
        if (gameRound > 3)
        {
            return true;
        }
        return false;
    }

    public bool IsWin()
    {
        if (mosquitoCount == 0)
        {
            return true;
        }
        return false;
    }


    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance().AddPlayer(this.GetComponent<Player>());
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == "Catch" && Input.GetMouseButtonDown(0))
        {
            Vector3 p = Input.mousePosition;

            Ray cast = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            int layerMask = 1 << LayerMask.NameToLayer("Mosquitos");

            if (Physics.Raycast(cast, out hit, 10F, layerMask))
            {
                Debug.Log($"{hit.transform.gameObject.name} got hit");
                Destroy(hit.transform.gameObject);
                mosquitoCount--;

            }
        }
        else
        {
            if (IsDead())
            {
                GameManager.Instance().GameEndNotify(false);

            }
            if (IsWin())
            {
                GameManager.Instance().GameEndNotify(true);
            }
        }
    }
}
