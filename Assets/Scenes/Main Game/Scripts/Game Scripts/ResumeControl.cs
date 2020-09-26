using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeControl : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        transform.parent.transform.parent.gameObject.SetActive(false);
        GameBrain.isPaused = false;
    }
}
