using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, Observer
{

    protected string gameState;

    public void GameStateUpdate(GameManager.GameState gameState)
    {
        this.gameState = gameState.ToString();
        Debug.Log($"{this.name} recognizes {gameState}");
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

            }
        }
    }
}
