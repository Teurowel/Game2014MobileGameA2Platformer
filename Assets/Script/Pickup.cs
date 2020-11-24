//SourceFileName : Pickup.cs
//Author's name : Doosung Jang
//Studnet Number : 101175013
//Date last Modified : Nov.24, 2020
//Program description : This script handles Pickup
//Revision History : Nov.23, 2020 Created, Added OnTriggerEnter2D

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] int score = 10;
    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If collided with player
        if(collision.gameObject.CompareTag("Player"))
        {
            GlobalData.instance.AddScore(score);

            //Destroy self
            Destroy(gameObject);
        }
    }
}
