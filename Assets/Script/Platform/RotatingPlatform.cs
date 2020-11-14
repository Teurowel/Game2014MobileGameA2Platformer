//SourceFileName : RotatingPlatform.cs
//Author's name : Doosung Jang
//Studnet Number : 101175013
//Date last Modified : Nov.14, 2020
//Program description : This script handles rotating platform
//Revision History : Nov.14, 2020 Created, Added simple rotating

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    [SerializeField] float speed = 2.0f; //rotation speed
    [SerializeField] float rotatingDelay = 1.0f; //how much time does it need to rotate after rotate 180

    float rotZ = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotZ += speed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotZ);
    }
}
