using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int highScore;
    public bool hasSoundEffects;
    public bool hasMusic;
    public bool hasNoAds;

    public GameData()
    {
        highScore = 0;
        hasSoundEffects = true;
        hasMusic = true;
        hasNoAds = false;
    }

    public GameData(int highScore, bool hasSoundEffects, bool hasMusic, bool hasNoAds)
    {
        this.highScore = highScore;
        this.hasSoundEffects = hasSoundEffects;
        this.hasMusic = hasMusic;
        this.hasNoAds = hasNoAds;
    }
}
