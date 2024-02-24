using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SunGlitterController : MonoBehaviour
{
    public DayNightController dayNightController;
    public float timeSlider;

    public Light2D oceanDiffuseLight1;
    public Gradient oceanDiffuse1Gradient;

    public Light2D oceanDiffuseLight2;
    public Light2D oceanDiffuseLight3;
    public Gradient oceanDiffuse2Gradient;
    public Gradient oceanDiffuse3Gradient;

    public Light2D oceanSparkleLight;
    public Gradient oceanSparkleGradient;
    public AnimationCurve oceanSparkleInnerCurve;

    public List<Light2D> oceanSpecularLightList = new List<Light2D>();
    public Gradient oceanSpecularGradient;
    public AnimationCurve specularFallOffCurve;

    void UpdateOceanDifusseColor()
    {
        oceanDiffuseLight1.color = oceanDiffuse1Gradient.Evaluate(timeSlider);
        oceanDiffuseLight2.color = oceanDiffuse2Gradient.Evaluate(timeSlider);
        oceanDiffuseLight3.color = oceanDiffuse3Gradient.Evaluate(timeSlider);
    }

    void UpdateOceanSpecularColor()
    {
        oceanSparkleLight.color = oceanSparkleGradient.Evaluate(timeSlider);
        oceanSparkleLight.pointLightInnerRadius = oceanSparkleInnerCurve.Evaluate(timeSlider);
        foreach (Light2D i in oceanSpecularLightList)
        {
            i.color = oceanSpecularGradient.Evaluate(timeSlider);
            i.falloffIntensity = specularFallOffCurve.Evaluate(timeSlider);
        }
    }


    void Update()
    {
        timeSlider = dayNightController.timeSlider;
        UpdateOceanDifusseColor();
        UpdateOceanSpecularColor();

    }
}
