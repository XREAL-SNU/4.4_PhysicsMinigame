using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour 
{
    public float count;

    void Start()
    {
        Destroy(gameObject, count);
    }


}
