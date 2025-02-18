using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuTime : MonoBehaviour
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
    public Material clouds;

    private void Start()
    {
        speedMultiplier = 0; 
        nightSpeed = 0;
        baseIntensity = maxIntensity / 2f;
    }


    private void Update()
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
                    DirectionalLight.intensity = baseIntensity + (baseIntensity / (noon - dawn)) * (TimeOfDay - dawn);
                    DirectionalLight.shadowStrength = minShadowStrength + ((maxShadowStrength - minShadowStrength) / (noon - dawn)) * (TimeOfDay - dawn);
                    clouds.color = Color.white;
                }
                else if (TimeOfDay > noon && TimeOfDay <= dusk)
                {
                    DirectionalLight.intensity = baseIntensity + (baseIntensity / (dusk - noon)) * (dusk - TimeOfDay);
                    DirectionalLight.shadowStrength = minShadowStrength + ((maxShadowStrength - minShadowStrength) / (dusk - noon)) * (dusk - TimeOfDay);
                    clouds.color = Color.white;
                }
                else
                {
                    DirectionalLight.intensity = baseIntensity;
                    DirectionalLight.shadowStrength = minShadowStrength;
                    clouds.color = Color.Lerp(clouds.color, Color.black, 10f);
                }
            }
            TimeOfDay %= 24;
            UpdateLighting(TimeOfDay / 24f);
        }
        else
        {
            UpdateLighting(TimeOfDay / 24f);
        }
    }


    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);

            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
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
