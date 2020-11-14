//SourceFileName : FallingPlatform.cs
//Author's name : Doosung Jang
//Studnet Number : 101175013
//Date last Modified : Nov.14, 2020
//Program description : This script handles falling platform
//Revision History : Nov.14, 2020 Created, Added simple falling

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    Rigidbody2D rb = null;

    [SerializeField] float fallingTime = 2.0f; //How much time does platform need to fall after player collided with it

    Vector3 originPos = Vector3.zero; //Original position of platform, will be used to reset platform



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        originPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= -15.0f)
        {
            //Destroy(gameObject);
            ResetPlatform();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If collided with player, start coroutine that makes platform fall after designated seconds
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke("StartFalling", fallingTime);
        }
    }

    void StartFalling()
    {
        if(rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    void ResetPlatform()
    {
        transform.position = originPos;

        if(rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity = new Vector3(0f, 0f, 0f);
        }
        
    }
}
