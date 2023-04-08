using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceObjectUI : MonoBehaviour
{

    private Color originalColor;

    void Start()
    {
        originalColor = transform.GetChild(0).gameObject.GetComponent<Image>().color;
    }

    private void ResetColor(int expectionIndex){
        for (int i = 0; i < 3; i++){
            if (i != expectionIndex){
                GameObject panel = transform.GetChild(i).gameObject;
                panel.GetComponent<Image>().color = originalColor;
            }
        }
    }

    private void SetColor(int index){
        GameObject panel = transform.GetChild(index).gameObject;
        panel.GetComponent<Image>().color = Color.grey;
    }



    

    // Update is called once per frame
    void Update()
    {




        if (Input.GetKeyDown(KeyCode.Alpha1)){  
            SetColor(0);
            ResetColor(0);
        }


        if (Input.GetKeyDown(KeyCode.Alpha2)){  
            SetColor(1);
            ResetColor(1);
        }

        // if (Input.GetKeyDown(KeyCode.Alpha1)){placedObjectTypeSO = placedObjectTypeSOList[0];}
        // if (Input.GetKeyDown(KeyCode.Alpha2)){placedObjectTypeSO = placedObjectTypeSOList[1];}

    }
}
