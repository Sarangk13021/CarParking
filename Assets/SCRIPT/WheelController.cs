using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class WheelController : MonoBehaviour
{
   

    [SerializeField] WheelCollider FrontLeft;
    [SerializeField] WheelCollider FrontRight;
    [SerializeField] WheelCollider BackLeft;
    [SerializeField] WheelCollider BackRight;

    [SerializeField] Transform FrontLefttransform;
    [SerializeField] Transform FrontRighttransform;
    [SerializeField] Transform BackLefttransform;
    [SerializeField] Transform BackRighttransform;

    //for staring
    [SerializeField] Transform Steering;
    [SerializeField] Transform speedometer1;

    public float Accelaration = 500f;
    public float Breakforce = 300f;
    public float MaxturnAngle = 20f;

    public Rigidbody rb;

    private float currentAccelaration = 0f;
    private float currentBreakforce = 0f;
    private float currentturnAngle = 0f;

    public TrailRenderer LeftTrail;
    public TrailRenderer rightTrail;

    public GameObject HeadLight;
    public bool IsLight = false;

    public GameObject BreakLight;
    public GameObject ReverseLight;
    
    public ParticleSystem effect1;
    public ParticleSystem effect2;

    public AudioSource runingAudio;
    public AudioSource BreakAudio;
    
    public float TopSeed = 100f;
    private bool isDrive = false;
    private bool isReverse = false;

    private void Start()
    {
        HeadLight.SetActive(false);  
    }
    private void Update()
    {
        audioUpdate();
        if (Input.GetKeyDown(KeyCode.E))
        {
            isDrive = true;
            isReverse = false;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            isDrive = false;
            isReverse = true;
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            isDrive = false;
            isReverse = false;
        }
    }
    public void FixedUpdate()
    {

        rb.centerOfMass = new Vector3(0, -0.5f, 0);
        // Get forward backward accelaration
        //currentAccelaration = Accelaration * Input.GetAxis("Vertical");
        if (isDrive)//gear to move forward
        {
            currentAccelaration = Accelaration * Mathf.Max(0, Input.GetAxis("Vertical"));
        }
        else if (isReverse)//gear to move backward
        {
            currentAccelaration = -Accelaration * Mathf.Max(0, Input.GetAxis("Vertical"));
        }
        else
        {
            currentAccelaration = 0f;
        }


        // For Breaks 
        if (Input.GetKey(KeyCode.Space))
        {
            currentBreakforce = Breakforce;
           
            BreakLight.SetActive(true);
            
        }
        else
        {
           
            currentBreakforce = 0f;
           
            BreakLight.SetActive(false);
           
        }
        if (Input.GetKey("l"))
        {
            if (IsLight)
            {
                HeadLight.SetActive(true);
                IsLight = false;
            }
            else
            {
                HeadLight.SetActive(false);
                IsLight = true;
            }
        }
        if(Input.GetKey("s"))
        {
            ReverseLight.SetActive(true); 
        }
        else
        {
            ReverseLight.SetActive(false);
        }
        if(Input.GetKey("w")&&Input.GetKey(KeyCode.Space))
        {
            BreakAudio.volume = 0.2f;
            breakTrails(true);
            PlayEffect();
        }
        else
        {
            BreakAudio.volume = 0f;
            breakTrails(false);
            StopEffect();
        }
       

        // Apply accelaration to the front wheels
        FrontLeft.motorTorque = currentAccelaration;
        FrontRight.motorTorque = currentAccelaration;
        BackLeft.motorTorque = currentAccelaration;
        BackRight.motorTorque = currentAccelaration;

        //Apply breaks to the all four wheels;
        FrontLeft.brakeTorque = currentBreakforce;
        FrontRight.brakeTorque = currentBreakforce;
        BackLeft.brakeTorque = currentBreakforce;
        BackRight.brakeTorque = currentBreakforce;

        // Take care of steering;
        currentturnAngle = MaxturnAngle * Input.GetAxis("Horizontal");

        FrontLeft.steerAngle = currentturnAngle;
        FrontRight.steerAngle = currentturnAngle;

        //update wheel Meshes
        UpdateWheel(FrontLeft, FrontLefttransform);
        UpdateWheel(FrontRight, FrontRighttransform);
        UpdateWheel(BackLeft, BackLefttransform);
        UpdateWheel(BackRight, BackRighttransform);
        Steering.localRotation = Quaternion.Euler(0, 0, -currentturnAngle * 2);
        speedometer1.localRotation = Quaternion.Euler(0, 0, -currentturnAngle * 2);
    }
    void UpdateWheel(WheelCollider col,Transform trans)
    {
        // Get Wheel Collider State;
        Vector3 position;
        Quaternion rotation;
        col.GetWorldPose(out position, out rotation);

        //Get wheel Transform state;
        trans.position = position;
        trans.rotation = rotation;
    }

    //Car Accelaration audio
    void audioUpdate()
    {
        float speed = rb.velocity.magnitude;
        runingAudio.pitch = Mathf.Lerp(1f, 5.0f, speed / TopSeed);


        runingAudio.volume = Mathf.Lerp(0.2f, 0.5f, speed / TopSeed);
    }

    void breakTrails(bool enable)
    {
        LeftTrail.emitting = enable;
        rightTrail.emitting = enable;
    }
    public void PlayEffect()
    {
        if (!effect1.isPlaying&&!effect2.isPlaying)
        {
            effect1.Play();
            effect2.Play();
        }
    }

    // Method to stop the particle effect
    public void StopEffect()
    {
        if (effect1.isPlaying&&effect2.isPlaying)
        {
            effect1.Stop();
            effect2.Stop();
        }
    }

}
