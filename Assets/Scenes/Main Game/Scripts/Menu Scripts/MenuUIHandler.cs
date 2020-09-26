using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIHandler : MonoBehaviour
{
    public GameObject uiController;
    public float boxBottomAnchor;
    public GameObject leftBox;
    public GameObject middleBox;
    public GameObject rightBox;
    public GameObject jumpingShape1;
    public GameObject jumpingShape2;

    private void Start()
    {
        //Centers All of the Boxes based on the current Screen
        Vector3 middleBoxScrnToWrld = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2.0f, 0, 0));
        middleBox.transform.position = new Vector3(middleBoxScrnToWrld.x, middleBoxScrnToWrld.y + boxBottomAnchor, 0);

        Vector3 leftBoxScrnToWrld = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 5.0f, 0, 0));
        leftBox.transform.position = new Vector3(leftBoxScrnToWrld.x, leftBoxScrnToWrld.y + boxBottomAnchor, 0);

        Vector3 rightBoxScrnToWrld = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - (Screen.width / 5.0f), 0, 0));
        rightBox.transform.position = new Vector3(rightBoxScrnToWrld.x, rightBoxScrnToWrld.y + boxBottomAnchor, 0);

        ActivateJumpingShapes();
    }

    public void StartMainGame()
    {
        DeactivateJumpingShapes();
        uiController.GetComponent<UIController>().PrepareMainGame();
        uiController.GetComponent<UIController>().HideMenu();
    }

    public void DeactivateJumpingShapes()
    {
        jumpingShape1.SetActive(false);
        jumpingShape2.SetActive(false);
    }

    public void ActivateJumpingShapes()
    {
        jumpingShape1.SetActive(true);
        jumpingShape2.SetActive(true);
    }

    public void UpdateNoAdsButton()
    {
        GetComponentInChildren<AdsButton>().ReUpdate();
    }

}
