using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// This script is used for showing the descriptions of each object in the quiz sections.
public class NextDescription : MonoBehaviour
{
	public AudioSource aSource;																// audio source reference
	public Image objImg;																	// UI image where object image will be displayed
	public Text objDesc;																	// UI text where object name will be displayed
	public List<ObjectInformation> objInfo = new List<ObjectInformation>();					// list of object info class
	private int arrayEntry = 0;																// used for accessing audio, image and name of specific object
	public PanelController panelController;														// reference to panel controller

	void Start()
	{
		NextObject();	// call first object
	}
	public void NextObject() // this is called from the forward button
	{
		if(arrayEntry < objInfo.Count) // if there are objects remaining
		{
			objImg.sprite = objInfo[arrayEntry].objectImage;
			objDesc.text = objInfo[arrayEntry].objectDescription;
			aSource.clip = objInfo[arrayEntry].objectAudio;
			arrayEntry++;
		}
		else // if not then call to panel controller to show next panel
		{
            aSource.Stop();
			panelController.GetNextPanel();
		}
		
	}

	public void PreviousObject() // inverse of NextObject
	{
		if(arrayEntry > 0)
		{
			arrayEntry--;
			objDesc.text = objInfo[arrayEntry].objectDescription;
			objImg.sprite = objInfo[arrayEntry].objectImage;
			aSource.clip = objInfo[arrayEntry].objectAudio;
		}
		else
		{
			panelController.GetPreviousPanel();
		}
	}
}

// class used to store object info. Not using CSV parser for this so made it public/serialized so can enter it manually. kinda tiresome i know.
[System.Serializable]
public class ObjectInformation
{
	public Sprite objectImage;				// image of object (chair, table, whatever)
	public string objectDescription;		// name or description of object, displayed in objDesc.text
    public AudioClip objectAudio;			// audio for the object
}