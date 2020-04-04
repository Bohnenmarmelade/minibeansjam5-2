using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GhostAIMovement : MonoBehaviour {
    
    public GhostController controller;

    [SerializeField] private float timeBetweenTargets = 3f; // in seconds
    [SerializeField] private float targetRadius = 5f;
    
    private float _nextTargetTime = 0f;
    private float _moveH = 0;
    private int sign = 1;

    private void Awake() {
        FindNextTarget();
    }
    

    private void FixedUpdate() {
        if (Time.time > _nextTargetTime) {
            FindNextTarget();
        }

        controller.Move(_moveH);
    }

    private void FindNextTarget() {
        //find a target in vicinity
        _moveH = targetRadius * sign;
        _nextTargetTime = Time.time + timeBetweenTargets;
        sign *= -1;
    }
}
