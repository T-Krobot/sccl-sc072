using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// controller script used in GameScene2
// Despite the quiz intro having 5 different objects, the quiz only quizzes users on one of them, the same every time. This was intended by the students.
public class QuizController2 : MonoBehaviour 
{
	public Quiz2QuestionData[] qData;		// array of question classes
	public GameObject[] answerObjects;		// array of answer objects

	public PanelController panelController;	// reference to panel controller

	public Image questionImageDisplay;		// UI image for the question image

	public GameObject wrongAnswerPanel;		// gameobject for the UI panel used when getting the wrong answer

	void Start () 
	{
		// originally this chose a random question to quiz the user on, but since the students only want one, i just changed it to always pick the item at index 0
		UpdateQuestions(0);
	}
	

	void UpdateQuestions(int qToChoose) 
	{
		questionImageDisplay.sprite = qData[qToChoose].questionImage;

		// set the answers
		for(int i = 0; i < qData[qToChoose].answers.Length; i++)
		{
			var aObject = answerObjects[i].GetComponent<AnswerObject>();
			aObject.isCorrect = qData[qToChoose].answers[i].isCorrect;
			aObject.answerAudio = qData[qToChoose].answers[i].answerAudio;
			aObject.UpdateAnswerText(qData[qToChoose].answers[i].answerText);
		}
	}


	// called from AnswerObject.cs
	public void ReceiveAnswer(bool isCorrect)
	{
		if(isCorrect)
		{
			panelController.GetNextPanel();
		}
		else if(!isCorrect)
		{
			wrongAnswerPanel.SetActive(true);
		}
	}
}

// class holding answer data such as the answer's text, audio clip and whether it is correct or not.
[System.Serializable]
public class Quiz2AnswerData
{
	public string answerText;
	public AudioClip answerAudio;
	public bool isCorrect;
}


// question class, holds an image and an array of answers
[System.Serializable]
public class Quiz2QuestionData
{
	public Quiz2AnswerData[] answers;
	public Sprite questionImage;
}
