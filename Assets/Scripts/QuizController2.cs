using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizController2 : MonoBehaviour 
{
	public Quiz2QuestionData[] qData;
	public GameObject[] answerObjects;

	public GameController gController;

	public Image questionImageDisplay;

	public GameObject wrongAnswerPanel;

	void Start () 
	{
		ChooseRandomQuestion();
	}

	void ChooseRandomQuestion()
	{
		int randQ = Random.Range(0, qData.Length);
		UpdateQuestions(randQ);
	}
	
	void Update () 
	{
		
	}

	void UpdateQuestions(int qToChoose) 
	{
		questionImageDisplay.sprite = qData[qToChoose].questionImage;
		for(int i = 0; i < qData[qToChoose].answers.Length; i++)
		{
			var aObject = answerObjects[i].GetComponent<AnswerObject>();
			aObject.isCorrect = qData[qToChoose].answers[i].isCorrect;
			aObject.UpdateAnswerText(qData[qToChoose].answers[i].answerText);

		}
	}

	public void ReceiveAnswer(bool isCorrect)
	{
		if(isCorrect)
		{
			gController.GetNextPanel();
		}
		else if(!isCorrect)
		{
			wrongAnswerPanel.SetActive(true);
		}
		else
		{
			Debug.LogWarning("Something wrong with receive answer");
		}
	}
}

[System.Serializable]
public class Quiz2AnswerData
{
	public string answerText;
	public bool isCorrect;
}

[System.Serializable]
public class Quiz2QuestionData
{
	public Quiz2AnswerData[] answers;
	public Sprite questionImage;
}
