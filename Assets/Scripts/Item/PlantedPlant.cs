using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlantedPlant{
    private Vector3 location;
    private GameObject plant;
    private float plantAge;
    private float harvestAge;
    private bool isWatered;
<<<<<<< HEAD
    private int lastDayWatered;
    public PlantedPlant(Vector3 location, GameObject plant, float plantAge, float harvestAge, bool isWatered, int lastDayWatered){
=======
    public PlantedPlant(Vector3 location, GameObject plant, float plantAge, float harvestAge, bool isWatered){
>>>>>>> ae88d656ec9449420c1523cf5d5885d0255ce041
        this.location = location;
        this.plant = plant;
        this.plantAge = plantAge;
        this.harvestAge = harvestAge;
        this.isWatered = isWatered;
<<<<<<< HEAD
        this.lastDayWatered = lastDayWatered;
=======
>>>>>>> ae88d656ec9449420c1523cf5d5885d0255ce041
    }
    public Vector3 GetLocation(){
        return location;
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
<<<<<<< HEAD
    public int GetLastDayWatered(){
        return lastDayWatered;
    }
    public PlantedPlant Clone(){
        return new PlantedPlant(this.location, this.plant, this.plantAge, this.harvestAge, this.isWatered, this.lastDayWatered);
=======
    public PlantedPlant Clone(){
        return new PlantedPlant(this.location, this.plant, this.plantAge, this.harvestAge, this.isWatered);
>>>>>>> ae88d656ec9449420c1523cf5d5885d0255ce041
    }
}