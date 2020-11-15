//SourceFileName : Goal.cs
//Author's name : Doosung Jang
//Studnet Number : 101175013
//Date last Modified : Nov.14, 2020
//Program description : This script handles goal
//Revision History : Nov.14, 2020 Created, Added OnTriggerEnter2D that if player enter this trigger, game over

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
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
        if(collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("GameOverScreen");
        }
    }
}
