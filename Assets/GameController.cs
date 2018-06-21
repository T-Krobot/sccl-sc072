using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
	public GameObject[] displayPanels;
	private SceneLoader sceneLoader;

	int currentPanel = 0;

	void Start () 
	{
		
	}
	
	void Update () 
	{
		
	}

	
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
			Debug.LogWarning("out of index");
		}
	}
}
