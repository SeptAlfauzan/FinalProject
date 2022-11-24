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
    [SerializeField] private TextMesh enterText;
    [SerializeField] private string enterTextString;
    private GameObject[] plants;
    private List<Plants> plantsBehaviour;
    public PlantLocationData plantLocationData;

    private void Start() {
        enterText.text = enterTextString;
        if(SceneManager.GetActiveScene().name == "Farm") InstantiateSavedPlant();

        inventoryObj = GameObject.FindGameObjectWithTag("Inventory");
        inventory = inventoryObj.GetComponent<Inventory>();

        plants = GameObject.FindGameObjectsWithTag("Plant");
    }
    private void Update() {
        if(plants.Length != GameObject.FindGameObjectsWithTag("Plant").Length) plants = GameObject.FindGameObjectsWithTag("Plant");
        if(isOnArea){
            enterText.gameObject.SetActive(true);
            if(Input.GetKey(KeyCode.E)) MoveScene(moveToSceneName); 
        }else{
            enterText.gameObject.SetActive(false);
        }
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

        List<PlantedPlant> tempPlantedPlants = new List<PlantedPlant>();
        foreach (GameObject plantedPlant in plants){
            Plants plantData = plantedPlant.GetComponent<Plants>();

            tempPlantedPlants.Add(
                new PlantedPlant(plants[index].transform.position,
                plantedPlant.GetComponent<Plants>().plantItemData.prefabData,
                plantData.plantAge,
                plantData.harvestTime,
                plantData.isWatered,
                plantData.lastDayWatered
                ));
            index++;
        }
        plantLocationData.SetPlantedLocation(tempPlantedPlants);
    }

    private void InstantiateSavedPlant(){
        try{
            List<PlantedPlant> plantedPlants = plantLocationData.GetAllPlantedPlant();
            if(plantedPlants == null) return;

            foreach (PlantedPlant plantedPlant in plantedPlants){
                GameObject newPlant = Instantiate(plantedPlant.GetPlant());

                newPlant.transform.position = plantedPlant.GetLocation();
                newPlant.GetComponent<Plants>().plantAge = plantedPlant.GetPlantAge();
                newPlant.GetComponent<Plants>().harvestTime = plantedPlant.GetHarvestAge();
                newPlant.GetComponent<Plants>().isWatered = plantedPlant.GetWateredStatus();
                newPlant.GetComponent<Plants>().lastDayWatered = plantedPlant.GetLastDayWatered();

            }
        }
        catch (System.Exception exception)
        {
            Debug.Log("error instansiati tanaman "+exception.Message);
        }
    }
}
