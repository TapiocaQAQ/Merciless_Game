using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    [SerializeField]private Light directionalLight;
    [SerializeField]private LightingPreset preset;
    [SerializeField, Range(0, 24)]private float timeOfDay = 12f;

    void Update() 
    {
        if(preset == null){
            return;
        }

        if(Application.isPlaying)
        {
            timeOfDay += Time.deltaTime * preset.sunSpeed;
            timeOfDay %= 24;
            UpdateLighting(timeOfDay / 24);
        }
    }

    void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = preset.ambientColor.Evaluate(timePercent);
        RenderSettings.fogColor = preset.fogColor.Evaluate(timePercent);

        if(directionalLight != null){
            directionalLight.color = preset.directionalColor.Evaluate(timePercent);
            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }
    }

    void OnValidate() 
    {
        if(directionalLight != null){
            return;
        }

        if(RenderSettings.sun != null)
        {
            directionalLight = RenderSettings.sun;
        }else{
            Debug.Log("There dont have the sun");
        }
    }
}
