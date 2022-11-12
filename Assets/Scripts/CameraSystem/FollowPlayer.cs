using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject player;
    [SerializeField] Vector3 offset;
    [SerializeField] float smoothTime;
    private Vector3 _currentVelocity = Vector3.zero;

    // Update is called once per frame
    void LateUpdate(){
        Vector3 targetPosition = player.transform.position + offset;
        Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, targetPosition, ref _currentVelocity, smoothTime);
    }
}
