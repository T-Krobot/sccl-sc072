using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ToggleScript : MonoBehaviour, IPointerClickHandler 
{
	Toggle thisToggle;
	Image bgImage;


	void Start () 
	{
		thisToggle = GetComponent<Toggle>();
		bgImage = GetComponent<Image>();
	}
	
	void Update () 
	{
		
	}
	

	void ToggleImage()
	{
		
	}

	public void OnPointerClick(PointerEventData pEventData)
	{
		Debug.Log("Toggled");
		if(thisToggle.isOn)
		{
			bgImage.color = Color.green;
		}
		else if(!thisToggle.isOn)
		{
			bgImage.color = Color.black;
		}
	}
}
