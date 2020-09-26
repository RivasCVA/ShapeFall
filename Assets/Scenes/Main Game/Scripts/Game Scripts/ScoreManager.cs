using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public void UpdateScoreText(string score)
    {
        GetComponent<Text>().text = score;
    }

    public void HideText()
    {
        GetComponent<Text>().CrossFadeAlpha(0.0f, 0.0f, false);
    }

    public void ShowText()
    {
        GetComponent<Text>().CrossFadeAlpha(1.0f, 0.0f, false);
    }

}
