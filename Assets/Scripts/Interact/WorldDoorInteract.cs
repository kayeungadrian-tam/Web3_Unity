using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldDoorInteract : MonoBehaviour, IInteractable
{
    public void Interact(Transform  interactorTransform){
        // SceneManager.LoadScene("World");
    }

    public string GetInteractText(){
        return "To world";
    }
    
    public Transform GetTransform(){
        return transform;
    }
    
    public void FacePlayer(Transform interactorTransform){}
    public void UpdatePlayerPrefs(string name, int value){}

}