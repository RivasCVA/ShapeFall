using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuJumpShape : MonoBehaviour
{
    public GameObject menuBoxLeft;
    public GameObject menuBoxMiddle;
    public GameObject menuBoxRight;
    public Sprite shape0;
    public Sprite shape1;
    public Sprite shape2;
    public Sprite shape3;

    public int startShapeIndex;
    private int currentShapeIndex;
    public int startBoxIndex;
    private int currentBoxIndex;

    public float jumpTime;
    public float jumpHeight;
    private float jumpStartTime;
    private Vector3 jumpPeakPosition;
    private Vector3 jumpDepartPosition;
    private Vector3 jumpDepartOffset;
    private Vector3 jumpLandPosition;
    private Vector3 jumpLandOffset;


    // Start is called before the first frame update
    void Start()
    {
        //Sets the sprite based on the given Index
        currentShapeIndex = startShapeIndex - 1;
        SetNextSprite();

        //Centers the shape to a certain box
        currentBoxIndex = startBoxIndex - 1;
        SetNextBoxJumpPositions();

        //Sets the start time for the jumping
        SetJumpStartDelay(0.5f);
    }

    void OnEnable()
    {
        SetNextSprite();
        SetNextBoxJumpPositions();
        SetJumpStartDelay(0.75f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > jumpStartTime)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            float jumpTimeElapsed = Time.time - jumpStartTime;
            if (jumpTimeElapsed / jumpTime < 1.0f)
            {
                transform.position = BezierCurve.CalculateQuadraticBezierPoint(jumpTimeElapsed / jumpTime, jumpDepartPosition + jumpDepartOffset, jumpPeakPosition, jumpLandPosition + jumpLandOffset);
            }
            else
            {
                GetComponent<SpriteRenderer>().enabled = false;
                SetNextBoxJumpPositions();
                switch (currentBoxIndex)
                {
                    case 0:
                        menuBoxLeft.GetComponent<BoxDownAnimation>().StartAnimation();
                        break;
                    case 1:
                        menuBoxMiddle.GetComponent<BoxDownAnimation>().StartAnimation();
                        break;
                    case 2:
                        menuBoxRight.GetComponent<BoxDownAnimation>().StartAnimation();
                        break;
                    default:
                        break;
                }
                SetNextSprite();
                SetJumpStartDelay(1);
            }
        }
    }

    private void SetJumpStartDelay(float delay)
    {
        jumpStartTime = Time.time + delay;
    }

    private void SetNextBoxJumpPositions()
    {
        if (++currentBoxIndex > 2)
        {
            currentBoxIndex = 0;
        }

        switch (currentBoxIndex)
        {
            case 0:
                transform.position = menuBoxLeft.transform.position;
                jumpDepartPosition = menuBoxLeft.transform.position;
                jumpLandPosition = menuBoxMiddle.transform.position;
                break;
            case 1:
                transform.position = menuBoxMiddle.transform.position;
                jumpDepartPosition = menuBoxMiddle.transform.position;
                jumpLandPosition = menuBoxRight.transform.position;
                break;
            case 2:
                transform.position = menuBoxRight.transform.position;
                jumpDepartPosition = menuBoxRight.transform.position;
                jumpLandPosition = menuBoxLeft.transform.position;
                break;
            default:
                transform.position = menuBoxLeft.transform.position;
                jumpDepartPosition = menuBoxLeft.transform.position;
                jumpLandPosition = menuBoxMiddle.transform.position;
                break;
        }

        float heightIncrease = 0;
        if (currentBoxIndex == 2)
        {
            heightIncrease = 1.0f;
        }
        jumpPeakPosition = new Vector3((jumpDepartPosition.x + jumpLandPosition.x) / 2.0f, menuBoxMiddle.transform.position.y + jumpHeight + heightIncrease, 0);

        float offsetAmount = 0.6f;
        if (jumpDepartPosition.x < jumpLandPosition.x)
        {
            jumpDepartOffset = new Vector3(-offsetAmount, 0, 0);
            jumpLandOffset = new Vector3(offsetAmount, 0, 0);
        }
        else
        {
            jumpDepartOffset = new Vector3(offsetAmount, 0, 0);
            jumpLandOffset = new Vector3(-offsetAmount, 0, 0);
        }
    }

    private void SetNextSprite()
    {
        if (++currentShapeIndex > 3)
        {
            currentShapeIndex = 0;
        }

        switch (currentShapeIndex)
        {
            case 0:
                GetComponent<SpriteRenderer>().sprite = shape0;
                break;
            case 1:
                GetComponent<SpriteRenderer>().sprite = shape1;
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = shape2;
                break;
            case 3:
                GetComponent<SpriteRenderer>().sprite = shape3;
                break;
            default:
                GetComponent<SpriteRenderer>().sprite = shape0;
                break;
        }
    }
}
