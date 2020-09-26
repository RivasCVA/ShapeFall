using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameBrain : MonoBehaviour
{
    private int difficulty;
    private int score;
    private int lives;

    public GameObject uiController;
    public float startDelay;

    public GameObject fallingShapeObject;
    private float fallAppearRate;
    private float fallTime;
    private float nextFallTime;

    private const int DIFFICULTY_INCREASE_RATE = 6;
    private const float START_APPEAR_RATE = 1.5f;
    private const float START_FALL_TIME = 2.5f;
    private const float END_APPEAR_RATE = 0.9f;
    private const float END_FALL_TIME = 1.9f;
    private const float EXTREME_APPEAR_RATE = 0.8f;
    private const float EXTREME_FALL_TIME = 1.8f;

    public GameObject leftBox;
    public GameObject middleBox;
    public GameObject rightBox;

    public Image Life1;
    public Image Life2;
    public Image Life3;

    public static bool isPaused;
    private float timeLeftForSpawnBeforePause;

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            if (UIController.currentGameState != GameState.Menu)
            {
                SpawnNewFallingObjects();
            }
        }
        else
        {
            nextFallTime = Time.time + timeLeftForSpawnBeforePause;
        }
    }

    public void WasPaused()
    {
        timeLeftForSpawnBeforePause = nextFallTime - Time.time;
    }

    public void ApplyStartDelay()
    {
        nextFallTime = Time.time + startDelay;
    }

    private void SpawnNewFallingObjects()
    {
        //Will spawn new falling shapes
        if (nextFallTime < Time.time)
        {
            nextFallTime = Time.time + fallAppearRate;

            Vector3 topMiddleScreenToWorld = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2.0f, Screen.height + (fallingShapeObject.transform.localScale.y / 2.0f), 0));
            switch (Random.Range(1, 4))
            {
                case 1:
                    Instantiate(fallingShapeObject, new Vector3(leftBox.transform.position.x, topMiddleScreenToWorld.y, 0), Quaternion.identity).GetComponent<ShapeFall>().SetFallSpeed(fallTime);
                    break;
                case 2:
                    Instantiate(fallingShapeObject, new Vector3(middleBox.transform.position.x, topMiddleScreenToWorld.y, 0), Quaternion.identity).GetComponent<ShapeFall>().SetFallSpeed(fallTime);
                    break;
                case 3:
                    Instantiate(fallingShapeObject, new Vector3(rightBox.transform.position.x, topMiddleScreenToWorld.y, 0), Quaternion.identity).GetComponent<ShapeFall>().SetFallSpeed(fallTime);
                    break;
                default:
                    break;
            }
        }
    }

    public void IncreaseScore()
    {
        score++;
        GetComponentInChildren<ScoreManager>().UpdateScoreText(score.ToString());
        if (score % DIFFICULTY_INCREASE_RATE == 0)
        {
            IncreaseDifficulty();
        }
    }

    private void IncreaseDifficulty()
    {
        difficulty++;
        UpdateDifficulty();
    }

    private void UpdateDifficulty()
    {
        if (difficulty == 0)
        {
            fallAppearRate = START_APPEAR_RATE;
            fallTime = START_FALL_TIME;
            //Debug.Log(difficulty + "\nFallAppearRate: " + fallAppearRate + "\nFallTime: " + fallTime);
        }
        else if (difficulty == 1)
        {
            fallAppearRate = START_APPEAR_RATE + (0.2f * (END_APPEAR_RATE - START_APPEAR_RATE));
            fallTime = START_FALL_TIME + (0.2f * (END_FALL_TIME - START_FALL_TIME));
            //Debug.Log(difficulty + "\nFallAppearRate: " + fallAppearRate + "\nFallTime: " + fallTime);
        }
        else if (difficulty == 2)
        {
            fallAppearRate = START_APPEAR_RATE + (0.4f * (END_APPEAR_RATE - START_APPEAR_RATE));
            fallTime = START_FALL_TIME + (0.4f * (END_FALL_TIME - START_FALL_TIME));
            //Debug.Log(difficulty + "\nFallAppearRate: " + fallAppearRate + "\nFallTime: " + fallTime);
        }
        else if (difficulty == 3)
        {
            fallAppearRate = START_APPEAR_RATE + (0.5f * (END_APPEAR_RATE - START_APPEAR_RATE));
            fallTime = START_FALL_TIME + (0.5f * (END_FALL_TIME - START_FALL_TIME));
            //Debug.Log(difficulty + "\nFallAppearRate: " + fallAppearRate + "\nFallTime: " + fallTime);
        }
        else if (difficulty >= 4 && difficulty < 6)
        {
            fallAppearRate = START_APPEAR_RATE + (0.6f * (END_APPEAR_RATE - START_APPEAR_RATE));
            fallTime = START_FALL_TIME + (0.6f * (END_FALL_TIME - START_FALL_TIME));
            //Debug.Log(difficulty + "\nFallAppearRate: " + fallAppearRate + "\nFallTime: " + fallTime);
        }
        else if (difficulty >= 6 && difficulty < 9)
        {
            fallAppearRate = START_APPEAR_RATE + (0.8f * (END_APPEAR_RATE - START_APPEAR_RATE));
            fallTime = START_FALL_TIME + (0.8f * (END_FALL_TIME - START_FALL_TIME));
            //Debug.Log(difficulty + "\nFallAppearRate: " + fallAppearRate + "\nFallTime: " + fallTime);
        }
        else if (difficulty >= 9 && difficulty < 15)
        {
            fallAppearRate = START_APPEAR_RATE + (1.0f * (END_APPEAR_RATE - START_APPEAR_RATE));
            fallTime = START_FALL_TIME + (1.0f * (END_FALL_TIME - START_FALL_TIME));
            //Debug.Log(difficulty + "\nFallAppearRate: " + fallAppearRate + "\nFallTime: " + fallTime);
        }
        else if (difficulty >= 15)
        {
            fallAppearRate = EXTREME_APPEAR_RATE;
            fallTime = EXTREME_FALL_TIME;
        }
    }

    public void DecreaseLives()
    {
        lives--;
        switch (lives)
        {
            case 2:
                Life1.gameObject.SetActive(false);
                break;
            case 1:
                Life2.gameObject.SetActive(false);
                break;
            case 0:
                Life3.gameObject.SetActive(false);
                EndGame();
                break;
        }
    }

    private void EndGame()
    {
        PlayerPrefs.SetInt("last_score", score);
        PlayerPrefs.SetInt("last_difficulty", difficulty);

        leftBox.GetComponent<BoxTouch>().HideShapes();
        middleBox.GetComponent<BoxTouch>().HideShapes();
        rightBox.GetComponent<BoxTouch>().HideShapes();

        //Hides the score text
        GetComponentInChildren<ScoreManager>().HideText();

        uiController.GetComponent<UIController>().ShowGameOver();
    }

    public void LeaveGame()
    {
        uiController.GetComponent<UIController>().HideGameOver();

        leftBox.GetComponent<BoxTouch>().HideShapes();
        middleBox.GetComponent<BoxTouch>().HideShapes();
        rightBox.GetComponent<BoxTouch>().HideShapes();

        GetComponentInChildren<ScoreManager>().HideText();

        uiController.GetComponent<UIController>().ShowMenu();
    }

    public void DestroyAllFallingShapes()
    {
        //Destroys all active Falling Shapes
        foreach (GameObject shape in GameObject.FindGameObjectsWithTag("FallingShape"))
        {
            Destroy(shape.gameObject);
        }
    }

    public void StartGame()
    {
        //Removes all active shapes
        DestroyAllFallingShapes();

        //Enables the hearts
        Life1.gameObject.SetActive(true);
        Life2.gameObject.SetActive(true);
        Life3.gameObject.SetActive(true);

        //Sets the Score to zero
        difficulty = 0;
        score = 0;
        lives = 3;
        GetComponentInChildren<ScoreManager>().UpdateScoreText(score.ToString());
        GetComponentInChildren<ScoreManager>().ShowText();
        isPaused = false;

        //Updates the fall rates
        UpdateDifficulty();

        //Sets the start delay
        ApplyStartDelay();

        //Prepares the boxes for a new game
        leftBox.GetComponent<BoxTouch>().PrepareBox();
        middleBox.GetComponent<BoxTouch>().PrepareBox();
        rightBox.GetComponent<BoxTouch>().PrepareBox();
    }

    public void StartGameFromPrevState()
    {
        //Removes all active shapes
        DestroyAllFallingShapes();

        //Enables the one heart for the 1 extra life
        Life3.gameObject.SetActive(true);

        //Restores the level and score form previous save
        difficulty = PlayerPrefs.GetInt("last_difficulty");
        score = PlayerPrefs.GetInt("last_score");
        lives = 1;
        GetComponentInChildren<ScoreManager>().UpdateScoreText(score.ToString());
        GetComponentInChildren<ScoreManager>().ShowText();
        isPaused = false;

        //Updates the fall rates
        UpdateDifficulty();

        //Sets the start delay
        ApplyStartDelay();

        //Shows the shapes where they left off
        leftBox.GetComponent<BoxTouch>().ShowShapes();
        middleBox.GetComponent<BoxTouch>().ShowShapes();
        rightBox.GetComponent<BoxTouch>().ShowShapes();
    }
}
