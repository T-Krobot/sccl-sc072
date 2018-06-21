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



	void Start () 
	{

		objNameP = objName;
		objDisplayP = objDisplay;
		//quiz1Private = quiz1;
		RandomiseEntries();
		Debug.Log(objDisplay.Count);
	}
	
	void Update () 
	{
		
	}

	void RandomiseEntries()
	{
		//int objDisplayToChoose = 0;

		for(int i = 0; i < objDisplay.Count; i++)
		{
			int imgRandEntry = Random.Range(0, objDisplayP.Count);
			int nameRandEntry = Random.Range(0, objNameP.Count);

			Debug.Log("img rand entry: " + imgRandEntry + " name rand entry: " + nameRandEntry);

			objDisplayP[imgRandEntry].sprite = quiz1[i].objImg;
			objNameP[nameRandEntry].text = quiz1[i].objName;

			objDisplayP.RemoveAt(imgRandEntry);
			objNameP.RemoveAt(nameRandEntry);

			//objDisplayToChoose++;
		}

	}
}

[System.Serializable]
public class QuizObjects
{
	public Sprite objImg;
	public string objName;
}
