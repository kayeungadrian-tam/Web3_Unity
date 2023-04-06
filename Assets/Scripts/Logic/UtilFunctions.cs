using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;

public class UtilFunctions : MonoBehaviour
{
    public static UtilFunctions Instance;

    [SerializeField] private TextMeshProUGUI mainMessageText;

    [SerializeField] private TextMeshProUGUI locationText;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetMessageText(){
        mainMessageText.text = "";
    }

    public void SetLocationText(string text){
        locationText.text = text;
    }


    public void ShowMessageOnCollision(Collider other, string tag, string message)
    {
        if (other.gameObject.CompareTag(tag))
        {
            mainMessageText.text = message;
        }
    }


}
