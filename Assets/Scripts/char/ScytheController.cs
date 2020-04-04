using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheController : MonoBehaviour {

    public LevelController levelController;
    
    [SerializeField][Range(0.1f, 10f)] private float attackSpeed = 1f;
    [SerializeField][Range(0.1f, 2f)] private float attackDuration = 0.5f;
    [SerializeField][Range(0.1f, 4f)] private float attackRange = 1.5f;
    private Collider2D _collider;

    private bool _isAttack = false;
    private float _attackEndTime = 0f;
    private Vector3 _attackTarget = Vector3.zero;
    private float _sign = 1;
    
    private void Awake() {
        _collider = GetComponent<CircleCollider2D>();
    }

    public void Attack(int sign) {
        this._attackTarget = attackRange * new Vector3(sign, 0,0);
        this._isAttack = true;
        this._attackEndTime = Time.time + attackDuration;
    }

    private void Update() {
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }

    private void FixedUpdate() {
        if (_isAttack) {
            float step = attackSpeed * Time.fixedDeltaTime ;
            var position = transform.position;
            position = Vector3.MoveTowards(position, _attackTarget + position, step* _sign);
            transform.position = position;
        }

        if (_isAttack && (Time.time > this._attackEndTime)) {
            var transform1 = _collider.transform;
            transform1.position = Vector3.zero;
            transform1.localPosition = Vector3.zero;
            var transform2 = transform;
            transform2.position = Vector3.zero;
            transform2.localPosition = Vector3.zero;
            _isAttack = false;
        } 
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (_isAttack) {
            levelController.targetHit(other);
        }

    }
}
