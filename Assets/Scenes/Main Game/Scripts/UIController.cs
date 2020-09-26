using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static GameState currentGameState = GameState.Menu;
    public GameObject MenuCanvas;
	public GameObject GameOverCanvas;
    public GameObject GameCanvas;


    public void ShowMenu()
	{
        UIController.currentGameState = GameState.Menu;
        MenuCanvas.SetActive(true);
        MenuCanvas.GetComponent<MenuUIHandler>().ActivateJumpingShapes();
        GetComponent<TopBannerAd>().ShowBanner();
	}

    public void HideMenu()
	{
        MenuCanvas.SetActive(false);
	}

	public void ShowGameOver()
	{
        UIController.currentGameState = GameState.GameOver;
        GameOverCanvas.SetActive(true);
        GameOverCanvas.GetComponentInChildren<GameOverUIHandler>().RestartUI();
        GetComponent<TopBannerAd>().ShowBanner();
    }

	public void HideGameOver()
	{
        GameCanvas.GetComponent<GameBrain>().DestroyAllFallingShapes();
        GameCanvas.SetActive(false);
        GameOverCanvas.SetActive(false);
	}

    public void PrepareMainGame()
    {
        GetComponent<TopBannerAd>().HideBanner();
        GameCanvas.SetActive(true);
        UIController.currentGameState = GameState.MainGame;
        GameCanvas.GetComponent<GameBrain>().StartGame();
    }

    public void PrepareContinueMainGame()
    {
        GetComponent<TopBannerAd>().HideBanner();
        GameCanvas.SetActive(true);
        UIController.currentGameState = GameState.MainGame;
        GameCanvas.GetComponent<GameBrain>().StartGameFromPrevState();
    }

}

public enum GameState
{
    Menu, MainGame, GameOver
}
