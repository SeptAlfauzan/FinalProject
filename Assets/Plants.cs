using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plants : MonoBehaviour
{
    // Start is called before the first frame update
     private void Start(){
        GameObject player = GameObject.FindGameObjectWithTag("Player");     
        Debug.Log(player);
        Physics.IgnoreCollision(player.GetComponent<CapsuleCollider>(), GetComponent<BoxCollider>());
    }
}
