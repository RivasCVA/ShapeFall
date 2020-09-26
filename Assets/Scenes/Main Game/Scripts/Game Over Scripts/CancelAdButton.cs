using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelAdButton : MonoBehaviour
{
    public GameObject GameOverCanvas;
    private void OnMouseUpAsButton()
    {
        GameOverCanvas.GetComponent<GameOverUIHandler>().StopLoadingRewardedVideo();
    }
}
