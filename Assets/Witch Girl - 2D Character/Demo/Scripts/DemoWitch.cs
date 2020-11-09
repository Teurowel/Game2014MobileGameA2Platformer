using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoWitch : MonoBehaviour
{
    public int life = 3;
    public float movePower = 10f;
    public float jumpPower = 15f; //Set Gravity Scale in Rigidbody2D Component to 5

    private Rigidbody2D rb;
    private Animator anim;
    Vector3 movement;
    private int direction=1;
    bool isJumping = false;
    public GameObject attack1;
    public GameObject attack2;

    private bool canControl = true;


    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();

        attack1.SetActive(false);
        attack2.SetActive(false);

    }

    private void FixedUpdate() {
        Attack1();
        Attack2();
        Jump();
        Run();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        DemoObjectType demoObjectType = other.GetComponent<DemoObjectType>();
        string type="null";
        if(demoObjectType!=null){
            type=demoObjectType.type;
        }
        if(type=="ground"){
            anim.SetBool("isJumping",false);
            if(life>0){
            canControl = true;
            }
        }
        if(type == "enemy" && canControl && !attack1.activeSelf&& !attack2.activeSelf) {
            Hurt(1);
            canControl = false;
        }
    }


    void Run(){
        Vector3 moveVelocity= Vector3.zero;
            anim.SetBool("isRunning",false);


        if(canControl && Input.GetAxisRaw("Horizontal")<0){
            direction= -1;
            moveVelocity = Vector3.left;

            transform.localScale = new Vector3(direction,1,1);
            anim.SetBool("isRunning",true);

        }
        if(canControl && Input.GetAxisRaw("Horizontal")>0){
            direction= 1;
            moveVelocity = Vector3.right;

            transform.localScale = new Vector3(direction,1,1);
            anim.SetBool("isRunning",true);

        }
        transform.position+=moveVelocity*movePower*Time.deltaTime;
    }
    void Jump(){
        if(canControl && Input.GetButtonDown("Jump")&&!anim.GetBool("isJumping")){
            isJumping=true;
            anim.SetTrigger("doJumping");
            anim.SetBool("isJumping",true);
        }
        if(!isJumping){
            return;
        }

        rb.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0,jumpPower);
        rb.AddForce(jumpVelocity,ForceMode2D.Impulse);

        isJumping=false;
    }
    private void Attack1(){
        if(canControl && Input.GetMouseButtonDown(0)){
            attack1.SetActive(true);
            anim.SetTrigger("attack1");
        }
        if(Input.GetMouseButtonUp(0)){
            attack1.SetActive(false);
        }
    }
    private void Attack2(){
        if(canControl && Input.GetMouseButtonDown(1)){
            attack2.SetActive(true);
            anim.SetTrigger("attack2");
        }
        if(Input.GetMouseButtonUp(1)){
            attack2.SetActive(false);
        }
    }
    private void Die(){
        anim.SetTrigger("death");

        BoxCollider2D coll= GetComponent<BoxCollider2D>();
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
    }
    private void Hurt(int damage){
        life-=damage;
        
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;
        Vector2 hurtVelocity = new Vector2(-15f,12f);
        rigid.AddForce(hurtVelocity,ForceMode2D.Impulse);
        if(life<1){
            Die();
            return;
        }
        anim.SetTrigger("hurt");
    }
    
}
