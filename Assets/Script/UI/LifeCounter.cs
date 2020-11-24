//SourceFileName : LifeCounter.cs
//Author's name : Doosung Jang
//Studnet Number : 101175013
//Date last Modified : Nov.14, 2020
//Program description : This script handles life count ui
//Revision History : Nov.14, 2020 Created

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCounter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GlobalData.instance != null)
        {
            GlobalData.instance.onLifeChanged.AddListener(OnLifeChanged);
        }
        else
        {
            Debug.LogWarning("GlobalData is null, check LifeCounter");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnLifeChanged()
    {
        if (transform.childCount != 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
