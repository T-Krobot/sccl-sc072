using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerID : MonoBehaviour 
{
	public int answerID;
	public QuizController qc;

	public void SendAnswer()
	{
		qc.ReceiveAnswer(answerID);
	}
}
