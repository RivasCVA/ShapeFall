using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTouch : MonoBehaviour
{
    private int currentSelectedShapeIndex;

    private void Start()
    {
        HideShapes();
    }

    public void PrepareBox()
    {
        currentSelectedShapeIndex = 0;

        for (int i = 0; i < GetComponentsInChildren<ShapeOrbit>().Length; i++)
        {
            GetComponentsInChildren<ShapeOrbit>()[i].RestartOrbit();
        }

        ShowShapes();
    }

    public void HideShapes()
    {
        foreach (SpriteRenderer r in GetComponentsInChildren<SpriteRenderer>())
        {
            if (r.CompareTag("Shape"))
            {
                r.color = new Color(r.color.r, r.color.g, r.color.b, 0);
            }
        }
    }

    public void ShowShapes()
    {
        foreach (SpriteRenderer r in GetComponentsInChildren<SpriteRenderer>())
        {
            if (r.CompareTag("Shape"))
            {
                r.color = new Color(r.color.r, r.color.g, r.color.b, 1);
            }
        }
    }

    private void OnMouseUpAsButton()
    {
        if (!GameBrain.isPaused)
        {
            if (UIController.currentGameState == GameState.MainGame)
            {
                IncreaseShapeIndex();
            }
        }
    }

    private void IncreaseShapeIndex()
    {
        currentSelectedShapeIndex++;
        if (currentSelectedShapeIndex > 3) 
        {
            currentSelectedShapeIndex = 0;
        }
    }

    public int GetCurrentSelectedIndex()
    {
        return currentSelectedShapeIndex;
    }

}
