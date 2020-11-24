//SourceFileName : Hazard.cs
//Author's name : Doosung Jang
//Studnet Number : 101175013
//Date last Modified : Nov.24, 2020
//Program description : This script handles hazard platform
//Revision History : Nov.24, 2020 Created, Added OnCOllisionEnter2D for colliding with player

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            ResetZone rz = FindObjectOfType<ResetZone>();
            if (rz != null)
            {
                rz.ResetPlayer();
            }
            else
            {
                Debug.LogWarning("ResetZone is null, Check Hazard");
            }
        }
    }
}
