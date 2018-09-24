using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NameToggleScript : MonoBehaviour 
{
	[HideInInspector] public Toggle[] toggles;
	[HideInInspector] public Image[] bgImages;
	[Tooltip("if the toggle has an image as a background instead of blank colour")]
	public bool useTint;



	void Start () 
	{
		toggles = gameObject.GetComponentsInChildren<Toggle>();
		bgImages = gameObject.GetComponentsInChildren<Image>();
		Debug.Log(bgImages.Length);
	}

	public void ToggleToggles()
	{
		for(int i = 0; i < bgImages.Length; i++)
		{
			if(toggles[i].isOn && !useTint && toggles[i].interactable)
			{
				bgImages[i].color =  Color.green;
			}
			else if (!toggles[i].isOn && !useTint && toggles[i].interactable)
			{
				bgImages[i].color = Color.black;
			}
			else if (toggles[i].isOn && useTint && toggles[i].interactable)
			{
				bgImages[i].color = Color.green;
			}
			else if (!toggles[i].isOn && useTint && toggles[i].interactable)
			{
				bgImages[i].color = Color.white;
			}
		}
	}
}
