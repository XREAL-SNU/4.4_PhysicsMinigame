using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour
{
    float speed = 0.02f;

    private void Update()
    {
        float ofs = speed * Time.time;
        transform.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(ofs, -1);
    }
}
