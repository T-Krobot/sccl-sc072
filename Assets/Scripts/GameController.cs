using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
	public GameObject[] displayPanels;
	private SceneLoader sceneLoader;
	public bool resetPanelActiveOrder;

	int currentPanel = 0;

	void Start () 
	{
		if(resetPanelActiveOrder)
		{
			ResetPanels();
		}
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

	void ResetPanels()
	{
		for(int i = 0; i < displayPanels.Length; i++)
		{
			displayPanels[i].SetActive(false);
		}
		displayPanels[0].SetActive(true);
	}
}
