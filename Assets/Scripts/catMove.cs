using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catMove : MonoBehaviour
{
    RaycastHit raycast;
    private float raydistance = 0.1f;
    private float catMoveSpeed = 1f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward * raydistance, out raycast))
        {
            if (raycast.transform.name.Contains("Wall"))
                transform.Rotate(0, 0.3f, 0);
            else
                transform.Translate(Vector3.forward * catMoveSpeed * Time.deltaTime);
        }

        transform.Translate(Vector3.forward * catMoveSpeed * Time.deltaTime);

    }
}
