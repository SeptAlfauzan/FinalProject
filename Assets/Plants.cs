using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plants : MonoBehaviour
{
    public float plantAge = 0f;
    public float maxHarvestTime = 70f;
    [SerializeField] private float harvestTime = 0f;
    public float growTime = 12f;
    public float maxSize = 2f;

    public bool isMaxSize = false;
    public bool isMaxHarvestTime = false;
    public bool hasFruit = false;
    public PlantItemData plantItemData;
    public GameObject fruitSpawnPoint;


    // Start is called before the first frame update
     private void Start(){
        if(isMaxSize == false) StartCoroutine(Grow());


        GameObject player = GameObject.FindGameObjectWithTag("Player");     
        Debug.Log(player);
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
        if(hasFruit) harvestTime = 0f;
        do{
            harvestTime += Time.deltaTime;
            yield return new WaitForSeconds(10);
        }
        while(harvestTime < maxHarvestTime);
        isMaxHarvestTime = true;
    }

    private void Update() {
        hasFruit = CheckItHasFruit();
        if(isMaxHarvestTime == false) StartCoroutine(GrowFruit());
        

        if(!isMaxSize) return;
        if(!isMaxHarvestTime) return;
        if(hasFruit){//keep reset harvest time when plant has fruit
            isMaxHarvestTime = false;
            return;
        } 
        
        GameObject newFruit = Instantiate(plantItemData.fruitPrefab, fruitSpawnPoint.transform);
        hasFruit = true;
    }

    private bool CheckItHasFruit(){
        return fruitSpawnPoint.transform.childCount > 0? true : false;
    }
}
