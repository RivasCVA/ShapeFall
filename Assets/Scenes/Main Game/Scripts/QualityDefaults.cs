using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualityDefaults : MonoBehaviour
{
    public bool vsyncEnabled;
    public int targetFPS;

    // Start is called before the first frame update
    void Start()
    {
        if (vsyncEnabled)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFPS;
    }
}
