using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsMechanic : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Animator>().SetBool("IsPickUp", false);
        if(Input.GetKey(KeyCode.C)) UseTool();
        if(Input.GetKey(KeyCode.V)) UseTool();
        if(Input.GetKey(KeyCode.B)) UseTool();

        if(this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("PickUp")){
            this.GetComponent<Player>().canMove = false;
        } else{
            this.GetComponent<Player>().canMove = true;
        }
    }

    void Animate(){
        this.GetComponent<Animator>().SetBool("IsPickUp", true);
    }

    void UseTool(){
        Animate();
        
    }
}
