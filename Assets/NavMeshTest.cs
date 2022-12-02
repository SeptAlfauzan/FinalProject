using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTest : MonoBehaviour
{
   NavMeshAgent agent;
   [SerializeField] ParticleSystem dustParticle;
   public Transform target;
   RaycastHit raycastHit;
   private void Awake() {
        agent = GetComponent<NavMeshAgent>();
   }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.magnitude > 0) dustParticle.Play();
        agent.SetDestination(target.position);
    }
}
