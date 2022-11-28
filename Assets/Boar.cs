using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform target;
    [SerializeField] private float speed = 2;
    // Update is called once per frame
    void Update()
    {
        if(target == null){
            GameObject[] plants = GameObject.FindGameObjectsWithTag("Plant");
            target = GetClosestPlant(plants);
        }
        MoveToTarget(target);
    }
    private Transform GetClosestPlant (GameObject[] plants)
    {
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

    private void MoveToTarget(Transform target){
        if(target) transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Plant") Destroy(other.gameObject);
    }
}
