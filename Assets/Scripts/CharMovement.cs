using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : MonoBehaviour {
    [Range(0.0f, 10.0f)]
    public float movementSpeed = 1f;

    public Animator animator;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Attack = Animator.StringToHash("Attack");
    

    // Start is called before the first frame update
    void Start() {
        this.animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update() {
        Vector3 pos = transform.position;
        animator.SetFloat(Speed, 0f);
        
        if (Input.GetKey ("w")) {
            animator.SetFloat(Speed, 1f);
            pos.y += movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey ("s")) {
            animator.SetFloat(Speed, 1f);
            pos.y -= movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey ("d")) {
            animator.SetFloat(Speed, 1f);
            pos.x += movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey ("a")) {
            animator.SetFloat(Speed, 1f);
            pos.x -= movementSpeed * Time.deltaTime;
        }
        
        if (Input.GetKey("space")) {
            animator.SetTrigger(Attack);
        }
             
        transform.position = pos;
    }
}
