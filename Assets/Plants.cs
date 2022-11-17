using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plants : MonoBehaviour
{
    public float timer = 0f;
    public float growTime = 12f;
    public float maxSize = 2f;

    public bool isMaxSize = false;

    // Start is called before the first frame update
     private void Start(){
        if(isMaxSize == false){
            StartCoroutine(Grow());
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");     
        Debug.Log(player);
        Physics.IgnoreCollision(player.GetComponent<CapsuleCollider>(), GetComponent<BoxCollider>());
    }

    private IEnumerator Grow(){
        Vector3 startScale = transform.localScale;
        Vector3 maxScale = new Vector3(maxSize, maxSize,maxSize);

        do{
            transform.localScale = Vector3.Lerp(startScale, maxScale, timer/growTime);
            timer += Time.deltaTime;
            yield return null;
        }
        while(timer < growTime);

        isMaxSize = true;
    }
}
