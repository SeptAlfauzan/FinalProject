using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Plant Location Data")]

public class PlantLocationData : ScriptableObject
{

    [SerializeField] private List<PlantedPlant> plantedPlants = new List<PlantedPlant>();
    public void Reset(){
        this.plantedPlants.Clear();
    }
    public void SetPlantedLocation(List<PlantedPlant> plantedPlants){
        this.plantedPlants = plantedPlants;
    }
    public void AddPlantedLocation(PlantedPlant plantedPlant){
        plantedPlants.Add(plantedPlant);
    }
    public PlantedPlant GetPlantedPlant(int index){
        return plantedPlants[index];
    }
    public List<PlantedPlant> GetAllPlantedPlant(){
        return plantedPlants;
    }
}
