using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerWorldInterface : MonoBehaviour
{
    public bool toHome;

    // Start is called before the first frame update
    void Start()
    {
        toHome = false;
        UtilFunctions.Instance.SetLocationText("World");
    }

    // Update is called once per frame
    void Update()
    {
        // if (toHome && Input.GetKeyDown(KeyCode.F)) {SceneManager.LoadScene("Home");}   
    }

    private void OnTriggerEnter(Collider other){
        UtilFunctions.Instance.ShowMessageOnCollision(other, "HomeDoor", "To Home <F>");
        toHome = true;
    }
        
    private void OnTriggerExit(Collider other){
        UtilFunctions.Instance.ResetMessageText();

        toHome = false;
    }
}
