using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemInteractUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI flowerTextGUI;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        string flowerString = PlayerPrefs.GetInt("flower").ToString("000");
        flowerTextGUI.text = flowerString;

    }
}
