using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
        
            Destroy(gameObject);

            gameManager.Instance.Clear();
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
