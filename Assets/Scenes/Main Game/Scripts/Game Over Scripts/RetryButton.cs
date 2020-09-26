using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RetryButton : MonoBehaviour
{
    public float loadingSpinRate;
    private bool isSpinning;

    private void OnMouseUpAsButton()
    {
        transform.parent.transform.parent.GetComponent<GameOverUIHandler>().HandleRetry();
    }

    public void StartSpin()
    {
        isSpinning = true;
    }

    public void EndSpin()
    {
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        isSpinning = false;
    }

    void Update()
    {
        if (isSpinning)
        {
            transform.Rotate(Vector3.back, loadingSpinRate * Time.deltaTime);
        }
    }
}
