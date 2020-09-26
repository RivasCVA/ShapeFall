using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayButotn : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        transform.parent.GetComponent<MenuUIHandler>().StartMainGame();
    }
}
