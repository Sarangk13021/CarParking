using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderRunTo1 : MonoBehaviour
{

	public int value;
	 public Slider slider;
	 private float speed=0.7f;

  public float current =0f;
  
  void Start()
  { 
	slider = GetComponent<Slider>();
	slider.interactable = false;
  }
  
    void Update()
    {
		if (current*100 < value)
		{
			current += Time.deltaTime * speed;
			slider.value = current;
		}
			
	}
	
	
}
