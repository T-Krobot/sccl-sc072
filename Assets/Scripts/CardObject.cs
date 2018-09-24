using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// This script is attached to each of the "cards" in "Cards container" in GameScene3
public class CardObject : MonoBehaviour, IPointerDownHandler 
{
	public Sprite inactiveSprite;		// sprite used when inactive
	private Image currentImage;			// image component on the card object

	[HideInInspector] public bool isActive;		// bool for if the card is active (when tapped and showing object or name)
	[HideInInspector] public Card card;			// scriptable object holding card info

	[HideInInspector] public bool alreadyActive;		// bool for if the card has been corretly guessed.
														// once correctly guessed the card will stay flipped and cannot be interacted with.
	public int ID;								// ID of the card, used to compare two cards to each other. Unlike normal memory cards where it's just matching two of the same, here we need to match a name and an image
	public string character = null;				// String holding character to display if the card is not an image card.
												// This is also used as a check when "activating" the card, if it is == "" then the card shows an image, else it shows the character	
	
	public MemoryCardController mController;	// reference to memory card contoller

	private Text characterText;					// Text child component

	void Awake()
	{	// grab references for components on object
		currentImage = GetComponent<Image>();
		characterText = GetComponentInChildren<Text>();
	}


	// called from MemoryCardController.cs. Sets card and character if it is a character card instead of an image card
	public void SetCard(Card newCard, bool charCard)
	{
		card = newCard;
		ID = newCard.ID;
		if(charCard)
		{
			character = newCard.character;
		}
	}


	// when the element is clicked or tapped
	public void OnPointerDown(PointerEventData pointerEventData)
	{
		if(!alreadyActive) // if the card is not already correctly guessed
		{
			if(MemoryCardController.flippedCards < 2 && !isActive) // if there are less than 2 "active" cards when this card is tapped, call activate card
			{
				ActivateCard();
			}
			else if(MemoryCardController.flippedCards >= 2)	// if there are already 2 cards active and a third is tapped, force deactivate them instead of waiting for coroutine
			{
				for(int i = 0; i < mController.cardObjects.Count; i++)
				{
					if(mController.cardObjects[i].GetComponent<CardObject>().isActive)
					{
						mController.cardObjects[i].GetComponent<CardObject>().DeactivateCard(false); // is not from coroutine
					}
				}
				// ActivateCard();	// don't think this was ever used????????!?!?
			}
		}
		
	}

	// called from OnPointerDown
	public void ActivateCard()
	{
		isActive = true;
		if(character == "")	// if the card is not a character card
		{
			currentImage.sprite = card.cardImage;
			Debug.Log("image card");
		}
		else
		{
			characterText.text = character;
			Debug.Log("character card");
			Debug.Log(character);
		}
		
		mController.CompareCards(); // check the two cards against each other
	}

	// deactivate card and check if need to stop coroutines. only using one coroutine so it's fine using StopAllCoroutines here
	public void DeactivateCard(bool fromCoroutine)
	{
		if(!alreadyActive)	// don't deactive correctly answered cards. these stay flipped permanently 
		{
			isActive = false;
			if(character == "")
			{
				currentImage.sprite = inactiveSprite;
			}
			else
			{
				characterText.text = "";
			}
			if(!fromCoroutine)
			{
				mController.StopAllCoroutines();
			}
		}
	}
}
