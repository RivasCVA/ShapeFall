using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundEffectsButton : MonoBehaviour
{
    public Sprite SoundEffectsOn;
    public Sprite SoundEffectsOff;
    public static bool soundEffectsEnabled;

    private void Start()
    {
        GameData gameData = SaveLoadSystem.LoadGameData();
        soundEffectsEnabled = gameData.hasSoundEffects;
        SetCorrectSprite();
    }

    private void SetCorrectSprite()
    {
        if (soundEffectsEnabled)
        {
            GetComponent<Image>().sprite = SoundEffectsOn;
        }
        else
        {
            GetComponent<Image>().sprite = SoundEffectsOff;
        } 
    }

    private void OnMouseUpAsButton()
    {
        soundEffectsEnabled = !soundEffectsEnabled;
        transform.parent.GetComponent<ButtonClickSoundEffect>().PlayIfPossible();
        SetCorrectSprite();

        GameData gameData = SaveLoadSystem.LoadGameData();
        gameData.hasSoundEffects = soundEffectsEnabled;
        SaveLoadSystem.SaveGameData(gameData);
    }
}
