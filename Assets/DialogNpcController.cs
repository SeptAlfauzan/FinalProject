using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogNpcController : MonoBehaviour
{
    public GameObject dialogContainer;
    public GameObject HUD;
    public Text dialogText;
    private bool isOnDialog = false;
    [SerializeField] GameObject closeDialogButton;
    [SerializeField] private AudioSource typeWrite;
    

    public float delay = 0.1f;
	private string currentText = "";
	private string dialogString;

	// Use this for initialization
	void OnEnable () {
		
	}
	
	IEnumerator ShowText(){
        typeWrite.Play();
		for(int i = 0; i < dialogString.Length; i++){
			currentText = dialogString.Substring(0,i);
			dialogText.text = currentText;
			yield return new WaitForSeconds(delay);
		}
        typeWrite.Stop();
	}

    private void Start() {
        closeDialogButton.GetComponent<Button>().onClick.AddListener(EndDialog);
    }
    public void EndDialog(){
        dialogContainer.SetActive(false);
        HUD.SetActive(true);
        isOnDialog = false;
        Camera.main.GetComponent<FollowPlayer>().SetIsZoom(false);
        typeWrite.Stop();
    }
    public void StartDialog(string text){
        HUD.SetActive(false);
        dialogString = text;
        dialogContainer.SetActive(true);
        isOnDialog = true;
        

        StartCoroutine(ShowText());
        Camera.main.GetComponent<FollowPlayer>().SetIsZoom(true);
    }
    public void Update(){
        if((Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetButtonDown("Interact")) && isOnDialog) EndDialog();
    }
}
