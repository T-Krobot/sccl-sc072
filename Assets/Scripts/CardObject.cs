using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardObject : MonoBehaviour, IPointerDownHandler 
{
	public Sprite inactiveSprite;
	Image currentImage;
	[HideInInspector]
	public Card sObject;
	[HideInInspector]
	public bool isActive;
	[HideInInspector]
	public Card card;

	[HideInInspector]
	public bool correct;
	public int ID;
	public string character = null;
	
	public MemoryCardController mController;

	private Text characterText;

	void Awake()
	{
		currentImage = GetComponent<Image>();
		characterText = GetComponentInChildren<Text>();
	}
	void Start()
	{
		
	}
	
	public void SetImage(Card newCard)
	{
		card = newCard;
	}

	public void SetCharacter()
	{
		characterText.text = character;
	}

	public void OnPointerDown(PointerEventData pointerEventData)
	{
		if(!correct)
		{
			if(MemoryCardController.flippedCards < 2 && !isActive)
			{
				ActivateCard();
			}
			else if(MemoryCardController.flippedCards >= 2)
			{
				for(int i = 0; i < mController.cardObjects.Count; i++)
				{
					if(mController.cardObjects[i].GetComponent<CardObject>().isActive)
					{
						mController.cardObjects[i].GetComponent<CardObject>().DeactivateCard(false);
					}
				}
				ActivateCard();
			}
		}
		
	}

	public void ActivateCard()
	{
		isActive = true;
		if(character == "")
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
		
		mController.CompareCards();
	}

	public void DeactivateCard(bool fromCoroutine)
	{
		if(!correct)
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
