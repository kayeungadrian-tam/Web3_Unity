using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerInteract : MonoBehaviour, IInteractable
{



    private float delay = 5f;    

    void Start()
    {
        Destroy(gameObject, delay);
    }


    private void PickupFlower(){
        int flowerCount = PlayerPrefs.GetInt("flower");
        // Debug.Log(flowerCount);
        flowerCount++;
        PlayerPrefs.SetInt("flower", flowerCount);
        Destroy(gameObject);
    }

    public void Interact(Transform transform){
        PickupFlower();
    }

    public Transform GetTransform(){
        return transform;
    }

    public void FacePlayer(Transform transform){
        
    }

    public string GetInteractText(){
        return "Pick up";
    }

    public void UpdatePlayerPrefs(string name, int value){
        PlayerPrefs.SetInt(name, value);
    }
}
