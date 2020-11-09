using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoCamaraFollow : MonoBehaviour
{
    public GameObject followTarget;  //A라는 GameObject변수 선언
        Transform AT;
        void Start ()
        {
                AT=followTarget.transform;
        }
        void LateUpdate () {
                transform.position = new Vector3 (AT.position.x,transform.position.y,-10);
        }
}
