using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScaleDown : MonoBehaviour
{
    void Start()
    {
        float change = ((float)Screen.width / (float)Screen.height) / (9.0f / 16.0f);
        if (change < 1.0f)
        {
            float width = transform.localScale.x * change;
            transform.localScale = new Vector3(width, width, width);
        }
    }
}
