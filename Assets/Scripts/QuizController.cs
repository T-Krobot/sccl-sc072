using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class QuizController : MonoBehaviour {

	public List<QuizObjects> quiz1 = new List<QuizObjects>();

	public List<Image> objDisplay;
	public List<Text> objName;

	private List<Image> objDisplayP;
	private List<Text> objNameP;

	public ToggleScript toggleScript;
	public NameToggleScript nameToggleScript;

	public ToggleGroup imageToggleGroup;
	public ToggleGroup nameToggleGroup;

	List<Color> randColors = new List<Color>(new Color[] {Color.blue, Color.green, Color.cyan, Color.magenta, Color.yellow});
	private AudioSource aSource;
	public AudioClip correct, wrong;

	int answeredCorrectly = 0;

	public GameObject nextPanelButton;

	void Start () 
	{
		objNameP = objName;
		objDisplayP = objDisplay;
		Debug.Log(objDisplayP.Count);
		RandomiseEntries();
		Debug.Log(objDisplayP.Count);
		aSource = GetComponent<AudioSource>();
	}
	

	void RandomiseEntries()
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
      objDisplayP[i].preserveAspect = true;
		}
	}

	public void CompareAnswers(int toggleID)
	{
		if(toggleScript.toggles[toggleID].isOn || nameToggleScript.toggles[toggleID].isOn)
		{
			Debug.Log("compare answers");
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
					aSource.clip = correct;
					aSource.Play();
					Debug.Log("yes");
					iToggle.interactable = false;
					nToggle.interactable = false;

					int tempInt = Random.Range(0, randColors.Count);


					iToggle.image.color = randColors[tempInt];
					nToggle.image.color = randColors[tempInt];
					randColors.RemoveAt(tempInt);
					AllTogglesOff();
					CorrectlyAnswered();
				}
				else
				{
					aSource.clip = wrong;
					aSource.Play();
					Debug.Log("no");
					AllTogglesOff();
				}
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
}

[System.Serializable]
public class QuizObjects
{
	public Sprite objImg;
	public string objName;
}
