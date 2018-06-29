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

	public MemoryCardController mController;

	void Start()
	{
		currentImage = GetComponent<Image>();
	}
	
	public void SetImage(Card newCard)
	{
		card = newCard;
		//TempDebug();
	}

	public void OnPointerDown(PointerEventData pointerEventData)
	{
		if(MemoryCardController.flippedCards < 2 && !isActive)
		{
			ActivateCard();
		}
		else if(MemoryCardController.flippedCards >= 2)
		{
			//mController.DeactivateAllCards();
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

	public void ActivateCard()
	{
		//MemoryCardController.flippedCards++;
		isActive = true;
		currentImage.sprite = card.cardImage;
		mController.CompareCards();
	}

	public void DeactivateCard(bool fromCoroutine)
	{
		if(!correct)
		{
			isActive = false;
			//MemoryCardController.flippedCards--;
			currentImage.sprite = inactiveSprite;
			if(!fromCoroutine)
			{
				mController.StopAllCoroutines();
			}
		}
	}

	void TempDebug()
	{
		currentImage.sprite = card.cardImage;
		var temp = currentImage.color;
		temp.a = 200f;
		currentImage.color = temp;
	}
}
