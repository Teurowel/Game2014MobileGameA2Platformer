﻿//SourceFileName : AnimatorEventReceive.cs
//Author's name : Doosung Jang
//Studnet Number : 101175013
//Date last Modified : Nov.23, 2020
//Program description : This script handles animator's event
//Revision History : Nov.23, 2020 Created, Added OnAttackAnimFinished that is called by animator event

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimatorEventReceive : MonoBehaviour
{
    public UnityEvent onAttackAnimFinished; //Player will subscibe this, enemy will subscribe this
    public UnityEvent onAttackCalculation; //Player will subscibe this, enemy will subscribe this

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public void OnAttackAnimFinished()
    {
        Debug.Log("Attack anim finisehd");
        if (onAttackAnimFinished != null)
        {
            onAttackAnimFinished.Invoke();
        }
    }

    //When attack animation visible(just at the moment when sword was swung)
    public void OnAttackCalculation()
    {
        if(onAttackCalculation != null)
        {
            onAttackCalculation.Invoke();
        }
    }
}
