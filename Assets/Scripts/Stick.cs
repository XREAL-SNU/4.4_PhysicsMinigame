using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    private bool shouldShoot = false;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldShoot)
        {
            transform.Translate(4 * Time.deltaTime * Vector3.up);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(shit());
    }

    private void OnMouseDrag()
    {
        // 1. Rotate Y axis
        // 2. Rotate Z axis
        // Shoot after drag off in other method

        // Test: Shoot
    }

    private void OnMouseUp()
    {
        shouldShoot = true;
    }

    IEnumerator shit()
    {
        yield return new WaitForSeconds(0.0001f);
        shouldShoot = false;
    }
}
