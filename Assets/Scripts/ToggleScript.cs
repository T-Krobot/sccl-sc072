using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// toggle script used in GameScene1
public class ToggleScript : MonoBehaviour 
{
	[HideInInspector]public Toggle[] toggles;	// array of toggles
	[HideInInspector]public Image[] bgImages;


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
			if (toggles[i].isOn && toggles[i].interactable)
			{
				bgImages[i].color = Color.green;
			}
			else if (!toggles[i].isOn && toggles[i].interactable)
			{
				bgImages[i].color = Color.white;
			}
		}
	}
}
