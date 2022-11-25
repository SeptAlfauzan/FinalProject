using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
public class SavePlantController : MonoBehaviour
{
    private GameObject[] plants;
    private List<Plants> plantsBehaviour;
    public PlantLocationData plantLocationData;
    public GameObject plantParentGameobject;

    private void Start() {
        if(SceneManager.GetActiveScene().name == "Farm") InstantiateSavedPlant();
        plants = GameObject.FindGameObjectsWithTag("Plant");
    }
    private void Update() {
        if(plants.Length != GameObject.FindGameObjectsWithTag("Plant").Length) plants = GameObject.FindGameObjectsWithTag("Plant");
    }

    public void SaveCurrentData(){
        // string sceneName =  SceneManager.GetActiveScene().name;

        // inventory.StoreToSceneInfo(sceneName);
       if(SceneManager.GetActiveScene().name == "Farm") SavePlantState();
    }

    private void SavePlantState(){
        int index = 0;
        plantLocationData.Reset();//to avoid redundant plant

        List<PlantedPlant> tempPlantedPlants = new List<PlantedPlant>();
        foreach (GameObject plantedPlant in plants){
            Plants plantData = plantedPlant.GetComponent<Plants>();

            tempPlantedPlants.Add(
                new PlantedPlant(plants[index].transform.position,
                plants[index].transform.localScale,
                plantedPlant.GetComponent<Plants>().plantItemData.prefabData,
                plantData.plantAge,
                plantData.harvestTime,
                plantData.isWatered,
                plantData.lastDayWatered
                ));
            index++;
        }
        Debug.Log("save plant"+index);
        plantLocationData.SetPlantedLocation(tempPlantedPlants);
    }

    private void InstantiateSavedPlant(){
        try{
            List<PlantedPlant> plantedPlants = plantLocationData.GetAllPlantedPlant();
            if(plantedPlants == null) return;

            int i = 0;
            foreach (PlantedPlant plantedPlant in plantedPlants){
                GameObject newPlant = Instantiate(plantedPlant.GetPlant(), plantParentGameobject.transform);

                newPlant.transform.position = plantedPlant.GetLocation();
                newPlant.transform.localScale = plantedPlant.GetSize();
                newPlant.GetComponent<Plants>().plantAge = plantedPlant.GetPlantAge();
                newPlant.GetComponent<Plants>().harvestTime = plantedPlant.GetHarvestAge();
                newPlant.GetComponent<Plants>().isWatered = plantedPlant.GetWateredStatus();
                newPlant.GetComponent<Plants>().lastDayWatered = plantedPlant.GetLastDayWatered();
                i++;
            }
            Debug.Log("instatiate"+i);
        }
        catch (System.Exception exception)
        {
            Debug.Log("error instansiati tanaman "+exception.Message);
        }
    }
}
