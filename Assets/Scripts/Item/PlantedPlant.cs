using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct PlantedPlant{
    private Vector3 location;
    private Vector3 plantSize;
    private GameObject plant;
    private float plantAge;
    private float harvestAge;
    private bool isWatered;
    private int lastDayWatered;
    public PlantedPlant(Vector3 location, Vector3 plantSize, GameObject plant, float plantAge, float harvestAge, bool isWatered, int lastDayWatered){
        this.location = location;
        this.plantSize = plantSize;
        this.plant = plant;
        this.plantAge = plantAge;
        this.harvestAge = harvestAge;
        this.isWatered = isWatered;
        this.lastDayWatered = lastDayWatered;
    }
    public Vector3 GetLocation(){
        return location;
    }
    public Vector3 GetSize(){
        return plantSize;
    }
    public GameObject GetPlant(){
        return plant;
    }
    public float GetPlantAge(){
        return plantAge;
    }
    public float GetHarvestAge(){
        return harvestAge;
    }
    public bool GetWateredStatus(){
        return isWatered;
    }
    public int GetLastDayWatered(){
        return lastDayWatered;
    }
    public PlantedPlant Clone(){
        return new PlantedPlant(this.location, this.plantSize ,this.plant, this.plantAge, this.harvestAge, this.isWatered, this.lastDayWatered);
    }
}