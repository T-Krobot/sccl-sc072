using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


// Quiz controller for the quiz in GameScene1
public class QuizController : MonoBehaviour {

	public List<QuizObjects> quiz1 = new List<QuizObjects>();		// list of classes containing the quiz information

	public List<Image> objDisplay;									// Original list of UI Images for displaying the picture of the object
	public List<Text> objName;										// Original list of UI Text for displaying the object's name.

	public ToggleScript toggleScript;								// reference to script used to toggle the quiz object images when the user taps them
	public NameToggleScript nameToggleScript;						// same but for text

	public ToggleGroup imageToggleGroup;							// toggle group for images
	public ToggleGroup nameToggleGroup;								// toggle group for names

	List<Color> randColors = new List<Color>(new Color[] {Color.blue, Color.green, Color.cyan, Color.magenta, Color.yellow});	// list of colours used when setting correctly connected items. can't just use green 'cause then they all look the same
	private AudioSource aSource;	// audio source
	public AudioClip correct, wrong;	// audio clips depending on if the user got the answer correct or not

	int answeredCorrectly = 0;	// how many questions have been answered correctly so far

	public GameObject nextPanelButton;	// button to go to the next scene, activates once all questions are answered

	void Start () 
	{
		RandomiseEntries();	// randomise the questions and answers
		aSource = GetComponent<AudioSource>();	// get reference to audio source
	}
	

	void RandomiseEntries()
	{

		// fisher yates shuffle
		int n = objDisplay.Count;
		while(n > 1)
		{
			n--;
			int k = Random.Range(0, n + 1);
			int k2 = Random.Range(0, n + 1);

			var displayValue = objDisplay[k];
			var nameValue = objName[k2];

			objDisplay[k] = objDisplay[n];
			objName[k2] = objName[n];

			objDisplay[n] = displayValue;
			objName[n] = nameValue;
			

		}
		SetEntries();
	}

	void SetEntries()
	{
		for(int i = 0; i < objDisplay.Count; i++)
		{
			objDisplay[i].sprite = quiz1[i].objImg;
			objName[i].text = quiz1[i].objName;
			objDisplay[i].GetComponent<ValueHolder>().nameValue = quiz1[i].objName;
      		objDisplay[i].preserveAspect = true;
		}
	}


	// called from the toggles in quiz1
	public void CompareAnswers(int toggleID)
	{
		// if 1 toggle from each group are active, continue.
		if(toggleScript.toggles[toggleID].isOn || nameToggleScript.toggles[toggleID].isOn)
		{
			Debug.Log("compare answers");
			Toggle iToggle = null; 	// image toggle that was selected
			Toggle nToggle = null;	// name toggle that was selected
			

			// get active toggle and set it to local variable
			if(nameToggleGroup.ActiveToggles().FirstOrDefault())
			{
				nToggle = nameToggleGroup.ActiveToggles().FirstOrDefault();
			}
			
			if(imageToggleGroup.ActiveToggles().FirstOrDefault())
			{
				iToggle = imageToggleGroup.ActiveToggles().FirstOrDefault();
			}

			//if both local toggle variables are set
			if(nToggle && iToggle)
			{
				// if the text in the text component of the name toggle is the same as the string held in the image toggles ValueHolder script, do correct stuff
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
				else	// else do wrong stuff
				{
					aSource.clip = wrong;
					aSource.Play();
					Debug.Log("no");
					AllTogglesOff();
				}
			}
		}
	}

	// turn all toggles off
	void AllTogglesOff()
	{
		nameToggleGroup.SetAllTogglesOff();
		imageToggleGroup.SetAllTogglesOff();
	}

	// set answered correctly +1 and check if answeredCorrectly is the same as the number of questions
	void CorrectlyAnswered()
	{
		answeredCorrectly++;
		if(answeredCorrectly == nameToggleScript.bgImages.Length)
		{
			nextPanelButton.SetActive(true);
		}
	}
}


// class to hold quiz data
[System.Serializable]
public class QuizObjects
{
	public Sprite objImg;
	public string objName;
}
