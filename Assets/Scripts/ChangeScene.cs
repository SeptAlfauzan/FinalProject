using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string moveToSceneName;
    GameObject inventoryObj;
    Inventory inventory;
    private bool isOnArea = false;

    [SerializeField] private GameObject LoadingCanvas;
    [SerializeField] private Image progressBar;
    private GameObject[] plants;
    private List<Plants> plantsBehaviour;
    public PlantLocationData plantLocationData;

    private void Start() {
        
        InstantiateSavedPlant();

        inventoryObj = GameObject.FindGameObjectWithTag("Inventory");
        inventory = inventoryObj.GetComponent<Inventory>();

        plants = GameObject.FindGameObjectsWithTag("Plant");
        Debug.Log(plants.Length);
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") isOnArea = true;
    }
    private void OnTriggerStay(Collider other) {
        if(other.gameObject.tag == "Player") isOnArea = true;
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player") isOnArea = false;
    }
    private void Update() {
        if(isOnArea){
            if(Input.GetKey(KeyCode.E)) MoveScene(moveToSceneName); 
        }
    }
    private void MoveScene(string sceneName){
        SaveCurrentData();
        SceneManager.LoadScene(sceneName);
    }

    private void SaveCurrentData(){
        // string sceneName =  SceneManager.GetActiveScene().name;

        // inventory.StoreToSceneInfo(sceneName);
       if(SceneManager.GetActiveScene().name == "Farm") SavePlantState();
    }

    private void SavePlantState(){
        int index = 0;
        plantLocationData.Reset();//to avoid redundant plant

        foreach (GameObject plantedPlant in plants){
            Plants plantData = plantedPlant.GetComponent<Plants>();
            plantLocationData.AddPlantedLocation(new PlantedPlant(plants[index].transform.position, plantedPlant.GetComponent<Plants>().plantItemData.prefabData, plantData.plantAge));
            index++;
        }
    }

    private void InstantiateSavedPlant(){
        try{
            List<PlantedPlant> plantedPlants = plantLocationData.GetAllPlantedPlant();
            if(plantedPlants == null) return;

            foreach (PlantedPlant plantedPlant in plantedPlants){
                GameObject newPlant = Instantiate(plantedPlant.GetPlant());
                newPlant.transform.position = plantedPlant.GetLocation();
                newPlant.GetComponent<Plants>().plantAge = plantedPlant.GetPlantAge();
            }
        }
        catch (System.Exception exception)
        {
            Debug.Log("error instansiati tanaman "+exception.Message);
        }
    }
}
