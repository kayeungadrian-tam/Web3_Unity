using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{

    private Transform visual;
    private PlacedObjectTypeSO placedObjectTypeSO;
    // Start is called before the first frame update
    void Start()
    {
        RefreshVisual();   
        testGrid.Instance.OnSelectedChanged += Instance_OnSelectedChanged;
    }

    private void Instance_OnSelectedChanged(object sender, System.EventArgs e) {
        RefreshVisual();
    }

    private void LateUpdate() {
        Vector3 targetPosition = testGrid.Instance.GetMouseWorldSnappedPosition();
        targetPosition.y = 1f;

        Debug.Log($"targetPosition: {targetPosition}");
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 15f);

        transform.rotation = Quaternion.Lerp(transform.rotation, testGrid.Instance.GetPlacedObjectRotation(), Time.deltaTime * 15f);
    }

    private void SetLayerRecursive(GameObject targetGameObject, int layer) {
        targetGameObject.layer = layer;
        foreach (Transform child in targetGameObject.transform) {
            SetLayerRecursive(child.gameObject, layer);
        }
    }


    private void RefreshVisual() {
        Debug.Log("RefreshVisual");
        if (visual != null) {
            Destroy(visual.gameObject);
            visual = null;
        }

        PlacedObjectTypeSO placedObjectTypeSO = testGrid.Instance.GetPlacedObjectTypeSO();

        if (placedObjectTypeSO != null) {
            visual = Instantiate(placedObjectTypeSO.visual, Vector3.zero, Quaternion.identity);
            visual.parent = transform;
            visual.localPosition = Vector3.zero;
            visual.localEulerAngles = Vector3.zero;
            SetLayerRecursive(visual.gameObject, 6);
        }
    }

}
