using System;
using UnityEngine;
using TMPro;

public class TimeHandler : MonoBehaviour
{
    [Header("Timings")]
    [SerializeField] private float m_TimeMultiplier;
    [SerializeField] private float m_StartTime;
    [SerializeField] private float m_Sunrise;
    [SerializeField] private float m_Sunset;

    [Header("Sky")]
    [SerializeField] private AnimationCurve m_LightChangeCurve;

    [Header("Day")]
    [SerializeField] private Light m_Sun;
    [SerializeField] private Color m_AmbientLightDay;
    [SerializeField] private float m_MaxSunLightIntensity;

    [Header("Night")]
    [SerializeField] private Light m_Moon;
    [SerializeField] private Color m_AmbientLightNight;
    [SerializeField] private float m_MaxMoonLightIntensity;

    [Header("Clock")]
    [SerializeField] private TextMeshProUGUI m_Clock;

    private DateTime m_CurrentTime;
    private TimeSpan m_SunriseTime;
    private TimeSpan m_SunsetTime;

    void Start()
    {
        m_CurrentTime = DateTime.Now.Date + TimeSpan.FromHours(m_StartTime);
        m_SunriseTime = TimeSpan.FromHours(m_Sunrise);
        m_SunsetTime = TimeSpan.FromHours(m_Sunset);
    }

    void Update()
    {
        UpdateTimeOfDay();
        RotateSun();
        UpdateLightSettings();
        CheckShift();
    }

    private void CheckShift()
    {
        if (m_CurrentTime.TimeOfDay < m_SunriseTime || m_CurrentTime.TimeOfDay > m_SunsetTime)
            GameManager.Instance.IsOnShift = false;
    }

    /// <summary>
    /// Updates the clock (i.e., the TextMeshPro UI element which displays the time).
    /// </summary>
    private void UpdateTimeOfDay()
    {
        m_CurrentTime = m_CurrentTime.AddSeconds(Time.deltaTime * m_TimeMultiplier);
        if (m_Clock) m_Clock.text = m_CurrentTime.ToString("HH:mm");
    }

    /// <summary>
    /// Rotates the sun (i.e., the Directional Light) as to simulate the sun rising and setting.
    /// </summary>
    private void RotateSun()
    {
        float sunRotation;
        double percentOfTimePassed;
        // Check if day time
        if (m_CurrentTime.TimeOfDay > m_SunriseTime && m_CurrentTime.TimeOfDay < m_SunsetTime)
        {
            TimeSpan sunsetToSunrise = CalculateTimeDifference(m_SunsetTime, m_SunriseTime);
            TimeSpan sinceSunrise = CalculateTimeDifference(m_SunriseTime, m_CurrentTime.TimeOfDay);

            percentOfTimePassed = sinceSunrise.TotalMinutes / sunsetToSunrise.TotalMinutes;

            sunRotation = Mathf.Lerp(0, 180, (float)percentOfTimePassed);
        }
        else // Else night time
        {
            TimeSpan sunriseToSunset = CalculateTimeDifference(m_SunsetTime, m_SunriseTime);
            TimeSpan sinceSunset = CalculateTimeDifference(m_SunriseTime, m_CurrentTime.TimeOfDay);

            percentOfTimePassed = sinceSunset.TotalMinutes / sunriseToSunset.TotalMinutes;

            sunRotation = Mathf.Lerp(180, 360, (float)percentOfTimePassed);
        }
        m_Sun.transform.rotation = Quaternion.AngleAxis(sunRotation, Vector3.right);
    }

    /// <summary>
    /// As to give the scene a more realistic look the light's day/night cycle effect, the ambiant color needs to update with the rotation too.
    /// </summary>
    private void UpdateLightSettings()
    {
        // If 1 = sun facing down | 0 = horizontal | -1 = up
        float dot = Vector3.Dot(m_Sun.transform.forward, Vector3.down);
        m_Sun.intensity = Mathf.Lerp(0, m_MaxSunLightIntensity, m_LightChangeCurve.Evaluate(dot));
        m_Moon.intensity = Mathf.Lerp(m_MaxMoonLightIntensity, 0, m_LightChangeCurve.Evaluate(dot));
        RenderSettings.ambientLight = Color.Lerp(m_AmbientLightNight, m_AmbientLightDay, m_LightChangeCurve.Evaluate(dot));
    }

    private TimeSpan CalculateTimeDifference(TimeSpan from, TimeSpan to)
    {
        TimeSpan difference = to - from;
        if (difference.TotalSeconds < 0)
            difference += TimeSpan.FromHours(24);
        return difference;
    }
}
