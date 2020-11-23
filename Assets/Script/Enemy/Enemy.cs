//SourceFileName : Enemy.cs
//Author's name : Doosung Jang
//Studnet Number : 101175013
//Date last Modified : Nov.23, 2020
//Program description : This script handles basic enemy's variables and behaviour
//Revision History : Nov.23, 2020 Created

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb = null; //player's rigid body

    [Header("Attribute")]
    public float moveSpeed = 1.5f;

    [Header("GroundDetection")]
    [SerializeField] Transform groundDetectionPoint; //ground detection start point
    [SerializeField] float groundDetectionRayLength = 2.0f;
    [SerializeField] LayerMask groundLayer;

    Vector2 moveDir = new Vector2(1.0f, 0.0f); //enemy's movement direction
    bool isFacingRight = true;

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    private void FixedUpdate()
    {
        //basic move
        Vector2 tempVel = rb.velocity;
        tempVel.x = moveDir.x * moveSpeed;
        rb.velocity = tempVel;

        GroundDetection();
    }

    //Check if there is ground ahead
    void GroundDetection()
    {
        //ray cast
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetectionPoint.position, Vector2.down, groundDetectionRayLength, groundLayer);
        
        //If we don't have ground ahead
        if(groundInfo.collider == null)
        {
            if(isFacingRight == true)
            {
                isFacingRight = false;
                Vector3 scale = transform.localScale;
                scale.x *= -1.0f;
                transform.localScale = scale;

                moveDir.x *= -1.0f;
            }
            else
            {
                isFacingRight = true;
                Vector3 scale = transform.localScale;
                scale.x *= -1.0f;
                transform.localScale = scale;

                moveDir.x *= -1.0f;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        //Draw attack circle
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(groundDetectionPoint.position, groundDetectionPoint.position + (Vector3.down * groundDetectionRayLength));
    }
}
