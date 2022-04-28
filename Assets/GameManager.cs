using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int cnt;
    // Start is called before the first frame update
    void Start()
    {
        cnt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = new RaycastHit();


            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                GameObject mouseHit = hit.transform.gameObject;
                if (mouseHit.CompareTag("Tangerine"))
                {
                    Destroy(mouseHit);
                    GameManager.cnt += 1;
                    Debug.Log(GameManager.cnt);
                }
            }
        }
    }
}
