using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerObject : MonoBehaviour 
{
	// this can be attached to an object like a button or whatever else you are using.
	public QuizController2 qController;
	public Text textDisplay;

	[HideInInspector]
	public bool isCorrect;
	[HideInInspector]
	public string answerText;
	
	void OnTriggerEnter2D(Collider2D other)
	{
		// if there are other colliders that might intersect then check other's tag and tag the bullet or w/e
		SubmitAnswer();
	}

	public void SubmitAnswer()
	{
		qController.ReceiveAnswer(isCorrect);
	}

	public void UpdateAnswerText(string aText)
	{
		textDisplay.text = aText;
	}
}
