using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class DayNightSystem : MonoBehaviour
{
    public Light directionalLight;

    public float dayDurationInseconds = 24.0f; //Adjust the duration of a full day in seconds
    public int currentHour;
    float currentTimeOfDay = 0.0f;

    public List<SkyboxTimeMapping> timeMappings;
    float blendedValue = 0.0f;



    // Update is called once per frame
    void Update()
    {
        currentTimeOfDay += Time.deltaTime / dayDurationInseconds;
        currentTimeOfDay %= 1;

        currentHour = Mathf.FloorToInt(currentTimeOfDay * 24);

        directionalLight.transform.rotation = Quaternion.Euler(new Vector3((currentTimeOfDay * 360) - 90, 170, 0));

        UpdateSkybox();
    }




    private void UpdateSkybox()
    {
        // Find the appropriate skybox material for the current hour.
        Material currentSkybox = null;
        foreach (SkyboxTimeMapping mapping in timeMappings)
        {
            if (currentHour == mapping.hour)
            {
                currentSkybox = mapping.skyboxMaterial;

                if (currentSkybox.shader != null)
                {
                    if (currentSkybox.shader.name == "Custom/SkyboxTransition")
                    {
                        blendedValue += Time.deltaTime;
                        blendedValue = Mathf.Clamp01(blendedValue);

                        currentSkybox.SetFloat("_TransitionFactor", blendedValue);
                    }
                }
                else
                {
                    blendedValue = 0;
                }
            }

            break;
        }
        if (currentSkybox != null)
        {
            RenderSettings.skybox = currentSkybox;
        }
    }

    [System.Serializable]
    public class SkyboxTimeMapping
    {
        public string phaseName;
        public int hour; // the hour of the day
        public Material skyboxMaterial;
    }
}
