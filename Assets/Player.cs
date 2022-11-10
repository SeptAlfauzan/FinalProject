using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] bool isPickup = false;
    [SerializeField] bool isWalking = false;
    [SerializeField] float rotationSpeed;

    [SerializeField] Animator animator;
    float slowDown = 1;

    // Update is called once per frame
    private void Update() {
        isWalking = true;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        if(Input.GetKey(KeyCode.LeftShift)) isWalking = false;
        slowDown = isWalking?  0.3f : 1;
        Vector3 movement = new Vector3(horizontal, 0, vertical) * Time.deltaTime * movementSpeed * slowDown;

        this.transform.Translate(movement, Space.World);
        AnimateRunOrWalk(movement.magnitude, isWalking);

        Vector3 normalizeMovement = movement.normalized;
        RotateBasedOnDirection(normalizeMovement);

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
    }
}
