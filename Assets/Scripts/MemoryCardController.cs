using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryCardController : MonoBehaviour 
{
	public Card[] cards;
	public List<GameObject> cardObjects;
	public static int flippedCards = 0;
	public GameObject restartButton;
	public static int correctlyGuessedCards = 0;

	
	void Start()
	{
		RandomiseEntries();
		correctlyGuessedCards = 0;
	}

	void AssignCards()
	{
		for(int i = 0; i < cardObjects.Count; i++)
		{
			CardObject asdf = cardObjects[i].GetComponent<CardObject>();
			asdf.SetImage(cards[i]);
		}
	}
	
	void RandomiseEntries()
	{
		int n = cardObjects.Count;
		while(n > 1)
		{
			n--;
			int k = Random.Range(0, n + 1);
			var displayValue = cardObjects[k];
			cardObjects[k] = cardObjects[n];
			cardObjects[n] = displayValue;
		}
		AssignCards();
	}

	public void DeactivateAllCards()
	{
		for(int i = 0; i < cardObjects.Count; i++)
		{
			cardObjects[i].GetComponent<CardObject>().DeactivateCard(false);
		}
	}
	
	void Update()
	{
		flippedCards = FlippedCards();
		Debug.Log(correctlyGuessedCards);
	}

	public void CompareCards()
	{
		CardObject card1 = null;
		CardObject card2 = null;

		for(int i = 0; i < cardObjects.Count; i++)
		{
			if(cardObjects[i].GetComponent<CardObject>().isActive && card1 == null)
			{
				card1 = cardObjects[i].GetComponent<CardObject>();
			}
			else if(cardObjects[i].GetComponent<CardObject>().isActive && card1 != null)
			{
				card2 = cardObjects[i].GetComponent<CardObject>();
			}
		}

		if(card1 && card2)
		{
			if(card1.card == card2.card)
			{
				card1.correct = true;
				card2.correct = true;
				card1.isActive = false;
				card2.isActive = false;
				correctlyGuessedCards++;
				if(correctlyGuessedCards >= 4)
				{
					GameOver();
				}
			}
			else
			{
			StartCoroutine(FlipCardBack(card1, card2));
			}
		}
	}

	void GameOver()
	{
		restartButton.SetActive(true);
	}

	public void RestartGame()
	{
		for(int i = 0; i < cardObjects.Count; i++)
		{
			cardObjects[i].GetComponent<CardObject>().correct = false;
		}
		DeactivateAllCards();
		RandomiseEntries();
		correctlyGuessedCards = 0;
		restartButton.SetActive(false);
	}

	IEnumerator FlipCardBack(CardObject card1, CardObject card2)
	{
		yield return new WaitForSeconds(1f);
		card1.DeactivateCard(true);
		card2.DeactivateCard(true);
	}

	int FlippedCards () 
	{
		int asdf = 0;
		for(int i = 0; i < cardObjects.Count; i++)
		{
			if(cardObjects[i].GetComponent<CardObject>().isActive)
			{
				asdf++;
			}
		}
		return asdf;
	}
}
