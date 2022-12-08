using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    public ParticleSystem dust;

    [SerializeField] float movementSpeed;
    // [SerializeField] bool isPickup = false;
    [SerializeField] bool isWalking = false;
    [SerializeField] float rotationSpeed;

    [SerializeField] Animator animator;
    float slowDown = 1;
    [SerializeField] public GameObject collectibleItem = null;
    [SerializeField] private Dictionary<string, CollectibleItem> itemsInBag = new Dictionary<string, CollectibleItem>();

    [Header("Particle System Object ")]
    [SerializeField] ParticleSystem coinParticle;
    [SerializeField] ParticleSystem rainParticle;
    // SCRIPTABLE OBJ
    [Header("Scriptable Object")]
    [SerializeField] SceneInfo sceneInfo;
    [SerializeField] QuestData questListData;
    // AUDIO
    [Header("Audio SFX")]
    [SerializeField] AudioSource grassFootStep;
    [SerializeField] AudioSource footStep;
    [SerializeField] AudioSource runFootStep;
    [SerializeField] AudioSource damagedSfx;
    public bool canMove = true;
    private Inventory inventory;

    [Header("Enemy Indicator")]
    [SerializeField] GameObject enemyIndicatorPrefab;
    [SerializeField] GameObject enemyIndicatorContainer;
    [SerializeField] List<GameObject> instantiatedEnemyIndicators;
    [Header("Game Over")]
    [SerializeReference] PauseMenu pauseMenu;

    [Header("Behaviours")]
    [SerializeField] ExhaustedUIController exhaustedUIController;

    public bool onArea = false;
    public GameObject interactIcon;
    private void Start() {
        interactIcon.SetActive(false);
        inventory =  GameObject.FindGameObjectWithTag("Inventory")? GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>() : null;
    }
    // Update is called once per frame
    private void Update() {
        interactIcon.SetActive(onArea? true : false);
        
        if(sceneInfo.lifePoint <= 0) pauseMenu.isGameOver = true;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemies");
        ShowEnemyIndicator(enemies);

        isWalking = true;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        if(Input.GetButton("Toggle Run") && sceneInfo.playerStamina > 15f) isWalking = false;
        slowDown = isWalking?  0.3f : 1;
        
        Vector3 movement = new Vector3(horizontal, 0, vertical) * Time.deltaTime * movementSpeed * slowDown;
// running / walking stuff goes here
        if(canMove) this.transform.Translate(movement, Space.World);
        AnimateRunOrWalk(movement.magnitude, isWalking);
        if(movement.magnitude > 0){
            if(isWalking) PlaySFXFootStep(footStep);
            else  PlaySFXFootStep(runFootStep);

            CreateDust();
        }
//end section
        Vector3 normalizeMovement = movement.normalized;
        RotateBasedOnDirection(normalizeMovement);
//control mechanic
        if(Input.GetButton("Interact")) PickUpItem();
        inventory.InventoryControl();
// when player stamina is empty
        if(sceneInfo.playerStamina * 100 <= 0 || Mathf.Floor(sceneInfo.dayTime) == 2) Exhausted();
    }
    void RotateBasedOnDirection(Vector3 movementDirection){
        if(movementDirection != Vector3.zero){
            // transform.forward = movementDirection;
            Quaternion rotationTarget = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, rotationSpeed * Time.deltaTime);
        }
    }
    void AnimateRunOrWalk(float magnitude, bool isWalking){
        animator.SetBool("IsWalking", isWalking);
        animator.SetFloat("Speed", magnitude * 100);
        // CreateDust();
    }
    private void OnTriggerStay(Collider other) {
        string tag = other.gameObject.tag;
        if( tag == "Collectible"|| tag == "Collectible Seed" ||tag == "Collectible Fruit") collectibleItem = other.gameObject;
    }
    private void OnTriggerEnter(Collider other) {
        string tag = other.gameObject.tag;
        if( tag == "Collectible"|| tag == "Collectible Seed" || tag == "Collectible Fruit") collectibleItem = other.gameObject;
    }
    private void OnTriggerExit(Collider other) {    
        string tag = other.gameObject.tag;
        if( tag == "Collectible"|| tag == "Collectible Seed" || tag == "Collectible Fruit") collectibleItem = null;
    }
    // interaction
    public void PickUpItem(){
        try{
            if(collectibleItem){
                Collectible collectible = collectibleItem.GetComponent<Collectible>();

                string name = collectibleItem.GetComponent<Collectible>().GetName();

                if(collectible.tag == "Collectible Fruit"){
                    coinParticle.Play();
                    sceneInfo.money += collectible.itemData.prize;
                    if(collectible.gameObject.transform.parent) collectible.gameObject.transform.parent.GetComponentInParent<Plants>().ReGrowFruit();
                    // HERE QUEST SYSTEM WILL RUN
                    int questIndex = CheckItemOnQuestGetIndexQuest(collectible.itemData);
                    if(questIndex != -1){
                        
                        if(questListData.quests[questIndex].notCompleted || questListData.quests[questIndex].completed) return;//stop script when quest is failed or completed

                        questListData.quests[questIndex].amountGiven += 1;
                        questListData.quests[questIndex].completed = IsQuestFinished(questIndex);
                    }
                }else{
                    inventory.Add(name, collectible);
                }

                Destroy(collectibleItem);
                collectibleItem = null;


            }
        }
        catch (System.Exception e)
        {   
            // throw;
            Debug.Log(e);
        }
    }
    int CheckItemOnQuestGetIndexQuest(ItemData item){//if given item is not on quest, this will return -1
        int index = 0;
        foreach (Quest quest in questListData.quests){
            if(item == quest.itemData) return index;
            index ++;
        }
        return -1;
    }
    bool IsQuestFinished(int questIndex){
        if(questListData.quests[questIndex].amountGiven == questListData.quests[questIndex].amountNeed) return true;
        return false;
    }
    // PARTICLE SYSTEM
    void CreateDust(){
        dust.Play();
    }
    // SFX GOES HERE
    void PlaySFXFootStep(AudioSource audioStep){
        if(!audioStep.isPlaying) audioStep.Play();
    }

    void Exhausted(){
        Time.timeScale = 0;
        exhaustedUIController.gameObject.SetActive(true);
    }
    void ShowEnemyIndicator(GameObject[] enemies){
        if(enemies.Length == 0) ClearEnemyIndicator();

        if(GameObject.FindGameObjectsWithTag("Enemy Indicator").Length != instantiatedEnemyIndicators.Count) ClearEnemyIndicator();
        
        if(enemies.Length != instantiatedEnemyIndicators.Count){
            
            ClearEnemyIndicator();
            foreach (var enemy in enemies){
                GameObject newIndicator = Instantiate(enemyIndicatorPrefab, enemyIndicatorContainer.transform);
                newIndicator.GetComponent<EnemyIndicatorController>().enemy = enemy;
                instantiatedEnemyIndicators.Add(newIndicator);
            }
        }
    }

    void ClearEnemyIndicator(){
        foreach (var indicator in instantiatedEnemyIndicators){
            Destroy(indicator);
        }
        instantiatedEnemyIndicators.Clear();
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Enemies"){
            // if(damagedSfx.isPlaying  ) damagedSfx.Stop();
            damagedSfx.Play();
            sceneInfo.playerStamina -= 4;
            

            Vector3 moveDirection = this.transform.position - other.transform.position;
            this.GetComponent<Rigidbody>().AddForce( moveDirection.normalized * -1000f);
        }
    }
}
