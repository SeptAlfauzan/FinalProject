using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject player;
    [SerializeField] Vector3 offset;
    [SerializeField] float smoothTime;
    [SerializeField] GameObject planeBoundary;

    private Vector3 _currentVelocity = Vector3.zero;
    private Dictionary<string, Vector3> planeBoundaryPos;

    private void Start() {
        if(planeBoundary) planeBoundaryPos = GetBoundaryPositions();
    }    

    // Update is called once per frame
    void LateUpdate(){
        Vector3 targetPosition = player.transform.position + offset;
        
        if(planeBoundary){
            targetPosition.z = Mathf.Clamp(targetPosition.z,  planeBoundaryPos["bottom right"].z, planeBoundaryPos["top right"].z);
            targetPosition.x = Mathf.Clamp(targetPosition.x, planeBoundaryPos["top left"].x, planeBoundaryPos["top right"].x);
        }
        
        Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, targetPosition, ref _currentVelocity, smoothTime);
    }

    private Dictionary<string, Vector3> GetBoundaryPositions(){
        Matrix4x4 localToWorld = planeBoundary.transform.localToWorldMatrix; //Create a Matrix in the planes LocalSpace
        Vector3 topLeftPos = localToWorld.MultiplyPoint3x4(new Vector3(-5, 0, 5)); //Get the Top Left Position in WorldSpace
        Vector3 topRightPos = localToWorld.MultiplyPoint3x4(new Vector3(5, 0, 5)); //Get the Top Right Position in WorldSpace
        Vector3 bottomLeftPos = localToWorld.MultiplyPoint3x4(new Vector3(-5, 0, -5)); //Get the Bottom Left Position in WorldSpace
        Vector3 bottomRightPos = localToWorld.MultiplyPoint3x4(new Vector3(5, 0, -5)); 

        return new Dictionary<string, Vector3>(){
            {"top left", topLeftPos},
            {"top right", topRightPos},
            {"bottom left", bottomLeftPos},
            {"bottom right", bottomRightPos},
        };
    }
}
