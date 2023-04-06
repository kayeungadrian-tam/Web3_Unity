using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerInterface : MonoBehaviour
{

    private int flowerCount;

    void Start(){
        // flowerCount = PlayerPrefs.GetInt("flower");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)){
            IInteractable interactable = GetInteractableObject();
            if(interactable != null){
                interactable.FacePlayer(transform);
                interactable.Interact(transform);
            }
        }
    }

    
    public IInteractable GetInteractableObject(){
        List<IInteractable> interactableList = new List<IInteractable>();
        float interactRange = 2f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray)
        {
            if(collider.TryGetComponent(out IInteractable IInteractable)){
                return IInteractable;
            }
        }

        IInteractable cloesestInteractable = null;
        foreach(IInteractable interactable in interactableList){
            if(cloesestInteractable == null){
                cloesestInteractable = interactable;
            } else {
                if(Vector3.Distance(transform.position, interactable.GetTransform().position) < 
                    Vector3.Distance(transform.position, cloesestInteractable.GetTransform().position))
                    {
                        cloesestInteractable = interactable;
                    }
            }
        }
        return cloesestInteractable;
    }

}






