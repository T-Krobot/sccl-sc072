using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextDescription : MonoBehaviour 
{
	public string nextScene;
	public Image objImg;
	public Text objDesc;
	public List<ObjectInformation> objInfo = new List<ObjectInformation>();
	private int arrayEntry = 0;
	private SceneLoader sceneLoader;
	public GameController gController;

	void Start()
	{
		NextObject();
	}
	public void NextObject()
	{
		if(arrayEntry < objInfo.Count)
		{
			objImg.sprite = objInfo[arrayEntry].objectImage;
			objDesc.text = objInfo[arrayEntry].objectDescription;
			arrayEntry++;
		}
		else
		{
			//sceneLoader.LoadNextSceneInBuildIndex();
			gController.GetNextPanel();
		}
		
	}

	public void PreviousObject()
	{
		if(arrayEntry > 0)
		{
			arrayEntry--;
			objDesc.text = objInfo[arrayEntry].objectDescription;
			objImg.sprite = objInfo[arrayEntry].objectImage;
		}
		else
		{
			//sceneLoader.LoadPreviousSceneInBuildIndex();
			//LoadPreviousSceneInBuildIndex();
			gController.GetPreviousPanel();
		}
	}
	public void LoadPreviousSceneInBuildIndex()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
	}
}

[System.Serializable]
public class ObjectInformation
{
	public Sprite objectImage;
	public string objectDescription;
}