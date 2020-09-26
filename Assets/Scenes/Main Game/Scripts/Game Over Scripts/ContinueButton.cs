using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        transform.parent.transform.parent.GetComponent<GameOverUIHandler>().LoadRewardedAd();
    }
}
