using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartControl : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        transform.parent.transform.parent.transform.parent.GetComponent<GameBrain>().StartGame();
        transform.parent.transform.parent.gameObject.SetActive(false);
        GameBrain.isPaused = false;
    }
}
