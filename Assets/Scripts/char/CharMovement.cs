using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CharMovement : MonoBehaviour {

    public CharController controller;

    [Range(1f, 10f)]
    public float movementSpeed;

    private float _horizontalMove = 0f;
    private bool _jump = false;
    private bool _attack = false;

    private void Update() {
        _horizontalMove = Input.GetAxisRaw("Horizontal") * movementSpeed;
        
        
        if (Input.GetButtonDown("Jump")) {
            _jump = true;
        }

        if (Input.GetButtonDown("Attack")) {
            _attack = true;
        }
    }

    private void FixedUpdate() {
        controller.Move(_horizontalMove * Time.fixedDeltaTime, _jump, _attack);
        _jump = false;
        _attack = false;
    }
}
