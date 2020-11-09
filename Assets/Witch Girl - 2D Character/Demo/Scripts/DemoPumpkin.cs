using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoPumpkin : MonoBehaviour
{
    public int life = 5;
    public float movePower = 1f;
    //private Animator anim;
    private Vector3 movement;
    private int movementFlag = 0; //0:Idle, 1: Left, 2:Right
    private float timer = 100f;
    private float waitingTime = 0.5f;
    private bool pullingAttacked = false;
    

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        StartCoroutine("ChangeMovement");
    }

    // Update is called once per frame
    void Update()
    {
        //Move();
        timer+=Time.deltaTime;
        if(pullingAttacked && timer>waitingTime){
            pullingAttacked=false;
            pullHurt(1);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        DemoObjectType demoObjectType = other.GetComponent<DemoObjectType>();
        string type="null";
        if(demoObjectType!=null){
            type=demoObjectType.type;
        }


        if(type == "attack"){
            Hurt(1);
        }

        if(type == "pullingAttack"){
            timer = 0;
            pullingAttacked = true;
        }
    }

    void Move(){
        Vector3 moveVelocity = Vector3.zero;
        if(movementFlag==1){
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(-1,1,1);
        }
        if(movementFlag==2){
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(1,1,1);
        }

        transform.position+=moveVelocity*movePower*Time.deltaTime;        
    }
    IEnumerator ChangeMovement(){
        movementFlag=Random.Range(0,3);
        
        yield return new WaitForSeconds(1f);

        StartCoroutine("ChangeMovement");
    }
    private void Die(){
        StopCoroutine("ChangeMovement");

        //anim.SetTrigger("death");

        //SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        //renderer.flipY=true;

        BoxCollider2D coll= GetComponent<BoxCollider2D>();
        coll.enabled=false;

        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        Vector2 dieVelocity = new Vector2(0,-10f);
        rigid.AddForce(dieVelocity,ForceMode2D.Impulse);

        Destroy(gameObject,5f);
    }
    private void Hurt(int damage){
        life-=damage;
        if(life<1){
            Die();
            return;
        }
        //anim.SetTrigger("hurt");

        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;
        Vector2 hurtVelocity = new Vector2(10f,0f);
        rigid.AddForce(hurtVelocity,ForceMode2D.Impulse);
        
    }
        private void pullHurt(int damage){
        life-=damage;
        if(life<1){
            Die();
            return;
        }
        //anim.SetTrigger("hurt");

        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;
        Vector2 hurtVelocity = new Vector2(-20f,0f);
        rigid.AddForce(hurtVelocity,ForceMode2D.Impulse);
        
    }
}
