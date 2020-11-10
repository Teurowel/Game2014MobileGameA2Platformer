﻿using System.Collections;
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

    //[SerializeField] bool debugMode = false;

    [Header("Attribute")]
    public float moveSpeed = 3.0f;
    public float jumpForce = 10f; //How strong does player jump
    //public float shootCoolTime = 0.5f; //Projectile shoot cool time
    public int hp = 10;

    Vector2 moveDir = Vector2.zero; //player's movement direction

    bool shouldJump = false; //Check if player should jump
    //bool canShoot = true; //Check if player can shoot projectile

    //[Header("Projectile")]
    //public int maxNumOfProjectile = 10; //Max number of projectile
    //public GameObject projectilePrefab = null; //Prefab for projectile
    //Queue<Projectile> listOfProjectile = null; //Queue for projectile pool
    //[SerializeField] float projectileSpawnDistance = 1f; //How far is projectile spanwed from player?
    //Projectile loadedProjectile = null; //projectile that is wating for shooting
    //Vector3 shootingLine; //Direction for loaded projectile

    bool isFacingRight = true; //Is character facing right side? for Characte flip

    [SerializeField] LayerMask tileLayerMask; //Used to check if player is on ground

    // to Get SFX sound name 
    //[SerializeField]
    //private string ShootSound;
    //[SerializeField]
    //private string JumpSound;


    //[Header("Interact")]
    //[SerializeField] float interactRadius = 5f;
    //[SerializeField] LayerMask interactLayer;

    //Camera mainCam = null;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
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
        HandleInput();
    }

    private void FixedUpdate()
    {
        //Jump
        if (shouldJump == true)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            shouldJump = false;
        }

        //Change player's velocity
        Vector2 tempVel = rb.velocity;
        tempVel.x = moveDir.x * moveSpeed;
        rb.velocity = tempVel;
        //animator.SetInteger("Direction", (int)moveDir.x);
    }

    void HandleInput()
    {
        //Horizontal move
        if (Input.GetKey(KeyCode.A))
        {
            moveDir.x = -1;

            //Characte flip
            if (isFacingRight == true)
            {
                isFacingRight = false;
                transform.Rotate(0f, 180f, 0f);
            }

        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveDir.x = 1;

            //Characte flip
            if (isFacingRight == false)
            {
                isFacingRight = true;
                transform.Rotate(0f, 180f, 0f);
            }

        }
        else
        {
            moveDir.x = 0f;

        }


        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Only can jump if player is on ground and not loaded projectile
            if (IsPlayerOnGround() == true)
            {
                //SoundManager.instance.PLaySE(JumpSound);
                shouldJump = true;
            }
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

        //MouseInputHandle();
    }

    void ShootProjectile()
    {
        ////Get projectile from list
        //if (listOfProjectile.Count != 0)
        //{
        //    Projectile projectile = listOfProjectile.Dequeue();

        //    //Activate projectile
        //    projectile.gameObject.SetActive(true);

        //    //Set projectile in front of player
        //    Vector3 forwardVec = -transform.right;
        //    Vector3 upwardVec = transform.up;
        //    projectile.transform.position = transform.position + (forwardVec * projectileSpawnXOffset) + (upwardVec * projectileSpawnYOffset);

        //    //Set projectile move direction
        //    projectile.SetProjectileDirection(forwardVec);

        //    //Can't shoot projectile continousely
        //    canShoot = false;
        //    Invoke("ResetShootCoolDown", shootCoolTime);
        //}
    }

    //void ResetShootCoolDown()
    //{
    //    canShoot = true;
    //}

    ////Return projectile to pool
    //public void ReturnProjectile(Projectile projectile)
    //{
    //    listOfProjectile.Enqueue(projectile);
    //}

    bool IsPlayerOnGround()
    {
        //Do capsule cast to downward of player so that it checks if player is on ground
        RaycastHit2D result = Physics2D.CapsuleCast(capsuleCollider2D.bounds.center, capsuleCollider2D.bounds.size, CapsuleDirection2D.Vertical, 0f, Vector2.down, 0.1f, tileLayerMask);

        //Debug.Log(result.collider);

        return (result.collider != null);
    }

    private void OnDrawGizmosSelected()
    {
        //if (debugMode)
        //{
        //    //Draw interactable circle
        //    Gizmos.color = Color.yellow;
        //    Gizmos.DrawWireSphere(transform.position, interactRadius);
        //}
    }

    //private void MouseInputHandle()
    //{
    //    //Left mouse down for spawn projectile
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        //Can only shoot when player is on ground
    //        if (canShoot == true && IsPlayerOnGround())
    //        {
    //            //Get projectile from list
    //            if (listOfProjectile.Count != 0)
    //            {
    //                loadedProjectile = listOfProjectile.Dequeue();

    //                //Activate projectile
    //                loadedProjectile.gameObject.SetActive(true);
    //            }
    //        }
    //    }

    //    //If holding mouse left button, calculate loaded projectile's position
    //    if (Input.GetMouseButton(0))
    //    {
    //        if (loadedProjectile != null)
    //        {
    //            //GEt mouse position of world
    //            Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
    //            mousePos.z = 0f;

    //            if (debugMode)
    //            {
    //                //Draw shooting line
    //                Debug.DrawLine(transform.position, mousePos, Color.red);
    //            }


    //            //Get forward vector of player
    //            Vector3 forwardVector = -transform.right;

    //            //Calculate shooting line
    //            shootingLine = mousePos - transform.position;
    //            shootingLine.Normalize();

    //            //Get angle between forward vector and shooting line
    //            float angleBetween = Vector2.Angle(forwardVector, shootingLine);

    //            //Set projectile based on shooting line
    //            if (angleBetween <= 45)
    //            {
    //                loadedProjectile.transform.position = transform.position + (shootingLine * projectileSpawnDistance);
    //            }
    //        }
    //    }

    //    //If release mosue left button, shoot loaded projectile
    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        if (loadedProjectile != null)
    //        {
    //            loadedProjectile.SetProjectileDirection(shootingLine);
    //            loadedProjectile = null;

    //            //Can't shoot projectile continousely
    //            canShoot = false;
    //            Invoke("ResetShootCoolDown", shootCoolTime);
    //        }
    //    }
    //}
}