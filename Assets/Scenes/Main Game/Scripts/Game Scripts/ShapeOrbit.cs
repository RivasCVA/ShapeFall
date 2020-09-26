using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeOrbit : MonoBehaviour
{
    public int shapeIndex;
    private const int ORBIT_RADIUS = 4;
    private const float ORBIT_SPEED = 650;

    private int lastFrameCurrentSelectedIndex;
    private bool isAnimatingOrbitIN;
    private bool isAnimatingOrbitOUT;

    // Start is called before the first frame update
    void Start()
    {
        //Gets the current selected index
        lastFrameCurrentSelectedIndex = transform.parent.GetComponent<BoxTouch>().GetCurrentSelectedIndex();

        //Hides if not in front
        if (shapeIndex != lastFrameCurrentSelectedIndex)
        {
            Hide();
        }

        //Sets to default position and rotation (zeros)
        transform.position = new Vector3(0, 0, transform.position.z);
        transform.rotation = Quaternion.Euler(0, 0, 0);

        //Rotates each shape to its designated spot
        transform.localPosition = new Vector3(0, 0, -ORBIT_RADIUS);
        switch (shapeIndex)
        {
            case 0:
                transform.RotateAround(transform.parent.transform.position, Vector3.down, 0);
                break;
            case 1:
                transform.RotateAround(transform.parent.transform.position, Vector3.down, 90);
                break;
            case 2:
                transform.RotateAround(transform.parent.transform.position, Vector3.down, 180);
                break;
            case 3:
                transform.RotateAround(transform.parent.transform.position, Vector3.down, 270);
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        int currentSelectedIndex = transform.parent.GetComponent<BoxTouch>().GetCurrentSelectedIndex();

        //Will Move Shape In
        if (shapeIndex == currentSelectedIndex && IsHidden())
        {
            Show();
            isAnimatingOrbitIN = true;
        }
        //Will Move Shape Out
        else if (shapeIndex != currentSelectedIndex && !IsHidden())
        {
            if (isAnimatingOrbitIN) { CompleteOrbitIN(); }
            isAnimatingOrbitOUT = true;
            //transform.RotateAround(transform.parent.transform.position, Vector3.up, 90);
            //Debug.Log(transform.localPosition.x + " - " + transform.localPosition.y + " - " + transform.localPosition.z);
        }
        //Will Rotate to Next Position in the Background
        else if (lastFrameCurrentSelectedIndex != currentSelectedIndex)
        {
            transform.RotateAround(transform.parent.transform.position, Vector3.up, 90);
        }
        lastFrameCurrentSelectedIndex = currentSelectedIndex;

        //Animates if still needs to complete Animation
        if (isAnimatingOrbitIN) { AnimateOrbitIN(); }

        if (isAnimatingOrbitOUT) { AnimateOrbitOUT(); }
    }

    public void RestartOrbit()
    {
        Start();
    }

    private void AnimateOrbitIN()
    {
        float rotationYDiff = System.Math.Abs(System.Math.Abs(transform.localRotation.eulerAngles.y) - 0.0f);
        float minAcceptedDifference = 40;
        if (rotationYDiff > minAcceptedDifference)
        {
            transform.RotateAround(transform.parent.position, Vector3.up, ORBIT_SPEED * Time.deltaTime);
        }
        else
        {
            CompleteOrbitIN();
        }
    }

    private void CompleteOrbitIN()
    {
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        transform.localPosition = new Vector3(0, 0, -ORBIT_RADIUS);
        isAnimatingOrbitIN = false;
    }

    private void AnimateOrbitOUT()
    {
        float rotationYDiff = System.Math.Abs(System.Math.Abs(transform.localRotation.eulerAngles.y) - 90.0f);
        float minAcceptedDifference = 40;
        if (rotationYDiff > minAcceptedDifference)
        {
            transform.RotateAround(transform.parent.position, Vector3.up, ORBIT_SPEED * Time.fixedDeltaTime);
        }
        else
        {
            CompleteOrbitOUT();
        }
    }

    private void CompleteOrbitOUT()
    {
        transform.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
        transform.localPosition = new Vector3(-ORBIT_RADIUS, 0, 0);
        Hide();
        isAnimatingOrbitOUT = false;
    }

    private void Hide()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
    }

    private bool IsHidden()
    {
        return !gameObject.GetComponent<Renderer>().enabled;
    }

    private void Show()
    {
        gameObject.GetComponent<Renderer>().enabled = true;
    }

}
