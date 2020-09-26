using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDownAnimation : MonoBehaviour
{
	public float animationDuration;
	public float animationDownLength;
	private bool isAnimating;
	private float animationStart;
    private Vector3 v1;
    private Vector3 v2;

    public void StartAnimation()
	{
        if (!isAnimating)
        {
            v1 = transform.position;
            v2 = new Vector3(transform.position.x, transform.position.y - animationDownLength, transform.position.z);
            animationStart = Time.time;
            isAnimating = true;
        }
    }

    private void Update()
	{
        if (isAnimating)
		{
            float percentCompleted = (Time.time - animationStart) / animationDuration;
            if (percentCompleted < 1)
            {
                transform.position = BezierCurve.CalculateQuadraticBezierPoint(percentCompleted, v1, v2, v1);
            }
            else
            {
                transform.position = v1;
                isAnimating = false;
            }
        }
	}

}
