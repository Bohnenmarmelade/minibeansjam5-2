using System;
using UnityEngine;

public class CharController : MonoBehaviour {

    public ScytheController scytheController;
    
    [SerializeField] private float jumpForce = 100f;
    
    [SerializeField] private LayerMask groundLayerMask;							// A mask determining what is ground to the character
    [SerializeField] private Transform groundCheckTransform; // A position marking where to check if the player is grounded.
    [SerializeField] private float movementSmoothing;
    [SerializeField] private bool hasAirControl;
    [SerializeField][Range(0f,25f)] private float extraJumpGravity = 0.2f;
    
    private Animator _animator;
    private const float GroundedRadius = .2f;
    private bool _isGrounded;
    private bool _isFacingRight;
    private Vector3 _velocity = Vector3.zero;

    private Rigidbody2D _rigidbody2D;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int IsGrounded = Animator.StringToHash("isGrounded");
    private static readonly int Attack = Animator.StringToHash("Attack");

    private void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
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
            _isGrounded = false;
            _rigidbody2D.AddForce(new Vector2(0f, jumpForce));
        }

        if (attack) {
            _animator.SetTrigger(Attack);
            int sign = _isFacingRight ? 1 : -1;
            scytheController.Attack(sign);
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

    private void Flip() {
        _isFacingRight = !_isFacingRight;

        var t = transform;
        Vector3 s = t.localScale;
        s.x *= -1;
        t.localScale = s;
    }
}
