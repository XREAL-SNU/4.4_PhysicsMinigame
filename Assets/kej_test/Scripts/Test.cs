using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] GameObject cube;
    [SerializeField] GameObject largeCube;
    [SerializeField] GameObject temp;

    // Start is called before the first frame update
    void Start()
    {
        Collider cubeCollider = cube.GetComponent<Collider>();
        Collider largeCubeCollider = largeCube.GetComponent<Collider>();

        Debug.Log(cubeCollider.bounds.size/2);
        Debug.Log(largeCubeCollider.bounds.size/2);
        Debug.Log(largeCubeCollider.bounds.center);
        Debug.Log(cubeCollider.bounds.center);

        temp.transform.position = new Vector3(cubeCollider.bounds.center.x + cubeCollider.bounds.size.x/2,
            cubeCollider.bounds.center.y + cubeCollider.bounds.size.y/2,
            cubeCollider.bounds.center.z + cubeCollider.bounds.size.z/2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
