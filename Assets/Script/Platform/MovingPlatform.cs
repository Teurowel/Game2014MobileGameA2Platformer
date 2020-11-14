//SourceFileName : MovingPlatform.cs
//Author's name : Doosung Jang
//Studnet Number : 101175013
//Date last Modified : Nov.14, 2020
//Program description : This script handles moving platform
//Revision History : Nov.14, 2020 Created, Added simple moving

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform movingPos1 = null; //position where platform should go
    [SerializeField] Transform movingPos2 = null; //position where platform should go
    Transform nextPos = null;

    [SerializeField] float speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        nextPos = movingPos1;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == movingPos1.position)
        {
            nextPos = movingPos2;
        }
        else if (transform.position == movingPos2.position)
        {
            nextPos = movingPos1;
        }

        //if (Vector3.Distance(transform.position, movingPos1.position) <= 1.0f)
        //{
        //    nextPos = movingPos2;
        //}
        //else if (Vector3.Distance(transform.position, movingPos2.position) <= 1.0f)
        //{
        //    nextPos = movingPos1;
        //}

        transform.position = Vector3.MoveTowards(transform.position, nextPos.position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If collided with player, set player's parent to this platform so taht player can move along platform
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = gameObject.transform;  
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }
    }
}
