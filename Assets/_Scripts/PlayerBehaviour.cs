using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Joystick joystick;
    public float joystickSensitivity;
    public float horizontalForce;
    public float jumpForce;
    public bool isGrounded;

    
    private Rigidbody2D rigidBody2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _Move();
    }

    private void _Move()
    {
        if(isGrounded)
        {
            if (joystick.Horizontal > joystickSensitivity)
            {
                //move to right
                rigidBody2D.AddForce(Vector2.right * horizontalForce * Time.deltaTime);
                spriteRenderer.flipX = false;
                animator.SetInteger("AnimState", 1);
            }
            else if (joystick.Horizontal < -joystickSensitivity)
            {
                //move to left
                rigidBody2D.AddForce(Vector2.left * horizontalForce * Time.deltaTime);
                spriteRenderer.flipX = true;
                animator.SetInteger("AnimState", 1);
            }
            else if (joystick.Vertical > joystickSensitivity)
            {
                //Jump
                rigidBody2D.AddForce(Vector2.up * jumpForce * Time.deltaTime);
            }
            else
            {
                animator.SetInteger("AnimState", 0);
            }
        }

      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
