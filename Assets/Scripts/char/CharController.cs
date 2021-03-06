﻿using System;
using UnityEngine;
using Utils;

namespace Char {
    public class CharController : MonoBehaviour {

        public LevelController levelController;
        
        //public ScytheController scytheController;
        private AttackController _attackController;
        
        [SerializeField] private float jumpForce = 100f;
    
        [SerializeField] private LayerMask groundLayerMask;							// A mask determining what is ground to the character
        [SerializeField] private Transform groundCheckTransform; // A position marking where to check if the player is grounded.
        [SerializeField] private float movementSmoothing;
        [SerializeField] private bool hasAirControl;
        [SerializeField][Range(0f,25f)] private float extraJumpGravity = 0.2f;
        [SerializeField] [Range(0f, 0.5f)] private float jumpDelay = 0f;
    
        private Animator _animator;
        private const float GroundedRadius = .2f;
        private bool _isGrounded;
        private bool _isFacingRight;
        private Vector3 _velocity = Vector3.zero;
        private float _jumpTime = 0;
        private bool _shouldJump = false;
    
        private Rigidbody2D _rigidbody2D;
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int IsGrounded = Animator.StringToHash("isGrounded");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Jump = Animator.StringToHash("Jump");

        private void Awake() {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _attackController = GetComponent<AttackController>();
        }


        private void FixedUpdate() {
            _isGrounded = false;

            Collider2D[] colliders =
                // ReSharper disable once Unity.PreferNonAllocApi
                Physics2D.OverlapCircleAll(groundCheckTransform.position, GroundedRadius, groundLayerMask);
            foreach (var c in colliders) {
                if (c.gameObject != gameObject) {
                    _isGrounded = true;
                }
            }

        }

        public void Move(float move, bool jump, bool attack) {
            if (_isGrounded || hasAirControl) {
                Vector3 targetVelocity = new Vector2(move * 100f, _rigidbody2D.velocity.y);
                //smoothing movement
                _rigidbody2D.velocity =
                    Vector3.SmoothDamp(_rigidbody2D.velocity, targetVelocity, ref _velocity, movementSmoothing);

                if (move > 0 && !_isFacingRight) {
                    Flip();
                } else if (move < 0 && _isFacingRight) {
                    Flip();
                }
            }
        
            if (_isGrounded && jump) {
                this._jumpTime = Time.time + jumpDelay;
                _animator.SetTrigger(Jump);
                _shouldJump = true;
            }
        
            checkJump();

            if (attack) {
                _animator.SetTrigger(Attack);
                int sign = _isFacingRight ? 1 : -1;
                _attackController.Attack();
                EventManager.TriggerEvent(Events.SFX_SCYTHE, "");
            }

            if (_isGrounded) {
                _animator.SetBool(IsGrounded, true);
                _animator.SetFloat(Speed, Math.Abs(move));
            } else {
                _animator.SetBool(IsGrounded, false);
            
                //add extra gravity just for jumps, feels better
                Vector3 velocity = _rigidbody2D.velocity;
                velocity.y -= extraJumpGravity * Time.deltaTime;
                _rigidbody2D.velocity = velocity;
            }
        }
    
        private void checkJump() {
            if (_shouldJump && Time.time > _jumpTime) {
                _isGrounded = false;
                _rigidbody2D.AddForce(new Vector2(0f, jumpForce));
                _shouldJump = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Gate")) {
                levelController.GateEntered();
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.CompareTag("Gate")) {
                levelController.GateLeft();
            }
        }

        private void Flip() {
            _isFacingRight = !_isFacingRight;

            var t = transform;
            Vector3 s = t.localScale;
            s.x *= -1;
            t.localScale = s;
        }
    }
}
