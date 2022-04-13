using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{



    public float rotateSpeed;


    void Update()
    {
        transform.Rotate(Vector3.up  * rotateSpeed * Time.deltaTime );
        
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Hodu") {


            Hodu hodu = other.GetComponent<Hodu>();
            // 여기서 Hodu는 스크립트 이름 



            hodu.itemCount++; 



            gameObject.SetActive(false);
            // SetActive(bool): 오브젝트 활성화 비활성, 여기서는 비활성화





        }
    }


}
