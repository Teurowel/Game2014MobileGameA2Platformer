//SourceFileName : ResetZone.cs
//Author's name : Doosung Jang
//Studnet Number : 101175013
//Date last Modified : Nov.15, 2020
//Program description : This script handles reset zone that reset player to designated position when player falls.
//Revision History : Nov.14, 2020 Created, Added OnTriggerEnter2D that if player enter this trigger, reset player

//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class ResetZone : MonoBehaviour
{
    public Transform currentSavePoint = null;

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
        if(collision.gameObject.CompareTag("Player"))
        {
            if (currentSavePoint != null)
            {
                Vector3 pos = currentSavePoint.position;
                pos.y += 2.0f;
                collision.gameObject.transform.position = pos;

                if(GlobalData.instance != null)
                {
                    GlobalData.instance.DecreaseLife();
                }
            }
        }
    }
}
