using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    [SerializeField, Range(0, 24)] private float TimeOfDay;
    
    [SerializeField, Range(-10, 10)] private float speedMultiplier;
    [SerializeField, Range(0, 10)] private float nightSpeed;
    [SerializeField] private float maxIntensity = 1.5f;
    private float baseIntensity = 0f;
    [SerializeField] private float maxShadowStrength = 2f;
    [SerializeField] private float minShadowStrength = 0.5f;
    private float nightSpeedUpStart = 22f;
    private float nightSpeedUpEnd = 4f;
    private float dawn = 6f;
    private float dusk = 18f;
    private float noon = 12f;
    [SerializeField] private Material clouds;

    private float updateInterval = 0.1f;
    private float timeSinceLastUpdate = 0f;
 
    private void Start()
    {
        speedMultiplier = 0.05f; 
        nightSpeed = 0.25f;
        baseIntensity = maxIntensity / 2f;
    }

    private void Update()
    {
        timeSinceLastUpdate += Time.deltaTime;

        if (timeSinceLastUpdate >= updateInterval)
        {
            if (Preset == null)
                return;

            if (Application.isPlaying)
            {
                if (TimeOfDay > nightSpeedUpStart || TimeOfDay < nightSpeedUpEnd)
                {
                    TimeOfDay += Time.deltaTime * speedMultiplier * nightSpeed;
                    TimeOfDay += Time.deltaTime * nightSpeed;
                }
                else
                {
                    TimeOfDay += Time.deltaTime * speedMultiplier;

                    if (TimeOfDay >= dawn && TimeOfDay <= noon)
                    {
                        DirectionalLight.intensity = Mathf.Clamp(baseIntensity + (baseIntensity / (noon - dawn)) * (TimeOfDay - dawn), 0, maxIntensity);
                        DirectionalLight.shadowStrength = Mathf.Clamp(minShadowStrength + ((maxShadowStrength - minShadowStrength) / (noon - dawn)) * (TimeOfDay - dawn), 0, 1);
                    }
                    else if (TimeOfDay > noon && TimeOfDay <= dusk)
                    {
                        DirectionalLight.intensity = Mathf.Clamp(baseIntensity + (baseIntensity / (dusk - noon)) * (dusk - TimeOfDay), 0, maxIntensity);
                        DirectionalLight.shadowStrength = Mathf.Clamp(minShadowStrength + ((maxShadowStrength - minShadowStrength) / (dusk - noon)) * (dusk - TimeOfDay), 0, 1);
                    }
                    else
                    {
                        DirectionalLight.intensity = Mathf.Clamp(baseIntensity, 0, maxIntensity);
                        DirectionalLight.shadowStrength = Mathf.Clamp(minShadowStrength, 0, 1);
                    }
                }

                TimeOfDay %= 24;
                UpdateLighting(TimeOfDay / 24f);
            }
            else
            {
                UpdateLighting(TimeOfDay / 24f);
            }

            timeSinceLastUpdate = 0f;
        }
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);

        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);

            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }

        if (clouds != null)
        {
            clouds.SetColor("_Color", Preset.CloudColor.Evaluate(timePercent));
        }
    }

    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;

        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }

}
