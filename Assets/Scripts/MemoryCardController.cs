using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryCardController : MonoBehaviour 
{
	public Card[] cards; 								// array of card scriptable objects
	public List<GameObject> cardObjects;				// list of card game objects
	public static int flippedCards = 0;					// number of flipped (activated) cards, updated every frame
	public GameObject restartButton;					// restart button obhect, hidden until the game is over
	public static int correctlyGuessedCards = 0;		// number of correctly guessed cards
	private AudioSource aSource;
	public AudioClip correct, wrong;					// clips played depending on the result of the two cards compared
	
	void Start()
	{
		RandomiseEntries();								// shuffle cards
		correctlyGuessedCards = 0;
		aSource = GetComponent<AudioSource>();
		restartButton.SetActive(false);					// hide restart button
	}


	// assign the cards to the gameobjects
	// in this game, rather than comparing two cards with the same image, the goal is to match a image to a word (character)
	void AssignCards()
	{
		for(int i = 0; i < cardObjects.Count; i++)
		{
			CardObject asdf = cardObjects[i].GetComponent<CardObject>();
			asdf.character = "";
			asdf.ID = cards[i].ID;
			if(i % 2 != 0)
			{
				asdf.SetCard(cards[i], true);
			}
			else
			{
				asdf.SetCard(cards[i], false);
			}
		}
	}
	

	// shuffle the card gameobjects, using fisher yates shuffle
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

	// iterate over all the cards and deactivate them
	public void DeactivateAllCards()
	{
		for(int i = 0; i < cardObjects.Count; i++)
		{
			cardObjects[i].GetComponent<CardObject>().DeactivateCard(false);
		}
	}
	
	void Update()
	{	// check how many cards are active each frame.
		flippedCards = FlippedCards();
	}

	// compare cards to see if they are correct
	public void CompareCards()
	{
		CardObject card1 = null;
		CardObject card2 = null;

		// get active card(s)
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

		// if there are 2 active cards, compare them
		if(card1 && card2)
		{	// if they both have the same ID
			if(card1.ID == card2.ID)
			{
				aSource.clip = correct;
				aSource.Play();
				card1.alreadyActive = true;
				card2.alreadyActive = true;
				card1.isActive = false;
				card2.isActive = false;
				correctlyGuessedCards++;
				if(correctlyGuessedCards >= 4)
				{	// if all cards have been guessed
					GameOver();
				}
			}
			else
			{
				aSource.clip = wrong;
				aSource.Play();
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
			cardObjects[i].GetComponent<CardObject>().alreadyActive = false;
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
