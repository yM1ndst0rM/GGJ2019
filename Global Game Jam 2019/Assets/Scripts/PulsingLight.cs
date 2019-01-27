using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Experimental.GlobalIllumination;

public class PulsingLight : MonoBehaviour
{
    const float FULL_CIRCLE_IN_RAD = Mathf.PI * 2;
    private Light LightSource;
    public float Frequency = 1;
    private float maxIntensity;
    private float currentRotationAngle = 0;

    // Start is called before the first frame update
    void Start()
    {
        Assert.AreNotApproximatelyEqual(0, Frequency, "Frequency my not be 0");

        LightSource = GetComponent<Light>();
        maxIntensity = LightSource.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        currentRotationAngle = (currentRotationAngle + Time.deltaTime * FULL_CIRCLE_IN_RAD * Frequency) % FULL_CIRCLE_IN_RAD;

        LightSource.intensity = Mathf.Abs(Mathf.Cos(currentRotationAngle / 2)) * maxIntensity;
    }
}
