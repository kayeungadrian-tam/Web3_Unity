using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Cinemachine;

public class NPCScript : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;
    [Header("Dialogue")]
    public string name;
    public string dialogue;
    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI nameText;
    
    [Header("Camera")]
    [SerializeField] CinemachineTargetGroup targetGroup;
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject dialogueCamera;
    

    private void FaceObject(Transform targetTransform)
    {
        var dir = targetTransform.position - transform.position;
        dir.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(dir);
        transform.DORotateQuaternion(targetRotation, 0.5f);
        // transform.DORotate()
    }


    private void Update(){
        if (Input.GetKeyDown(KeyCode.Q)){
            dialogueUI.SetActive(false);
            ChangeCamera(false);
        }
    }

    private void ChangeCamera(bool trigger){
        mainCamera.SetActive(!trigger);
        dialogueCamera.SetActive(trigger);
    }


    public void Interact(Transform interactorTransform){
        // facePlayer = true;
        targetGroup.m_Targets[1].target = transform.transform;

        ChangeCamera(true);

        dialogueUI.SetActive(true);
        dialogueText.text = dialogue;
        nameText.text = name;
        // Debug.Log($"Interacting... [{gameObject.name}]");
    }

    public string GetInteractText(){
        return interactText;
    }

    public void FacePlayer(Transform interactorTransform){
        FaceObject(interactorTransform);
    }

    public Transform GetTransform(){
        return transform;
    }

    public void UpdatePlayerPrefs(string name, int value){
        // PlayerPrefs.SetInt(name, value);
    }
}
