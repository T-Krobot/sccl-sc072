using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


// This script handles showing the next/previous screen based on button presses.
// This script is used in all 3 quiz scenes.
public class PanelController : MonoBehaviour 
{
	public GameObject[] displayPanels;		// all the panels that make up the set of displays
	public bool resetPanelActiveOrder;		// If this is true, it will deactivate all the displays and then set the first one to active. Useful to avoid having the wrong panel activated in a build.

	int currentPanel = 0;					// currently activate panel, used for indexing.

	void Start () 
	{
		// resetting panels if true
		if(resetPanelActiveOrder)
		{
			ResetPanels();
		}
	}

	
	// Called from buttons in the scene. 
	public void NextPanel(bool forward)
	{
		if(forward)
		{
			GetNextPanel();
		}
		else if(!forward)
		{
			GetPreviousPanel();
		}
	}

	public void GetNextPanel()
	{
		// grabs next panel if one exists. The last panel has a button that manually calls a scene load, rather than automatically calling one here.
		if(currentPanel < displayPanels.Length)
		{
			displayPanels[currentPanel].SetActive(false);
			currentPanel += 1;
			displayPanels[currentPanel].SetActive(true);
		}
		else
		{
			Debug.LogWarning("out of index");
		}
	}


	// same but in reverse and auto loads the level select if on the first panel
	public void GetPreviousPanel()
	{
		if(currentPanel > 0)
		{
			displayPanels[currentPanel].SetActive(false);
			currentPanel -= 1;
			displayPanels[currentPanel].SetActive(true);
		}
		else
		{
			SceneManager.LoadScene("LevelSelect");
		}
	}


	// resetting panels
	void ResetPanels()
	{
		for(int i = 0; i < displayPanels.Length; i++)
		{
			displayPanels[i].SetActive(false);
		}
		displayPanels[0].SetActive(true);
	}
}
