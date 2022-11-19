using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlantedPlant{
    private Vector3 location;
    private GameObject plant;
    private float plantAge;
    public PlantedPlant(Vector3 location, GameObject plant, float plantAge){
        this.location = location;
        this.plant = plant;
        this.plantAge = plantAge;
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
    public PlantedPlant Clone(){
        return new PlantedPlant(this.location, this.plant, this.plantAge);
    }
}