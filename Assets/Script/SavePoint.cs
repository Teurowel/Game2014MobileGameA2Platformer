//SourceFileName : SavePoint.cs
//Author's name : Doosung Jang
//Studnet Number : 101175013
//Date last Modified : Nov.24, 2020
//Program description : This script handles save point
//Revision History : Nov.24, 2020 Created, Added OnTriggerEnter2D that if collided with player ,change reset point

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
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
        //If it was player
        if(collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<ResetZone>().currentSavePoint = gameObject.transform;

            if (SoundManager.instance != null)
            {
                SoundManager.instance.Play("SavePointSFX");
            }
            else
            {
                Debug.Log("Should start from main menu scene since SoundManager is only created in menu scene");
            }
        }
    }
}
