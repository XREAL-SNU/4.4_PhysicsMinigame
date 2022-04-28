using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerGenerator : MonoBehaviour
{
    private int block_num = 5;
    private GameObject[] b_array = new GameObject[5];
    private int block_count = 0;
    private bool _InGame;
    private bool created;
    public GameObject blocker;

    void Start(){
        _InGame = GameManager.Instance.getInGame();
        created = false;
    }
    public void setInGame(){
        _InGame = true;
    }

    public Vector3 ClickedPosition(){
        if(!_InGame){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            int layerMask = 1 << LayerMask.NameToLayer("Floor");
            if(Physics.Raycast(ray, out hit, float.MaxValue, layerMask))
                return hit.point;
        }
        return new Vector3(0,0,0);
    }

    public void BuildBlocker(){
        if(block_count>=block_num){
            Debug.Log("Cannot create more!");
            return;
        }
        Vector3 createPosition = ClickedPosition();
        b_array[block_count] = Instantiate(blocker);
        b_array[block_count++].transform.position = createPosition;
        Debug.Log("create a blocker");
    }

    void Update(){
        if(!_InGame){
            if(Input.GetMouseButtonDown(0) && !created){
                BuildBlocker();
                created = true;
            }
            else if(Input.GetMouseButtonUp(0)){
                created = false;
            }
        }
    }
}
