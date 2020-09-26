using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour
{
    public Sprite MusicOnSprite;
    public Sprite MusicOffSprite;
    public AudioSource BackgroundMusicSource;
    public static bool musicEnabled;

    private void Start()
    {
        GameData gameData = SaveLoadSystem.LoadGameData();
        musicEnabled = gameData.hasMusic;
        SetCorrectSprite();
    }

    private void SetCorrectSprite()
    {
        if (musicEnabled)
        {
            GetComponent<Image>().sprite = MusicOnSprite;
        }
        else
        {
            GetComponent<Image>().sprite = MusicOffSprite;
        }
    }

    private void OnMouseUpAsButton()
    {
        musicEnabled = !musicEnabled;
        transform.parent.GetComponent<ButtonClickSoundEffect>().PlayIfPossible();
        SetCorrectSprite();
        BackgroundMusicSource.GetComponent<BackgroundMusic>().PlayIfPossible();

        GameData gameData = SaveLoadSystem.LoadGameData();
        gameData.hasMusic = musicEnabled;
        SaveLoadSystem.SaveGameData(gameData);
    }

}
