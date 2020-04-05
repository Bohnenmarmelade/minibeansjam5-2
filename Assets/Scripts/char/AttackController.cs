using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Char {
    public class AttackController : MonoBehaviour {
        public LevelController levelController;
        public LayerMask enemyLayers;
        public Transform hitbox;

        [SerializeField] private float attackRange = .5f;
        [SerializeField] private float attackDuration = (float)5/24;
        [SerializeField] private float attackDelay = (float)5/24;
        [SerializeField] private Vector3 hitboxOffset = new Vector3(0f, .65f, 0f);
        [SerializeField] private Vector3 hitboxAttackOffset = new Vector3(10.45f, -.15f, 0f);
        [SerializeField] private float hitboxSpeed = 5f;

        private float _attackStartTime;
        private float _attackEndTime;
        private bool _isAttack = false;
        private List<Collider2D> currentTargets;

        private void Awake() {
            currentTargets = new List<Collider2D>();
        }

        private void OnDrawGizmos() {
            Gizmos.DrawWireSphere(hitbox.transform.position, attackRange);
            Debug.Log(hitbox.transform.localPosition);
        }

        private void Update() {
            if (_isAttack && Time.time > _attackStartTime) {
                float step = hitboxSpeed * Time.deltaTime;
                hitbox.transform.localPosition = Vector3.MoveTowards(hitbox.transform.localPosition, hitboxAttackOffset, step);
                CheckHits();
                if (Time.time > _attackEndTime) {
                    
                    _isAttack = false;
                    hitbox.transform.localPosition = hitboxOffset;
                    currentTargets.Clear();
                }

            }
        }

        private void CheckHits() {
            Collider2D[] targets = Physics2D.OverlapCircleAll(hitbox.position, attackRange, enemyLayers);
            foreach (Collider2D collider in targets) {
                if (!currentTargets.Contains(collider)) {
                    currentTargets.Add(collider);
                    levelController.targetHit(collider);
                }
            }
        }

        public void Attack() {
            _attackStartTime = Time.time + attackDelay;
            _attackEndTime = _attackStartTime + attackDuration;
            _isAttack = true;
        }
    
    }
}
