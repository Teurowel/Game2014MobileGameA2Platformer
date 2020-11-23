using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimatorEventReceive : MonoBehaviour
{
    public UnityEvent onAttackAnimFinished; //Player will subscibe this

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
}
