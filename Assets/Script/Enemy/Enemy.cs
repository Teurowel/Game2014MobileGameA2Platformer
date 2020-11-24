//SourceFileName : Enemy.cs
//Author's name : Doosung Jang
//Studnet Number : 101175013
//Date last Modified : Nov.23, 2020
//Program description : This script handles basic enemy's variables and behaviour
//Revision History : Nov.23, 2020 Created, added ground checking, movement, attacking
//                   Nov.24, 2020 Added death system

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb = null; //player's rigid body
    Animator animator = null;
    Stats stats = null;

    [Header("Attribute")]
    public float moveSpeed = 1.5f;
    [SerializeField] int score = 50;

    [Header("GroundDetection")]
    [SerializeField] Transform groundDetectionPoint; //ground detection start point
    [SerializeField] float groundDetectionRayLength = 2.0f;
    [SerializeField] LayerMask groundLayer;

    Vector2 moveDir = new Vector2(1.0f, 0.0f); //enemy's movement direction
    bool isFacingRight = true;


    [Header("Attack")]
    [SerializeField] Transform attackCirclePos = null; //Circle's center position that will detect enemies
    [SerializeField] float attackCircleRadius = 0.0f; //Circle's radius
    [SerializeField] LayerMask playerLayer; //player layer
    bool attackCool = true;
    [SerializeField] float attackSpeed = 1.0f; //attack per second
    bool isAttacking = false; //TO prevent moving when player is attacking
    [SerializeField] float attackFirstDelay = 0.3f; //How much time does it take to initiate attack?

    Player player = null;

    bool hasDead = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        animator.SetFloat("HorizontalSpeed", moveDir.x);

        player = FindObjectOfType<Player>();

        //Add callback functions
        AnimatorEventReceive aer = GetComponentInChildren<AnimatorEventReceive>();
        if(aer != null)
        {
            aer.onAttackAnimFinished.AddListener(OnAttackAnimFinished);
            aer.onAttackCalculation.AddListener(OnAttackCalculation);
        }

        stats = GetComponent<Stats>();
        if (stats != null)
        {
            stats.onDeath.AddListener(OnDeath);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (hasDead == false)
        {
            float distance = CheckPlayerDistance();

            //If distance between player and enemy is less than attack radius, attack
            if (distance <= attackCircleRadius)
            {
                Attack();
            }
            //or just move
            else
            {
                //If enemy is not attacking...
                if (isAttacking == false)
                {
                    //basic move
                    Vector2 tempVel = rb.velocity;
                    tempVel.x = moveDir.x * moveSpeed;
                    rb.velocity = tempVel;

                    GroundDetection();
                }
            }

            animator.SetFloat("HorizontalSpeed", rb.velocity.sqrMagnitude);
        }
    }

    float CheckPlayerDistance()
    {
        float distance = 0.0f;
        if (player != null)
        {
            distance = Vector3.Distance(player.transform.position, attackCirclePos.position);
        }

        return distance;
    }

    void Attack()
    {
        //If distance between player and attack point is less than attack radius
        if(attackCool == true)
        {
            isAttacking = true;
            attackCool = false;
            rb.velocity = Vector2.zero;


            //After attack first dealy time, initiate attack
            Invoke("TriggerAttack", attackFirstDelay);
        }
    }

    void TriggerAttack()
    {
        if (hasDead == false)
        {
            animator.SetTrigger("Attack");

            Invoke("ResetAttackCool", attackSpeed);
        }
    }

    void ResetAttackCool()
    {
        attackCool = true;
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

      
        //Draw attack circle
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackCirclePos.position, attackCircleRadius);
    }

    void OnAttackAnimFinished()
    {
        isAttacking = false;
    }

    void OnAttackCalculation()
    {
        //Cast circle to detect enemies
        Collider2D playerToDamage = Physics2D.OverlapCircle(attackCirclePos.position, attackCircleRadius, playerLayer);
        if (playerToDamage != null)
        {
            Stats playerStats = playerToDamage.gameObject.GetComponent<Stats>();
            if (playerStats != null)
            {
                Debug.Log("Damage Player");
                playerStats.GetDamage(stats.damage);
            }
        }
    }

    void OnDeath()
    {
        //Set death trigger
        animator.SetTrigger("Death");

        //Disable collider
        GetComponent<Collider2D>().enabled = false;

        //Change rigidbody type to kenematic
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

        //disable enemy script
        hasDead = true;

        //Add score
        GlobalData.instance.AddScore(score);

        //After 5 seconds, destroy enemy
        Invoke("DestroyEnemy", 5);
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
