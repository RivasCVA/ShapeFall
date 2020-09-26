using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUIHandler : MonoBehaviour
{
    public GameObject uiController;
    public GameObject backgroundMusicSource;
    public Text ScoreText;
    public Text HighScoreText;
    public GameObject AdPanel;
    public Button ContinueButton;
    public Button RetryButton;
    public Button MenuButton;
    public Image newHighScoreImage;
    private bool shouldKeepContinueButtonHidden;
    private const float INTERSTITIAL_MAX = 2.0f;
    private float interstitialAdScore = 2.0f;
    private const float INTERSTITIAL_WAIT_TIME = 4.0f;

    private GameData gameData;

    private void OnEnable()
    {
        GetComponent<LoseSoundEffect>().PlayIfPossible();
    }

    private void OnDisable()
    {
        GetComponent<HandleInterstitialAd>().StopAdFromLoading();
    }

    private void SetAllScoresTexts()
    {
        int highScore = gameData.highScore;
        int lastScore = PlayerPrefs.GetInt("last_score");
        if (lastScore > highScore)
        {
            highScore = lastScore;
            gameData.highScore = highScore;
            SaveLoadSystem.SaveGameData(gameData);
            PlayerPrefs.SetInt("high_score", highScore);
            newHighScoreImage.gameObject.SetActive(true);
        }
        ScoreText.text = lastScore.ToString();
        HighScoreText.text = "High Score: " + highScore.ToString();
    }

    public void RestartUI()
    {
        gameData = SaveLoadSystem.LoadGameData();
        HideContinueButton();
        newHighScoreImage.gameObject.SetActive(false);
        SetAllScoresTexts();
        if (!shouldKeepContinueButtonHidden) {
            ShowContinueButtonByChance();
        }
        else
        {
            shouldKeepContinueButtonHidden = false;
        }

        EnableRetryButtonTouch();
        EnableMenuButtonTouch();

        if (!gameData.hasNoAds)
        {
            if (interstitialAdScore <= 1.0f)
            {
                LoadIntersitialAd();
            }
        }
    }

    public void HandleRetry()
    {
        if (!gameData.hasNoAds)
        {
            if (!ContinueButton.gameObject.activeInHierarchy)
            {
                interstitialAdScore -= 0.5f;
            }
            else
            {
                interstitialAdScore -= 1.0f;
            }

            if (interstitialAdScore <= 0.0f)
            {
                interstitialAdScore = INTERSTITIAL_MAX;
                DisableRetryButtonTouch();
                Invoke("EnableRetryButtonTouch", INTERSTITIAL_WAIT_TIME);
                ShowInterstitialAd();
            }
            else
            {
                RetryMainGame();
            }
        }
        else
        {
            RetryMainGame();
        }
    }

    public void EnableRetryButtonTouch()
    {
        RetryButton.gameObject.GetComponent<RetryButton>().EndSpin();
        RetryButton.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void DisableRetryButtonTouch()
    {
        RetryButton.gameObject.GetComponent<RetryButton>().StartSpin();
        RetryButton.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void EnableMenuButtonTouch()
    {
        MenuButton.gameObject.GetComponent<MenuButton>().EndSpin();
        MenuButton.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void DisableMenuButtonTouch()
    {
        MenuButton.gameObject.GetComponent<MenuButton>().StartSpin();
        MenuButton.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void ShowInterstitialAd()
    {
        GetComponent<HandleInterstitialAd>().ShowAd();
    }

    public void LoadIntersitialAd()
    {
        GetComponent<HandleInterstitialAd>().RequestInterstitial();
    }

    public void RetryMainGame()
    {
        uiController.GetComponent<UIController>().HideGameOver();
        uiController.GetComponent<UIController>().PrepareMainGame();
    }

    public void ContinueMainGame()
    {
        interstitialAdScore = INTERSTITIAL_MAX + 0.5f;
        shouldKeepContinueButtonHidden = true;
        uiController.GetComponent<UIController>().HideGameOver();
        uiController.GetComponent<UIController>().PrepareContinueMainGame();
    }

    public void HandleSwitchToMenu()
    {
        if (!gameData.hasNoAds)
        {
            if (!ContinueButton.gameObject.activeInHierarchy)
            {
                interstitialAdScore -= 0.5f;
            }
            else
            {
                interstitialAdScore -= 1.0f;
            }

            if (interstitialAdScore <= -0.5f)
            {
                interstitialAdScore = INTERSTITIAL_MAX;
                DisableMenuButtonTouch();
                Invoke("EnableMenuButtonTouch", INTERSTITIAL_WAIT_TIME);
                GetComponent<HandleInterstitialAd>().ShowBeforeMenu();
            }
            else
            {
                SwitchToMenu();
            }
        }
        else
        {
            SwitchToMenu();
        }
    }

    public void SwitchToMenu()
    {
        uiController.GetComponent<UIController>().HideGameOver();
        uiController.GetComponent<UIController>().ShowMenu();
    }

    public void LoadRewardedAd()
    {
        GetComponent<RewardVideoAd>().RequestRewardVideo();
    }

    public void StopLoadingRewardedVideo()
    {
        HideAdPanel();
        GetComponent<RewardVideoAd>().StopAdFromLoading();
    }

    public void ShowContinueButtonByChance()
    {
        if (Random.Range((int)0, (int)3) > 0)
        {
            ContinueButton.gameObject.SetActive(true);
        }
    }

    public void HideContinueButton()
    {
        ContinueButton.gameObject.SetActive(false);
    }

    public void ShowAdPanel()
    {
        backgroundMusicSource.GetComponent<BackgroundMusic>().Pause();
        AdPanel.SetActive(true);
    }

    public void HideAdPanel()
    {
        backgroundMusicSource.GetComponent<BackgroundMusic>().PlayIfPossible();
        HideContinueButton();
        AdPanel.SetActive(false);
    }

}
