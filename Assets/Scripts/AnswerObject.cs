using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// this is used in GameScene2 for the answer object
public class AnswerObject : MonoBehaviour 
{
	public QuizController2 qController;	// reference to quiz controller
	public Text textDisplay;

	[HideInInspector]
	public bool isCorrect;
	[HideInInspector]
	public string answerText;
	public AudioClip answerAudio;
	
	public void SubmitAnswer()
	{
		qController.ReceiveAnswer(isCorrect);
	}

	public void UpdateAnswerText(string aText)
	{
		textDisplay.text = aText;
	}
}
