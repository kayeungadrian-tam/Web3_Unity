using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField] private GameObject InteractUI;
    [SerializeField] private PlayerInterface playerInterface;
    [SerializeField] private TextMeshProUGUI interactTextGUI;


    private void Update(){
        if(playerInterface.GetInteractableObject() != null){
            Show(playerInterface.GetInteractableObject());
        } else {
            Hide();
        }
    }

    private void Show(IInteractable interactable){
        InteractUI.SetActive(true);
        interactTextGUI.text = interactable.GetInteractText();
    }

    private void Hide(){
        InteractUI.SetActive(false);
    }
}
