//SourceFileName : BackgroundMoving.cs
//Author's name : Doosung Jang
//Studnet Number : 101175013
//Date last Modified : Nov.14, 2020
//Program description : This script handles moving background
//Revision History : Nov.14, 2020 Created, Added simple background scrolling

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMoving : MonoBehaviour
{
    float length, startpos;
    public GameObject cam;
    public float parallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    private void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = cam.transform.position.x * parallaxEffect;

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if(temp > startpos + length)
        {
            startpos += length;
        }
        else if(temp < startpos - length)
        {
            startpos -= length;
        }
    }
}
