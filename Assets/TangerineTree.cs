using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangerineTree : MonoBehaviour
{
    public GameObject _tangerinePrefab;

    private CharacterController _controller;
    private bool _isActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isActive)
            return;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            TangerineParty();
        }
    }

    private void TangerineParty()
    {
        StartCoroutine(RipedTangerine());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _controller = other.GetComponent<CharacterController>();
            _isActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _isActive = false;
    }

    IEnumerator RipedTangerine()
    {
        for(int i=0;i<10;i++)
        {
            for (int j = 0; j < 6; j++)
            {
                GameObject tangerine = Instantiate(_tangerinePrefab);
                tangerine.transform.position = new Vector3(UnityEngine.Random.Range(-3, 3), UnityEngine.Random.Range(8, 10), UnityEngine.Random.Range(-3, 3));
            }
            yield return new WaitForSeconds(1);
        }
    }
}
