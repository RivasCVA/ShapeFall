using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdsButton : MonoBehaviour
{
	public Sprite AdsOn;
	public Sprite AdsOff;
	public static bool hasNoAds;

	private void Start()
	{
		GameData gameData = SaveLoadSystem.LoadGameData();
		hasNoAds = gameData.hasNoAds;
		SetCorrectSprite();
	}

    public void ReUpdate()
    {
        Start();
    }

	private void SetCorrectSprite()
	{
		if (hasNoAds)
		{
			GetComponent<Image>().sprite = AdsOff;
		}
		else
		{
			GetComponent<Image>().sprite = AdsOn;
		}
	}

	private void OnMouseUpAsButton()
	{
        if (!hasNoAds)
        {
            transform.parent.GetComponent<ButtonClickSoundEffect>().PlayIfPossible();
            GetComponentInParent<IAPManager>().BuyNoAds();
        }
	}
}
