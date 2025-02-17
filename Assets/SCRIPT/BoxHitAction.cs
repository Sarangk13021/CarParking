using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHitAction : MonoBehaviour
{
    public GameObject Gate;
    public int speed = 2;
    public Animator gateAnimation;
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("BoxArea"))
        {
            gateAnimation.SetBool("gateAnimation", true);
        }
    }
}
