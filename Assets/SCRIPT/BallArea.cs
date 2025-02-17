using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallArea : MonoBehaviour
{
    public Rigidbody[] BallArea1;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BallArea1"))
        {
            foreach (Rigidbody rb in BallArea1)
            {
                rb.isKinematic = false;
            }
        }
       
    }
}
