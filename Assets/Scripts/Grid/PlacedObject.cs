using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObject : MonoBehaviour
{
    private PlacedObjectTypeSO placedObjectSO;
    private Vector2Int origin;
    private PlacedObjectTypeSO.Dir dir;

    public void DestroySelf(){
        Destroy(gameObject);
    }

    public static PlacedObject Create(Vector3 worldPosition, Vector2Int origin, PlacedObjectTypeSO.Dir dir, PlacedObjectTypeSO placedObjectSO){
        Transform placedObjectTransform = Instantiate(placedObjectSO.prefab, worldPosition, Quaternion.Euler(0, placedObjectSO.GetRotationAngle(dir), 0));


        PlacedObject placedObject = placedObjectTransform.GetComponent<PlacedObject>();

        placedObject.placedObjectSO = placedObjectSO;
        placedObject.origin = origin;
        placedObject.dir = dir;
        return placedObject;
    }

    public List<Vector2Int> GetGridPositionList(){
        return placedObjectSO.GetGridPositionList(origin, dir);
    }


}
