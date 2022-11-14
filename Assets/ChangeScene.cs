using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string moveToSceneName;
    private bool isOnArea = false;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") isOnArea = true;
    }
    private void OnTriggerStay(Collider other) {
        if(other.gameObject.tag == "Player") isOnArea = true;
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player") isOnArea = false;
    }
    private void Update() {
        if(isOnArea){
            if(Input.GetKey(KeyCode.E)) MoveScene(moveToSceneName); 
        }
    }
    private void MoveScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
}
