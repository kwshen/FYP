using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private Texture2D nightSkybox;
    [SerializeField] private Texture2D sunriseSkybox;
    [SerializeField] private Texture2D daySkybox;
    [SerializeField] private Texture2D sunsetSkybox;

    [SerializeField] private Gradient lightGraddientNightToSunrise;
    [SerializeField] private Gradient lightGraddientSunriseToDay;
    [SerializeField] private Gradient lightGraddientDayToSunset;
    [SerializeField] private Gradient lightGraddientSunsetToNight;

    [SerializeField] private Light globalLight;

    private int minutes;

    public int Minutes
    { get { return minutes; } set { minutes = value; OnMinutesChange(value); } }

    private int hours = 5;

    public int Hours
    { get { return hours; } set { hours = value; OnHoursChange(value); } }

    private int days;

    public int Days
    { get { return days; } set { days = value; } }

    private float sec;

    public void Update()
    {
        if (globalLight == null)
        {
            // Try to find the light in the new scene
            globalLight = GameObject.FindObjectOfType<Light>();

            if (globalLight == null)
            {
                Debug.LogWarning("No light found in the current scene.");
                return;
            }
        }

        sec += Time.deltaTime;

        if (sec >= 1)
        {
            Minutes += 1;
            sec = 0;
        }
    }

    private void OnMinutesChange(int value)
    {
        globalLight.transform.Rotate(Vector3.up, (1f / (1440f / 4f)) * 360f, Space.World);
        if (value >= 60)
        {
            Hours++;
            minutes = 0;
        }
        if (Hours >= 24)
        {
            Hours = 0;
            Days++;
        }
    }

    private void OnHoursChange(int value)
    {
        if (value == 6)
        {
            StartCoroutine(LerpSkybox(nightSkybox, sunriseSkybox, 10f));
            StartCoroutine(LerpLight(lightGraddientNightToSunrise, 10f));
        }
        else if (value == 8)
        {
            StartCoroutine(LerpSkybox(sunriseSkybox, daySkybox, 10f));
            StartCoroutine(LerpLight(lightGraddientSunriseToDay, 10f));
        }
        else if (value == 18)
        {
            StartCoroutine(LerpSkybox(daySkybox, sunsetSkybox, 10f));
            StartCoroutine(LerpLight(lightGraddientDayToSunset, 10f));
        }
        else if (value == 22)
        {
            StartCoroutine(LerpSkybox(sunsetSkybox, nightSkybox, 10f));
            StartCoroutine(LerpLight(lightGraddientSunsetToNight, 10f));
        }
    }

    private IEnumerator LerpSkybox(Texture2D a, Texture2D b, float time)
    {
        RenderSettings.skybox.SetTexture("_Texture1", a);
        RenderSettings.skybox.SetTexture("_Texture2", b);
        RenderSettings.skybox.SetFloat("_Blend", 0);
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            RenderSettings.skybox.SetFloat("_Blend", i / time);
            yield return null;
        }
        RenderSettings.skybox.SetTexture("_Texture1", b);
    }

    private IEnumerator LerpLight(Gradient lightGradient, float time)
    {
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            globalLight.color = lightGradient.Evaluate(i / time);
            RenderSettings.fogColor = globalLight.color;
            yield return null;
        }
    }
}