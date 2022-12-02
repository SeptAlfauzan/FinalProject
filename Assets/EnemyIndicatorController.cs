using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIndicatorController : MonoBehaviour
{
    public GameObject enemy;
    // Update is called once per frame
    void Update()
    {
        if(!enemy) return;
        var lookPos = enemy.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 40f);
    }
}
