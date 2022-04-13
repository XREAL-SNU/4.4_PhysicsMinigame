using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneUI : MonoBehaviour
{

    private void Start()
    {
        GameManager.Instance().addSceneUI(this.GetComponent<SceneUI>());
        GameObject ClearPanel = transform.Find("ClearPanel").gameObject;
        ClearPanel.SetActive(false);
    }

    public void Init()
    {

    }

    public void Clear() {
        GameObject ClearPanel = transform.Find("ClearPanel").gameObject;
        ClearPanel.SetActive(true);
    }
}
