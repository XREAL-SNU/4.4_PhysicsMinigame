using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public GameObject cat;
    public List<GameObject> points;

    public int catCatchScore = 0;


    private int destinationNum;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        destinationNum = GameObject.Find("Player").GetComponent<TestNav>().targetPoint;
        foreach(var p in points)
        {
            if (p.name.Contains(destinationNum.ToString()) == true)
                continue;

            Instantiate(cat, p.transform.position,Quaternion.Euler(new Vector3(0, 90 * Random.Range(0, 3), 0)));


        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
