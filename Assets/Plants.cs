using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plants : MonoBehaviour
{
    public float plantAge = 0f;
    public float growTime = 12f;
    public float maxSize = 2f;
    public float harvestTime = 0f;
    public float tempHarvestTime = 0f;//only used to track current plant harvest time without losing the value when plant has fruit
    public float maxHarvestTime = 70f;
    public bool isMaxSize = false;
    public bool isMaxHarvestTime = false;
    public bool hasFruit = false;
    public PlantItemData plantItemData;
    public GameObject fruitSpawnPoint;

    private bool startNewFruitCycle = true;

    // Start is called before the first frame update
     private void Start(){
        if(isMaxSize == false) StartCoroutine(Grow());
        if(isMaxHarvestTime == false && !hasFruit) StartCoroutine(GrowFruit());


        GameObject player = GameObject.FindGameObjectWithTag("Player");     
        Physics.IgnoreCollision(player.GetComponent<CapsuleCollider>(), GetComponent<BoxCollider>());
    }

    private IEnumerator Grow(){
        Vector3 startScale = transform.localScale;
        Vector3 maxScale = new Vector3(maxSize, maxSize,maxSize);

        do{
            transform.localScale = Vector3.Lerp(startScale, maxScale, plantAge/growTime);
            plantAge += Time.deltaTime;
            yield return null;
        }
        while(plantAge < growTime);

        isMaxSize = true;
    }
    private IEnumerator GrowFruit(){ 
        // if(startNewFruitCycle){
            
        //     isMaxHarvestTime = false;

        //     startNewFruitCycle = false;
        // }

        do{
            harvestTime += Time.deltaTime;
            tempHarvestTime += Time.deltaTime;
            yield return null;
        }
        while(harvestTime < maxHarvestTime);
        isMaxHarvestTime = true;
    }

    public void ReGrowFruit(){
        Debug.Log("Regrow");
        isMaxHarvestTime = false;
        harvestTime = 0;
        StartCoroutine(GrowFruit());
    }

    private void Update() {
        hasFruit = CheckItHasFruit();
        // startNewFruitCycle = hasFruit? true : false;
        // harvestTime = hasFruit? 0 : harvestTime;

        // if(isMaxHarvestTime == false) StartCoroutine(GrowFruit());

        if(!isMaxSize) return;
        if(!isMaxHarvestTime) return;
        if(hasFruit) return;

        GameObject newFruit = Instantiate(plantItemData.fruitPrefab, fruitSpawnPoint.transform);
        hasFruit = true;
    }

    private bool CheckItHasFruit(){
        return fruitSpawnPoint.transform.childCount > 0? true : false;
    }
}
