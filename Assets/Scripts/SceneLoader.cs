using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader 
{
	public void LoadNextSceneInBuildIndex()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void LoadPreviousSceneInBuildIndex()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
	}

	public void LoadSceneByName(string name)
	{
		SceneManager.LoadScene(name);
	}
}
