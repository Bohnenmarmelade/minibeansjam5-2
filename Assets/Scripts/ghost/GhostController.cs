using System;
using UnityEngine;

public class GhostController : MonoBehaviour {
    [SerializeField][Range(0.1f, 2f)] private float movementSpeed = 1f;
    [SerializeField] private float movementSmoothing = 0.1f;
    [SerializeField][Range(0.1f, 10f)] private float wobbleSpeed = 4f;
    [SerializeField][Range(0.1f, 10f)] private float wobbleAmplitude = 7f;
    
    private bool _isFacingRight;
    private Vector3 _velocity = Vector3.zero;
    
    
    private Rigidbody2D _rigidbody2D;
    
    private void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        Vector3 pos = transform.position;
        pos.y = (float) Math.Sin(Time.time * wobbleSpeed) * Time.fixedDeltaTime * wobbleAmplitude;
        transform.position = pos;
    }

    public void Move(float moveH) {
        var velocity = _rigidbody2D.velocity;
        Vector3 targetVelocity = new Vector3(moveH, velocity.y);
        //smoothing movement
        _rigidbody2D.velocity =
            Vector3.SmoothDamp(velocity, targetVelocity, ref _velocity, movementSmoothing);

        if (moveH > 0 && !_isFacingRight) {
            Flip();
        } else if (moveH < 0 && _isFacingRight) {
            Flip();
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
