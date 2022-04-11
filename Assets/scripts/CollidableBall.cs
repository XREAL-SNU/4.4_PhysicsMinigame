using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidableBall : MonoBehaviour
{
    private int score = 0;
    private float rotationSpeed = 10f;
    private Vector3 revolutionRotateVector = new Vector3(0, 0, 1);
    public GameObject centerBall;

    // Start is called before the first frame update
    void Start()
    {
        centerBall = GameObject.FindGameObjectWithTag("CenterBall");
    }

    // Update is called once per frame
    void Update()
    {
        rotateSelf();
        revolution();
    }

    // 자전하는 함수 
    private void rotateSelf()
    {
        transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
    }

    private void revolution()
    {
        transform.RotateAround(centerBall.transform.position, revolutionRotateVector, Time.deltaTime * rotationSpeed);
    }
    public int getScore()
    {
        return score;
    }
}
