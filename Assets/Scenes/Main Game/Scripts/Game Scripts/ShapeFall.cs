using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeFall : MonoBehaviour
{
	private float fallingSpeed;
    public Sprite sprite0;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;

    public float jumpOutTime;
    public float jumpOutHeight;
    private int currentShapeIndex;
    private bool isJumpingOutOfBox;
    private float jumpOutStartTime;
    private string collidingBoxName;
    private Vector3 scrnToWrldBottomMiddle;
    private Vector3 scrnToWrldTopMiddle;
    private Vector3 jumpOutStartPos;


    // Start is called before the first frame update
    void Start()
    {
        SetRandomShape();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameBrain.isPaused)
        {
            //Will animate jump or fall down
            if (isJumpingOutOfBox)
            {
                JumpOutOfBox();
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - (fallingSpeed * Time.deltaTime), transform.position.z);
            }

            //Self destroy if shape leaves the screen
            if (transform.position.y < scrnToWrldBottomMiddle.y)
            {
                //++
                GameObject.Find("Game Canvas").GetComponent<GameBrain>().DecreaseLives();
                Destroy(this.gameObject);
            }
        }
    }

    private void SetRandomShape()
    {
        switch (Random.Range(0, 4))
        {
            case 0:
                currentShapeIndex = 0;
                GetComponent<SpriteRenderer>().sprite = sprite0;
                break;
            case 1:
                currentShapeIndex = 1;
                GetComponent<SpriteRenderer>().sprite = sprite1;
                break;
            case 2:
                currentShapeIndex = 2;
                GetComponent<SpriteRenderer>().sprite = sprite2;
                break;
            case 3:
                currentShapeIndex = 3;
                GetComponent<SpriteRenderer>().sprite = sprite3;
                break;
            default:
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            if (transform.localPosition.y - (transform.localScale.y / 2.0f) < other.transform.localPosition.y)
            {
                if (other.GetComponent<BoxTouch>().GetCurrentSelectedIndex() == currentShapeIndex && UIController.currentGameState == GameState.MainGame && !isJumpingOutOfBox)
                {
                    //+++
                    GameObject.Find("Game Canvas").GetComponent<GameBrain>().IncreaseScore();
                    other.transform.gameObject.GetComponent<BoxDownAnimation>().StartAnimation();
                    GameObject.Find("Game Canvas").GetComponent<ScoreSoundEffect>().PlayIfPossible();
                    Destroy(this.gameObject);
                }
                else
                {
                    if (!isJumpingOutOfBox)
                    {
                        if (UIController.currentGameState == GameState.MainGame)
                        {
                            GameObject.Find("Game Canvas").GetComponent<FailSoundEffect>().PlayIfPossible();
                        }
                        jumpOutStartPos = transform.position;
                        isJumpingOutOfBox = true;
                        jumpOutStartTime = Time.time;
                        collidingBoxName = other.name;
                    }
                }
            }
        }
    }

    private void JumpOutOfBox()
    {
        float lastYPos = transform.position.y;
        Vector3 p0 = jumpOutStartPos;
        Vector3 p1;
        Vector3 p2;
        switch (collidingBoxName)
        {
            case "Left Box":
                p2 = new Vector3(jumpOutStartPos.x - (10.0f * transform.localScale.x), jumpOutStartPos.y, jumpOutStartPos.z);
                p1 = new Vector3((p0.x + p2.x) / 2.0f, jumpOutStartPos.y + jumpOutHeight, jumpOutStartPos.z);
                transform.position = BezierCurve.CalculateQuadraticBezierPoint((Time.time - jumpOutStartTime) / jumpOutTime, p0, p1, p2);
                break;
            case "Middle Box":
                p2 = new Vector3(jumpOutStartPos.x, jumpOutStartPos.y, jumpOutStartPos.z);
                p1 = new Vector3(jumpOutStartPos.x, jumpOutStartPos.y + jumpOutHeight, jumpOutStartPos.z);
                transform.position = BezierCurve.CalculateQuadraticBezierPoint((Time.time - jumpOutStartTime) / jumpOutTime, p0, p1, p2);
                break;
            case "Right Box":
                p2 = new Vector3(jumpOutStartPos.x + (10.0f * transform.localScale.x), jumpOutStartPos.y, jumpOutStartPos.z);
                p1 = new Vector3((p0.x + p2.x) / 2.0f, jumpOutStartPos.y + jumpOutHeight, jumpOutStartPos.z);
                transform.position = BezierCurve.CalculateQuadraticBezierPoint((Time.time - jumpOutStartTime) / jumpOutTime, p0, p1, p2);
                break;
            default:
                break;
        }

        //Will change layer when the shape is coming down
        if (lastYPos > transform.position.y)
        {
            GetComponent<SpriteRenderer>().sortingOrder += 1;
        }
    }

    public void SetFallSpeed(float fallTime)
    {
        scrnToWrldBottomMiddle = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2.0f, 0, 0));
        scrnToWrldTopMiddle = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2.0f, Screen.height, 0));
        fallingSpeed = (Mathf.Abs(scrnToWrldTopMiddle.y) + Mathf.Abs(scrnToWrldBottomMiddle.y)) / fallTime;
    }

}
