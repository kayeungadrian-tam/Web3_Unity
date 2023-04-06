using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetCamera : MonoBehaviour
{
    WebCamTexture webcam;
    public string path;
    public RawImage imgDisplay;

    // Start is called before the first frame update
    void Start()
    {
        webcam = new WebCamTexture();
        GetComponent<Renderer>().material.mainTexture = webcam;
        webcam.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
