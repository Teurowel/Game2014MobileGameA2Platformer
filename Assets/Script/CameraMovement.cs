//SourceFileName : CameraMovement.cs
//Author's name : Doosung Jang
//Studnet Number : 101175013
//Date last Modified : Nov.10, 2020
//Program description : This script handles camera following player
//Revision History : Nov.10, 2020 Created, Added simple following

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] GameObject target = null; //target to follow

    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    private void LateUpdate()
    {
        Vector3 newPos = transform.position;
        newPos.x = target.transform.position.x;
        newPos.y = target.transform.position.y;

        transform.position = newPos;
    }
}
