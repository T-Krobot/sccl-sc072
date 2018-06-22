using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizController : MonoBehaviour {

	public List<QuizObjects> quiz1 = new List<QuizObjects>();
	private List<QuizObjects> quiz1Private = new List<QuizObjects>();

	public List<Image> objDisplay;
	public List<Text> objName;

	private List<Image> objDisplayP;
	private List<Text> objNameP;

	public ToggleGroup[] toggleGroups;



	void Start () 
	{
		//Debug.Log(objDisplayP.Count);
		objNameP = objName;
		objDisplayP = objDisplay;
		Debug.Log(objDisplayP.Count);
		RandomiseEntries2();
		Debug.Log(objDisplayP.Count);
	}
	
	void Update () 
	{
		
	}

	void RandomiseEntries()
	{

		for(int i = 0; i < objDisplay.Count; i++)
		{
			int imgRandEntry = Random.Range(0, objDisplayP.Count);
			int nameRandEntry = Random.Range(0, objNameP.Count);

			Debug.Log("img rand entry: " + imgRandEntry + " name rand entry: " + nameRandEntry);

			objDisplayP[imgRandEntry].sprite = quiz1[i].objImg;
			objNameP[nameRandEntry].text = quiz1[i].objName;

			objDisplayP.RemoveAt(imgRandEntry);
			objNameP.RemoveAt(nameRandEntry);

		}

	}


	void RandomiseEntries2()
	{
		int n = objDisplay.Count;
		while(n > 1)
		{
			n--;
			int k = Random.Range(0, n + 1);
			int k2 = Random.Range(0, n + 1);
			var displayValue = objDisplayP[k];
			var nameValue = objNameP[k2];
			objDisplayP[k] = objDisplayP[n];
			objNameP[k2] = objNameP[n];
			objDisplayP[n] = displayValue;
			objNameP[n] = nameValue;
			

		}
		SetEntries();
	}

	void SetEntries()
	{
		for(int i = 0; i < objDisplayP.Count; i++)
		{
			objDisplayP[i].sprite = quiz1[i].objImg;
			objNameP[i].text = quiz1[i].objName;
		}
	}

	public void ReceiveAnswer(int answerID)
	{

	}
}

[System.Serializable]
public class QuizObjects
{
	public Sprite objImg;
	public string objName;
}
