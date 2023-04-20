using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeDoorInteract : MonoBehaviour, IInteractable
{
    public void Interact(Transform  interactorTransform){
        // SceneManager.LoadScene("Home");
    }

    public string GetInteractText(){
        return "To Home";
    }
    
    public Transform GetTransform(){
        return transform;
    }
    
    public void FacePlayer(Transform interactorTransform){}
    public void UpdatePlayerPrefs(string name, int value){}
    
}