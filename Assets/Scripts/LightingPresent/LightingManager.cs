using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingManager : MonoBehaviour
{
    #region Singleton

    public static LightingManager instance;

    private void Awake() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this);
        }else{
            LightingManager.instance.Destroy();
            instance = this;
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    #endregion

    [SerializeField]private Light directionalLight;
    [SerializeField]private LightingPreset preset;
    [SerializeField, Range(0, 24)]private float timeOfDay = 12f;//17:time to break, //5.5:time to work
    public bool isWorkingTime;

    void Update() 
    {
        if(preset == null){
            return;
        }

        timeOfDay += Time.deltaTime * preset.sunSpeed;
        timeOfDay %= 24;
        UpdateLighting(timeOfDay / 24);

        if(timeOfDay > 5.5 && timeOfDay < 17){
            isWorkingTime = true;
        }else{
            isWorkingTime = false;
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
