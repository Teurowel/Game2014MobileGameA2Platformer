//SourceFileName : Stats.cs
//Author's name : Doosung Jang
//Studnet Number : 101175013
//Date last Modified : Nov.23, 2020
//Program description : This script handles Stats
//Revision History : Nov.23, 2020 Created

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stats : MonoBehaviour
{
    public int hp = 0;
    public int maxHp = 0;
    public int damage = 0;

    //hp, maxhp
    public UnityEvent<int, int> onHealthChanged; //Health UI will subscribe this,

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public void GetDamage(int _damage)
    {
        hp -= _damage;

        onHealthChanged.Invoke(hp, maxHp);
    }
}
