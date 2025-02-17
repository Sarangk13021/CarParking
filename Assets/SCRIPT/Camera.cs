using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera; // Reference to the FreeLook camera
    public CinemachineVirtualCamera virtualcamera;
    public GameObject character;
    void Start()
    {
        freeLookCamera.Priority = 10;
        virtualcamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("v"))
        {
            SwitchCamera();
        }
    }
    void SwitchCamera()
    {
        if (freeLookCamera.Priority > 0)
        {
            // Switch to Main Camera
            freeLookCamera.Priority = 0;  // Lower priority disables the FreeLook Camera
            virtualcamera.enabled = true;   // Enable Main Camera
            character.SetActive(false);
        }
        else
        {
            // Switch to FreeLook Camera
            freeLookCamera.Priority = 10; // Higher priority activates the FreeLook Camera
            virtualcamera.enabled = false;  // Disable Main Camera
            character.SetActive(true);
        }
    }
}
