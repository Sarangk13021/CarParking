using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;



public class HOMEvolumSettings : MonoBehaviour
{
    [SerializeField]private AudioMixer audioMixer;

    public void SetVolume(float sliderValue)
    {
        
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }

}
