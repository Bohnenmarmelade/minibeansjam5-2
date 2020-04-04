using System;
using UnityEngine;

public class GhostController : MonoBehaviour {
    [SerializeField][Range(0.1f, 2f)] private float movementSpeed = 1f;
    [SerializeField] private float movementSmoothing = 0.1f;
    [SerializeField][Range(0.1f, 10f)] private float wobbleSpeed = 4f;
    [SerializeField][Range(0.1f, 10f)] private float wobbleAmplitude = 7f;
    private Animator _animator;

    public float WobbleSpeed {
        get => wobbleSpeed;
        set => wobbleSpeed = value;
    }

    public float WobbleAmplitude {
        get => wobbleAmplitude;
        set => wobbleAmplitude = value;
    }

    private bool _isFacingRight;
    private Vector3 _velocity = Vector3.zero;
    
    
    private Rigidbody2D _rigidbody2D;
    private static readonly int Dead = Animator.StringToHash("Dead");

    private void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update() {
        Vector3 pos = _rigidbody2D.transform.position;
        Vector3 newPos = new Vector2(pos.x,
            (float) Math.Sin(Time.time * wobbleSpeed) * Time.fixedDeltaTime * wobbleAmplitude + pos.y);
        _rigidbody2D.transform.position = newPos;
    }

    public void Move(float moveH) {
        var velocity = _rigidbody2D.velocity;
        Vector3 targetVelocity = new Vector3(moveH, _velocity.y);
        //smoothing movement
        _rigidbody2D.velocity =
            Vector3.SmoothDamp(velocity, targetVelocity, ref _velocity, movementSmoothing);

        if (moveH > 0 && !_isFacingRight) {
            Flip();
        } else if (moveH < 0 && _isFacingRight) {
            Flip();
        }
    }

    public void Die() {
        _animator.SetTrigger(Dead);
        Destroy(gameObject, .5f);
    }

    private void Flip() {
        _isFacingRight = !_isFacingRight;

        var t = transform;
        Vector3 s = t.localScale;
        s.x *= -1;
        t.localScale = s;
    }
}
