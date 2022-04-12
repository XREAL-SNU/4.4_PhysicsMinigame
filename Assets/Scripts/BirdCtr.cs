using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCtr : MonoBehaviour
{
    public Transform oCtr;

    float _birdSpeed;
    bool isDead = false;

    void Start()
    {
        SetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        float move = _birdSpeed * Time.deltaTime;

        if (!isDead)
        {
            transform.Translate(Vector3.right * move, Space.World);
        }
        else
        {
            transform.Translate(Vector3.down * move, Space.World);
        }

        if(transform.position.x>7 || transform.position.y < -3)
        {
            Destroy(gameObject);
        }
    }

    void SetPosition()
    {
        _birdSpeed = Random.Range(2, 3f);
        transform.position = new Vector3(-7, Random.Range(1.4f, 4.7f), 8);
    }

    void DeadBird()
    {
        isDead = true;

        transform.eulerAngles = new Vector3(0, 0, 180); //180µµ È¸Àü

        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.2f, 8);
        Quaternion rot = Quaternion.identity;

        rot.eulerAngles = new Vector3(0, 0, 0);
        Instantiate(oCtr, pos, rot);
    }
}
