using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class QuizController : MonoBehaviour {

	public List<QuizObjects> quiz1 = new List<QuizObjects>();
	private List<QuizObjects> quiz1Private = new List<QuizObjects>();

	public List<Image> objDisplay;
	public List<Text> objName;

	private List<Image> objDisplayP;
	private List<Text> objNameP;

	public ToggleScript toggleScript;
	public NameToggleScript nameToggleScript;

	public ToggleGroup imageToggleGroup;
	public ToggleGroup nameToggleGroup;

	public LineRenderer lineRend;

	int answeredCorrectly = 0;

	public GameObject nextPanelButton;

	


	void Start () 
	{
		//Debug.Log(objDisplayP.Count);
		objNameP = objName;
		objDisplayP = objDisplay;
		Debug.Log(objDisplayP.Count);
		RandomiseEntries2();
		Debug.Log(objDisplayP.Count);
		lineRend.startWidth = 0.3f;
		lineRend.endWidth = 0.3f;
		lineRend.positionCount = 2;
	}
	
	void Update () 
	{
		
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
			objDisplayP[i].GetComponent<ValueHolder>().nameValue = quiz1[i].objName;
		}
	}

	public void ReceiveAnswer(int answerID)
	{

	}

	public void CompareAnswers()
	{
		Toggle iToggle = null;
		Toggle nToggle = null;
		
		if(nameToggleGroup.ActiveToggles().FirstOrDefault())
		{
			nToggle = nameToggleGroup.ActiveToggles().FirstOrDefault();
		}
		
		if(imageToggleGroup.ActiveToggles().FirstOrDefault())
		{
			iToggle = imageToggleGroup.ActiveToggles().FirstOrDefault();
		}

		if(nToggle && iToggle)
		{
			if(nToggle.GetComponentInChildren<Text>().text == iToggle.GetComponent<ValueHolder>().nameValue)
			{
				Debug.Log("yes");
				iToggle.interactable = false;
				nToggle.interactable = false;
				AllTogglesOff();
				CorrectlyAnswered();
			}
			else
			{
				Debug.Log("no");
				AllTogglesOff();
			}
		}
	}

	void AllTogglesOff()
	{
		nameToggleGroup.SetAllTogglesOff();
		imageToggleGroup.SetAllTogglesOff();
	}

	void CorrectlyAnswered()
	{
		answeredCorrectly++;
		if(answeredCorrectly == nameToggleScript.bgImages.Length)
		{
			nextPanelButton.SetActive(true);
		}
	}

	void DrawLines()
	{
		//lineRend.SetPosition(0, iToggle.GetComponent<RectTransform>().anchoredPosition3D);
		//lineRend.SetPosition(1, nToggle.GetComponent<RectTransform>().anchoredPosition3D);

	}

}

[System.Serializable]
public class QuizObjects
{
	public Sprite objImg;
	public string objName;
}
