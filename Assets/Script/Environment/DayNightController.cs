using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class DayNightController : MonoBehaviour
{
    [Range(0,1)]
    public float timeSlider;
    public float timeDelta;
   

    public Renderer skyGradient;
    public Gradient skyTopColor;
    public Gradient skyMiddleColor;
    public Gradient skyBottomColor;

    public SpriteRenderer oceanBase;
    public SpriteRenderer oceanShadow;
    public Gradient oceanBaseColor;
    public Gradient oceanShadowColor;

    public Light2D cloudLight;
    public Gradient cloudGradient;

    public Light2D terrainLight;
    public Gradient terrainGradient;

    public Light2D frontLight;
    public Gradient frontGradient;

    public Light2D cloudSpotLight;
    public Gradient cloudSpotGradient;

    public Light2D terrainSpotLight;
    public Gradient terrainSpotGradient;

    public Light2D frontSpotLight;
    public Gradient frontSpotGradient;

    public Light2D cloudNormalLight;
    public Gradient cloudNormalGradient;

    public Light2D sunLight;
    public Gradient sunLightGradient;

    public Light2D lensFlare;
    public Gradient lensFlareGradient;

    public Transform sun;
    public AnimationCurve sunAnimationCurve;

    public Transform moon;
    public AnimationCurve moonAnimationCurve;

    public Renderer oceanSpecular;
    public AnimationCurve specularIntenseCurve;

    private bool isDay = true;
    void UpdateSkyColor()
    {
        skyGradient.material.SetColor("_TopColor", skyTopColor.Evaluate(timeSlider));
        skyGradient.material.SetColor("_MiddleColor", skyMiddleColor.Evaluate(timeSlider));
        skyGradient.material.SetColor("_BottomColor", skyBottomColor.Evaluate(timeSlider));
    }

    void UpdateOceanColor()
    {
        oceanBase.color = oceanBaseColor.Evaluate(timeSlider);
        oceanShadow.color = oceanShadowColor.Evaluate(timeSlider);
    }

    void UpdateGlobalLight()
    {
        cloudLight.color = cloudGradient.Evaluate(timeSlider);
        terrainLight.color = terrainGradient.Evaluate(timeSlider);
        frontLight.color = frontGradient.Evaluate(timeSlider);
    }

    void UpdateSpotLight()
    {
        cloudSpotLight.color = cloudSpotGradient.Evaluate(timeSlider);
        terrainSpotLight.color = terrainSpotGradient.Evaluate(timeSlider);
        frontSpotLight.color = frontSpotGradient.Evaluate(timeSlider);
    }

    float CalculateCurveDelta(AnimationCurve curve)
    {
        float valueAtT = curve.Evaluate(timeSlider);
        float valueAtDeltaT = isDay ? curve.Evaluate(timeSlider + timeDelta) : curve.Evaluate(timeSlider - timeDelta);

        float deltaCurve = valueAtDeltaT - valueAtT;

        return deltaCurve;
    }

    void UpdateSunMoonPosition()
    {
        float derivativeAtT = CalculateCurveDelta(sunAnimationCurve);
        Vector3 sunPos = new Vector3(0, derivativeAtT, 0);
        sun.position += sunPos;

        derivativeAtT = CalculateCurveDelta(moonAnimationCurve);
        Vector3 moonPos = new Vector3(0, derivativeAtT, 0);
        moon.position += moonPos;
    }
    
    void UpdateCloudNormal()
    {
        cloudNormalLight.color = cloudNormalGradient.Evaluate(timeSlider);
    }

    void UpdateSunLight()
    {
        sunLight.color = sunLightGradient.Evaluate(timeSlider);
        lensFlare.color= lensFlareGradient.Evaluate(timeSlider);
    }

    void Update()
    {
        UpdateSkyColor();
        UpdateOceanColor();
        UpdateGlobalLight();
        UpdateSpotLight();
        UpdateCloudNormal();
        UpdateSunLight();
    }

    void FixedUpdate()
    {
        if (isDay)
        {
            timeSlider += timeDelta;
            if (timeSlider >= 1.05f)
            {
                isDay = false;
            }

        }
        else
        {
            timeSlider -= timeDelta;
            if (timeSlider <= -0.05f)
            {
                isDay = true;
            }
        }
        
        UpdateSunMoonPosition();
    }
}
