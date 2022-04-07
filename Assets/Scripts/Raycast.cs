using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    RaycastHit raych;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 3, Color.red);
        if(Physics.Raycast(transform.position, transform.forward * 3, out raych))
        {
            if(raych.transform.name.Contains("cat"))
            {
                Destroy(raych.transform.gameObject);
                int score = GameManager.instance.catCatchScore += 1;
                Debug.Log("Score : "+ score.ToString());
            }
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name.Contains("cat"))
        {
            Destroy(collision.gameObject);
            int score = GameManager.instance.catCatchScore += 1;
            Debug.Log("Score : " + score.ToString());
        }
    }
}
