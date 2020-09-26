using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButtonAnimation : MonoBehaviour
{
    public float scaleRate;
    public float rotationRate;
    public float rotationAmount;
    private Vector3 startScale;
    private float startTime;

    private void Start()
    {
        startScale = transform.localScale;
        startTime = Time.time;
    }

    private void Update()
	{
        //Scale animation
        float scaleMult = (4.0f + (0.5f * Mathf.Sin((1 / scaleRate) * (Time.time - startTime) * Mathf.PI))) / 4.0f;
        transform.localScale = new Vector3(startScale.x * scaleMult, startScale.y * scaleMult, startScale.z);

        //Rotation animation
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationAmount * Mathf.Sin((1 / rotationRate) * (Time.time - startTime) * Mathf.PI));
	}
}
