using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsMechanic : MonoBehaviour
{
    
    [SerializeField] private ParticleSystem hoeSlash;
    [SerializeField] private ParticleSystem sickleSlash;
    [SerializeField] private bool isUsingTools = false;
    [SerializeField] private GameObject shove;
    [SerializeField] private GameObject wateringCan;
    [SerializeField] private GameObject sickle;
    [SerializeField] private string lastUsedAnimation;
    [SerializeField] private TileSystem tileSystem;
    [SerializeField] private GameObject wateredLocation;
    [SerializeField] private SceneInfo sceneInfo;
    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Animator>().SetBool("IsSickle", false);
        this.GetComponent<Animator>().SetBool("IsWatering", false);
        this.GetComponent<Animator>().SetBool("IsDigging", false);

        if(Input.GetKey(KeyCode.C)) UseTool("hoe");
        if(Input.GetKey(KeyCode.V)) UseTool("water");
        if(Input.GetKey(KeyCode.B)) UseTool("sickle");

        if(this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("PickUp")){
            this.GetComponent<Player>().canMove = false;
        } else{
            this.GetComponent<Player>().canMove = true;
        }        

        isUsingTools = CheckIsAnimationStillPlaying(lastUsedAnimation);

        if(isUsingTools){
            if(lastUsedAnimation == "Sickle") sickle.SetActive(true);
            if(lastUsedAnimation == "watering") wateringCan.SetActive(true);
            if(lastUsedAnimation == "Digging") shove.SetActive(true);
            // ZOOM THE CAMERA
            Camera.main.GetComponent<FollowPlayer>().SetIsZoom(true);
        }else{
            Camera.main.GetComponent<FollowPlayer>().SetIsZoom(false);
            HideTools();
        }

        this.GetComponent<Player>().canMove = !isUsingTools;
    }

    void Animate(){
        this.GetComponent<Animator>().SetBool("IsPickUp", true);
    }
    void AnimateSikle(){
        HideTools();
        lastUsedAnimation = "Sickle";

        sickleSlash.Play();
        this.GetComponent<Animator>().SetBool("IsSickle", true);
    }
    void AnimateWatering(){
        HideTools();
        lastUsedAnimation = "watering";

        this.GetComponent<Animator>().SetBool("IsWatering", true);
    }
    void AnimateDigging(){
        HideTools();
        lastUsedAnimation = "Digging";
        
        hoeSlash.Play();
        this.GetComponent<Animator>().SetBool("IsDigging", true);
    }
    bool CheckIsAnimationStillPlaying(string name){
        return this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(name)? true : false;
    }
    void UseTool(string actionName){
        isUsingTools = true;
        
        if(actionName == "sickle"){
            DecreaseStamina(6);
            AnimateSikle();
        } 
        if(actionName == "hoe"){
            DecreaseStamina(6);
            AnimateDigging();
        }
        if(actionName == "water"){
            DecreaseStamina(3);
            AnimateWatering();
        }
    }
    void HideTools(){
        wateringCan.SetActive(false);
        shove.SetActive(false);
        sickle.SetActive(false);
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Plant") {
            if(isUsingTools && lastUsedAnimation == "Sickle"){
                //decrease stamina
                // emit particle
                // destroy plant game object
                Destroy(other.gameObject);
            }
            if(isUsingTools && lastUsedAnimation == "watering"){
                //decrease stamina
                // emit particle
                other.gameObject.GetComponent<Plants>().Watered();
                // check if plant position same as watered location
                // if(!tileSystem) return;
                // if(other.transform.position == GetWateredPlantLocation()){
                //     //watered that plant
                // }

            }
        }
    }
    private void OnTriggerStay(Collider other) {
        if(other.gameObject.tag == "Plant") {
            if(isUsingTools && lastUsedAnimation == "Sickle"){
                // emit particle
                // destroy plant game object
                Destroy(other.gameObject);
            }
            if(isUsingTools && lastUsedAnimation == "watering"){
                // emit particle
                other.gameObject.GetComponent<Plants>().Watered();
                // check if plant position same as watered location
                // if(!tileSystem) return;
                // if(other.transform.position == GetWateredPlantLocation()){
                //     //watered that plant
                // }

            }
        }
    }
    private Vector3 GetWateredPlantLocation(){
        return tileSystem.SnapObjCoordinateToGrid(wateredLocation.transform.position);
    }
    private void DecreaseStamina(float staminaUsed){
        if(sceneInfo.playerStamina > 0){
            float currentStamina = sceneInfo.playerStamina * 100;
            currentStamina -= staminaUsed;
            sceneInfo.playerStamina = currentStamina / 100;
        }
    }
}
