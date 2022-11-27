using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LightingController : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    //Variables
    [SerializeField, Range(0, 24)] private float TimeOfDay;
    [SerializeField] SceneInfo sceneInfo;
    [SerializeField] int timeScale = 10;
    [SerializeField] GameObject rainParticle;
    private bool dayChanged = false;
    private void Start() {
        TimeOfDay = sceneInfo.dayTime;
    }

    private void Update()
    {
        EmitRainWhenOutsideHome();
        if (Preset == null)
            return;
        this.GetComponent<Light>().intensity = sceneInfo.isRain? 0.5f : 1;

        if (Application.isPlaying)
        {
            if(SceneManager.GetActiveScene().name == "Home") return; //time dont increase when player is inside house
            TimeOfDay += Time.deltaTime / timeScale;
            TimeOfDay %= 24; //Modulus to ensure always between 0-24
            UpdateLighting(TimeOfDay / 24f);

            if(Mathf.Floor(TimeOfDay) == 0 && !dayChanged){
                dayChanged = true;
                sceneInfo.isRain = RainWheater();
                sceneInfo.SetGameTime(sceneInfo.gameTime + 1);//increase gametime (day) when daytime reach 0
            }
            if(Mathf.Floor(TimeOfDay) > 0){
                dayChanged = false;
            }
            sceneInfo.SetDayTime(TimeOfDay);
        }
        else
        {
            UpdateLighting(TimeOfDay / 24f);
        }
    }

    void EmitRainWhenOutsideHome(){
        if(sceneInfo.isRain && SceneManager.GetActiveScene().name == "Home") rainParticle.SetActive(false);
        if(sceneInfo.isRain && SceneManager.GetActiveScene().name != "Home") rainParticle.SetActive(true);
        if(!sceneInfo.isRain) rainParticle.SetActive(false);
    }

    private void UpdateLighting(float timePercent)
    {
        //Set ambient and fog
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        //If the directional light is set then rotate and set it's color, I actually rarely use the rotation because it casts tall shadows unless you clamp the value
        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);

            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }

    }

    //Try to find a directional light to use if we haven't set one
    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;

        //Search for lighting tab sun
        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        //Search scene for light that fits criteria (directional)
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
    private bool RainWheater(){
        int rInt = Random.Range(0, 100);
        Debug.Log(rInt);
        return rInt >= 60? true : false;
    }
}
