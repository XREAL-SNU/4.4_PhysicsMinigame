using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject Canvas;
    public void disableUI(){
        Destroy(Canvas);
    }
}
