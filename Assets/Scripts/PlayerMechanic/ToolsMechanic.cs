using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsMechanic : MonoBehaviour
{
    
    [SerializeField] private ParticleSystem swordSlash;
    [SerializeField] private ParticleSystem sickleSlash;
    [SerializeField] private ParticleSystem waterSplash;
    [SerializeField] private bool isUsingTools = false;
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject wateringCan;
    [SerializeField] private GameObject sickle;
    [SerializeField] private string lastUsedAnimation;
    [SerializeField] private TileSystem tileSystem;
    [SerializeField] private GameObject wateredLocation;
    [SerializeField] private SceneInfo sceneInfo;
    [Header("SFX")]
    [SerializeField] private AudioSource swingSFX;
    [SerializeField] private AudioSource impactSFX;
    [SerializeField] private AudioSource wateringSFX;
    [Header("Enemy Properties")]
    [SerializeField] private ParticleSystem smokeEffect;
    [SerializeField] private ParticleSystem coinEffect;
    [SerializeField] private AudioSource enemyDieSFX;
    [SerializeField] private AudioSource coinSFX;
    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Animator>().SetBool("IsSickle", false);
        this.GetComponent<Animator>().SetBool("IsWatering", false);
        this.GetComponent<Animator>().SetBool("IsSword", false);

        if(Input.GetButtonDown("Using Tool 1")) UseTool("sword");
        if(Input.GetButtonDown("Using Tool 2")) UseTool("water");
        if(Input.GetButtonDown("Using Tool 3")) UseTool("sickle");

        if(this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("PickUp")){
            this.GetComponent<Player>().canMove = false;
        } else{
            this.GetComponent<Player>().canMove = true;
        }        

        isUsingTools = CheckIsAnimationStillPlaying(lastUsedAnimation);
// sword.SetActive(true);
        if(isUsingTools){
            if(lastUsedAnimation == "Sickle") sickle.SetActive(true);
            if(lastUsedAnimation == "watering") wateringCan.SetActive(true);
            if(lastUsedAnimation == "Sword") sword.SetActive(true);
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
        if(!swingSFX.isPlaying) swingSFX.Play();
        this.GetComponent<Animator>().SetBool("IsSickle", true);
    }
    void AnimateWatering(){
        HideTools();
        lastUsedAnimation = "watering";
        waterSplash.Play();
        if(!wateringSFX.isPlaying) wateringSFX.Play();
        this.GetComponent<Animator>().SetBool("IsWatering", true);
    }
    void AnimateSword(){
        HideTools();
        lastUsedAnimation = "Sword";
        
        sickleSlash.Play();
        if(!swingSFX.isPlaying) swingSFX.Play();
        this.GetComponent<Animator>().SetBool("IsSword", true);
    }
    bool CheckIsAnimationStillPlaying(string name){
        return this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(name)? true : false;
    }
    void UseTool(string actionName){
        isUsingTools = true;
        // "Sickle"
        // "watering"
        // "Digging"
        if(actionName == "sickle"){
            if(!CheckIsAnimationStillPlaying("Sickle")) DecreaseStamina(6);
            AnimateSikle();
        } 
        if(actionName == "sword"){
            if(!CheckIsAnimationStillPlaying("Sword")) DecreaseStamina(0);
            AnimateSword();
        }
        if(actionName == "water"){
            if(!CheckIsAnimationStillPlaying("watering")) DecreaseStamina(3);
            AnimateWatering();
        }
    }
    void HideTools(){
        wateringCan.SetActive(false);
        sword.SetActive(false);
        sickle.SetActive(false);
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Plant") {
            if(isUsingTools && lastUsedAnimation == "Sickle"){
                Destroy(other.gameObject);
            }
            if(isUsingTools && lastUsedAnimation == "watering"){
                //decrease stamina
                // emit particle
                other.gameObject.GetComponent<Plants>().Watered();
            }
        }
        if(other.gameObject.tag == "Enemies"){
            if(isUsingTools && lastUsedAnimation == "Sword"){
                KillEnemy(other.gameObject);
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
        if(other.gameObject.tag == "Enemies"){
            if(isUsingTools && lastUsedAnimation == "Sword"){
                KillEnemy(other.gameObject);
            }
        }
    }
    private Vector3 GetWateredPlantLocation(){
        return tileSystem.SnapObjCoordinateToGrid(wateredLocation.transform.position);
    }
    private void DecreaseStamina(float staminaUsed){
        if(sceneInfo.playerStamina > 0){
            float currentStamina = sceneInfo.playerStamina;
            currentStamina -= staminaUsed;
            sceneInfo.playerStamina = currentStamina;
        }
    }

    private void KillEnemy(GameObject enemy){
        Vector3 copyPosition = enemy.transform.localPosition + Vector3.zero;
        Quaternion copyRotation = Quaternion.Euler(Vector3.zero) * enemy.transform.localRotation;

        ParticleSystem smokeParticle = Instantiate(smokeEffect, copyPosition, copyRotation);
        ParticleSystem coinParticle = Instantiate(coinEffect, copyPosition, copyRotation);
        smokeParticle.Play();
        coinParticle.Play();

        if(!enemyDieSFX.isPlaying) enemyDieSFX.Play();
        if(!coinSFX.isPlaying) coinSFX.Play();

        sceneInfo.money += 10;//gain 10 gold when kill enemy
        
        Destroy(smokeParticle.gameObject, 1f);
        Destroy(coinParticle.gameObject, 1f);
        // play sfx
        Destroy(enemy.gameObject);
    }
}
