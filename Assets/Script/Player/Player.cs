//SourceFileName : Player.cs
//Author's name : Doosung Jang
//Studnet Number : 101175013
//Date last Modified : Nov.10, 2020
//Program description : This script handles player such as player animation, movement, attack etc...
//Revision History : Nov.10, 2020 Created, Added simple movement
//                   Nov.23, 2020, Added jumping, Falling animation system, Added attack

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
[DefaultExecutionOrder(-100)] //ensure this script runs before all other player scripts to prevent laggy input
public class Player : MonoBehaviour
{
    #region Variables

    Rigidbody2D rb = null; //player's rigid body
    CapsuleCollider2D capsuleCollider2D = null; //Player's capsule collider
    Animator animator = null;
    SpriteRenderer spriteRenderer = null;
    Stats stats = null;

    //[SerializeField] bool debugMode = false;

    [Header("Attribute")]
    public float moveSpeed = 3.0f;
    public float jumpForce = 10f; //How strong does player jump

    Vector2 moveDir = Vector2.zero; //player's movement direction
    bool isFacingRight = true;

    bool shouldJump = false; //Check if player should jump
    //bool canShoot = true; //Check if player can shoot projectile

    //[Header("Projectile")]
    //public int maxNumOfProjectile = 10; //Max number of projectile
    //public GameObject projectilePrefab = null; //Prefab for projectile
    //Queue<Projectile> listOfProjectile = null; //Queue for projectile pool
    //[SerializeField] float projectileSpawnDistance = 1f; //How far is projectile spanwed from player?
    //Projectile loadedProjectile = null; //projectile that is wating for shooting
    //Vector3 shootingLine; //Direction for loaded projectile

    //bool isFacingRight = true; //Is character facing right side? for Characte flip

    [SerializeField] LayerMask tileLayerMask = 0; //Used to check if player is on ground

    // to Get SFX sound name 
    //[SerializeField]
    //private string ShootSound;
    //[SerializeField]
    //private string JumpSound;


    //[Header("Interact")]
    //[SerializeField] float interactRadius = 5f;
    //[SerializeField] LayerMask interactLayer;

    //Camera mainCam = null;

    [Header("Joystick")]
    [SerializeField] Joystick joystick = null; //joystick for player movement
    [SerializeField] float joystickHorizontalSensitivity = 0f; //minimum sensitiviy for joystick
    [SerializeField] float joystickVerticalSensitivity = 0f; //minimum sensitiviy for joystick

    [Header("Attack")]
    [SerializeField] Transform attackCirclePos = null; //Circle's center position that will detect enemies
    [SerializeField] float attackCircleRadius = 0.0f; //Circle's radius
    [SerializeField] LayerMask enemyLayer; //enemy layer
    bool attackCool = true;
    [SerializeField] float attackSpeed = 1.0f; //attack per second
    bool isAttacking = false; //TO prevent moving when player is attacking
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();

        //Add callback functions
        AnimatorEventReceive aer = GetComponentInChildren<AnimatorEventReceive>();
        if (aer != null)
        {
            aer.onAttackAnimFinished.AddListener(OnAttackAnimFinished);
            aer.onAttackCalculation.AddListener(OnAttackCalculation);
        }


        stats = GetComponent<Stats>();
        //animator = GetComponentInChildren<Animator>();

        //Create queue for projectile pool
        //listOfProjectile = new Queue<Projectile>();
        //for (int i = 0; i < maxNumOfProjectile; ++i)
        //{
        //    GameObject projectile = Instantiate(projectilePrefab);

        //    //Set owner of this projectile
        //    projectile.GetComponent<Projectile>().owner = this;

        //    //Set layer 
        //    //if (gameObject.layer == (int)USER_LAYER.PLAYER)
        //    //{
        //    //    projectile.layer = (int)USER_LAYER.PLAYER_PROJECTILE;
        //    //}
        //    //else if (gameObject.layer == (int)USER_LAYER.OPPONENT)
        //    //{
        //    //    projectile.layer = (int)USER_LAYER.OPPONENT_PROJECTILE;
        //    //}

        //    //Add to pool
        //    listOfProjectile.Enqueue(projectile.GetComponent<Projectile>());
        //}

        //mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking == false)
        {
            HandleInput();
        }
    }

    private void FixedUpdate()
    {
        //Jump
        if (shouldJump == true)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            //rb.velocity = Vector2.ClampMagnitude(rb.velocity, 10);
            shouldJump = false; 
            Debug.Log("Jump!");
        }

        //Change player's velocity
        Vector2 tempVel = rb.velocity;
        tempVel.x = moveDir.x * moveSpeed;
        rb.velocity = tempVel;

        //Set vertical speed for animator
        animator.SetFloat("VerticalSpeed", rb.velocity.y);

        //Check if animation is playing jumping, if true, check we are on ground and set jumping back to idle
        if(animator.GetBool("IsJumping") == true)
        {
            if(IsPlayerOnGround() == true)
            {
                animator.SetBool("IsJumping", false);
            }
        }

        //If we just fall...
        if(rb.velocity.y < -1.0f)
        {
            animator.SetBool("IsJumping", true);
        }
    }

    void HandleInput()
    {
        //Move to right
        if (Input.GetKey(KeyCode.D) || joystick.Horizontal > joystickHorizontalSensitivity)
        {
            moveDir.x = 1;
            //spriteRenderer.flipX = false;
            if (isFacingRight == false)
            {
                Vector3 scale = transform.localScale;
                scale.x *= -1.0f;
                transform.localScale = scale;
                isFacingRight = true;
            }

            animator.SetFloat("HorizontalSpeed", 1.0f);
        }
        //Move to left
        else if (Input.GetKey(KeyCode.A) || joystick.Horizontal < -joystickHorizontalSensitivity)
        {
            moveDir.x = -1;
            //spriteRenderer.flipX = true;
            if (isFacingRight == true)
            {
                Vector3 scale = transform.localScale;
                scale.x *= -1.0f;
                transform.localScale = scale;
                isFacingRight = false;
            }

            animator.SetFloat("HorizontalSpeed", 1.0f);
        }
        else
        {
            moveDir.x = 0f;
            animator.SetFloat("HorizontalSpeed", 0.0f);
        }


        //Jump
        if (Input.GetKeyDown(KeyCode.Space) || joystick.Vertical > joystickVerticalSensitivity)
        {
            //Only can jump if player is on ground and not loaded projectile
            if (IsPlayerOnGround() == true && shouldJump == false)
            {
                //SoundManager.instance.PLaySE(JumpSound);
                shouldJump = true;
                animator.SetBool("IsJumping", true);
            }
        }

        //Attack
        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }

        ////Interact
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    //Find any interactable object within circle
        //    Collider2D result = Physics2D.OverlapCircle(transform.position, interactRadius, interactLayer);
        //    if (result != null)
        //    {
        //        //Call interact interface function
        //        IInteractable comp = result.gameObject.GetComponent<IInteractable>();
        //        if (comp != null)
        //        {
        //            comp.Interact();
        //        }
        //    }
        //}
    }

    public void Attack()
    {
        //Can only attack when player is on ground
        if (IsPlayerOnGround() == true && attackCool == true)
        {         
            //Play animation
            animator.SetTrigger("Attack");

            attackCool = false;
            Invoke("ResetAttackCool", attackSpeed);

            isAttacking = true;
            moveDir.x = 0f;
        }
    }

    void ResetAttackCool()
    {
        attackCool = true;
    }

    bool IsPlayerOnGround()
    {
        //Do capsule cast to downward of player so that it checks if player is on ground
        RaycastHit2D result = Physics2D.CapsuleCast(capsuleCollider2D.bounds.center, capsuleCollider2D.bounds.size, CapsuleDirection2D.Vertical, 0f, Vector2.down, 0.05f, tileLayerMask);

        //Debug.Log(result.collider);

        bool check = false;

        if(result.collider != null && rb.velocity.y <= 0f)
        {
            Debug.Log("Ground");
            check = true;
        }

        return check;
    }

    private void OnDrawGizmosSelected()
    {
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
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackCirclePos.position, attackCircleRadius, enemyLayer);
        if (enemiesToDamage != null)
        {
            //Deal all enemies inside circle
            for (int i = 0; i < enemiesToDamage.Length; ++i)
            {
                Stats enemyStats = enemiesToDamage[i].gameObject.GetComponent<Stats>();
                if (enemyStats != null)
                {
                    enemyStats.GetDamage(stats.damage);
                }
            }
        }
    }
}
