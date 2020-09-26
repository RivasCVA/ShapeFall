using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
    public GameObject PausePanel;

    private void OnMouseUpAsButton()
    {
        if (UIController.currentGameState == GameState.MainGame)
        {
            bool prevPause = GameBrain.isPaused;
            GameBrain.isPaused = true;
            if (prevPause == false)
            {
                PausePanel.transform.gameObject.SetActive(true);
                transform.parent.gameObject.GetComponent<GameBrain>().WasPaused();
            }
        }
    }



}
