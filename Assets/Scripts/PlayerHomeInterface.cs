using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHomeInterface : MonoBehaviour
{


    public bool toWorld = false;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        UtilFunctions.Instance.SetLocationText("Home");
    }

    // Update is called once per frame
    void Update()
    {
        if (toWorld && Input.GetKeyDown(KeyCode.F)) { 
            // SceneManager.LoadScene("World");
        }
    }


    private void OnTriggerEnter(Collider other){
        UtilFunctions.Instance.ShowMessageOnCollision(other, "HomeDoor", "To World <F>");

        toWorld = true;
    }
        
    private void OnTriggerExit(Collider other){
        UtilFunctions.Instance.ResetMessageText();

        toWorld = false;
    }

}
