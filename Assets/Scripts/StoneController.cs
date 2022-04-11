using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneController : MonoBehaviour
{

    [SerializeField]
    private float _speed = 10.0f;
    public float energy = 0;

    private Rigidbody _rb;
    private Vector3 _movementInput;
    private float _energyInput;

    //private bool _isBeingPressed=true;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        //_movementInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //_rb.MovePosition(_rb.position + _movementInput * _speed * Time.deltaTime);

        _energyInput = Input.GetAxis("Jump");
        energy += _energyInput * _speed * Time.deltaTime;
    }



    //later on, make this as charge-explode system
}


