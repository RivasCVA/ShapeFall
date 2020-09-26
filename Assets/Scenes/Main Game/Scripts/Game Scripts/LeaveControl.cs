using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveControl : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        transform.parent.transform.parent.gameObject.SetActive(false);
        GameBrain.isPaused = false;
        transform.parent.transform.parent.transform.parent.GetComponent<GameBrain>().LeaveGame();
    }
}
