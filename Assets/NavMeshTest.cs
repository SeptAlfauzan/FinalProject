using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTest : MonoBehaviour
{
   NavMeshAgent agent;
   [SerializeField] ParticleSystem dustParticle;
   [SerializeField] ParticleSystem smokeParticle;
   [Header("SFX")]
   [SerializeField] AudioSource dieSFX;

   public Transform target;
   RaycastHit raycastHit;
   private void Awake() {
        agent = GetComponent<NavMeshAgent>();
   }

    // Update is called once per frame
    void Update(){   
        GameObject[] plants = GameObject.FindGameObjectsWithTag("Plant");
        if(plants.Length > 0) target = GetClosestPlant(plants);

        if(!target) target = GameObject.FindGameObjectWithTag("Player").transform;
        if(transform.position.magnitude > 0) dustParticle.Play();
        agent.SetDestination(target.position);
    }

    private void OnDestroy() {
        // dieSFX.Play();
        ParticleSystem smoke = Instantiate(smokeParticle, this.transform.position + Vector3.zero, Quaternion.Euler(Vector3.zero));
        AudioSource dieSfx = Instantiate(dieSFX, this.transform.position + Vector3.zero, Quaternion.Euler(Vector3.zero));

        smoke.Play();
        dieSfx.Play();
        Debug.Log(dieSfx);
        Destroy(smoke.gameObject, 3f);//destroy particle when finished played
        Destroy(dieSfx.gameObject, 1f);//destroy audio when finished played
    }

    private Transform GetClosestPlant (GameObject[] plants){
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach(GameObject potentialTarget in plants)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
            }
        }
        return bestTarget;
    }
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Plant"){
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerStay(Collider other) {
        if(other.gameObject.tag == "Plant"){
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
