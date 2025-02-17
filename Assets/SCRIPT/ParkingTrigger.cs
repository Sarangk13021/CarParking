using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class ParkingTrigger : MonoBehaviour
{
    [SerializeField] GameObject car;
    [SerializeField] GameObject WinScreen;
    [SerializeField] bool inside = false;
    [SerializeField] public bool parked = false;


    // Public properties to access 'inside' and 'parked' from other scripts
    public bool isInside => inside;
    public bool isParked => parked;
    private void Start()
    {
        WinScreen.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        inside = IsInside(other);

        if (inside && !parked)
        {
            Invoke("Park", 1.5f);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Car")
        {
            parked = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Car")
        {
            inside = IsInside(other);
            if (inside && !parked)
            {
                Invoke("Park", 1.5f);
            }
        }
    }

    bool IsInside(Collider other)
    {
        Bounds triggerBounds = GetComponent<Collider>().bounds;
        Bounds otherBounds = other.bounds;
        return triggerBounds.Contains(otherBounds.min) && triggerBounds.Contains(otherBounds.max);
    }

    private void Park()
    {
        if (inside)
        {
            parked = true;
            StartCoroutine(compleat());
        }

        //Debug.Log("hey parked");
    }
    IEnumerator compleat()
    {
        yield return new WaitForSecondsRealtime(1f);
        {
            WinScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }
    }

}