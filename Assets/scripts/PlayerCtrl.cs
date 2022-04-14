using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCtrl : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator anim;
    private new Transform transform;

    private Vector3 movedirection;

    
    void Start() {
        
        anim=GetComponent<Animator>();
        transform=GetComponent<Transform>();
        
    }

 void Update() {




    

        
        if (movedirection!=Vector3.zero){
            transform.rotation=Quaternion.LookRotation(movedirection);
            transform.Translate(Vector3.forward*Time.deltaTime*4.0f);

         
        }
    }

    void OnMove(InputValue value)
    {
        Vector2 direction = value.Get<Vector2>();
       
        
        movedirection= new Vector3(direction.x,0,direction.y);
        anim.SetFloat("Movement",direction.magnitude);
        Debug.Log($"move {direction.x} and {direction.y}");  
        
    }

    // Update is called once per frame
    void OnAttack()
    {
        Debug.Log($"catch the food!!");
        anim.SetTrigger("Attack");
        
    }

  
    
}
