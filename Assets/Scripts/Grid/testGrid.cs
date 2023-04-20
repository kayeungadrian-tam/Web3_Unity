using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testGrid : MonoBehaviour
{

    public static testGrid Instance { get; private set; }

    public event EventHandler OnSelectedChanged;
    public event EventHandler OnObjectPlaced;


    [SerializeField] private List<PlacedObjectTypeSO> placedObjectTypeSOList;
    
    private PlacedObjectTypeSO placedObjectTypeSO;
    private Grid<GridObject> grid;
    private PlacedObjectTypeSO.Dir dir = PlacedObjectTypeSO.Dir.Down;
    private bool isDemolishActive;


    private void Awake()
    {
        Instance = this;
        int gridWidth = 6;
        int gridHeight = 6;
        float cellSize = 10f;
        grid = new Grid<GridObject>(gridWidth, gridHeight, cellSize, Vector3.zero, (Grid<GridObject> g, int x, int z) => new GridObject(g, x, z));
    
        placedObjectTypeSO = placedObjectTypeSOList[0];
    }

    private void Start()
    {
        // grid = new Grid(4, 2, 10f, new Vector3(20, 0, 0));
    }

    private bool IsValidGridPosition(int x, int z){
        List<Vector2Int> gridPositionList = placedObjectTypeSO.GetGridPositionList(new Vector2Int(x, z), dir);

        foreach (Vector2Int gridPosition in gridPositionList)
        {
            if (!grid.GetObject(gridPosition.x, gridPosition.y).CanBuild())
            {
                return false;
            }
        }
        return true;
    }

    private void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            grid.GetXZ(GetMouseWorld(), out int x, out int z);

            // bool canBuild = true;
            List<Vector2Int> gridPositionList = placedObjectTypeSO.GetGridPositionList(new Vector2Int(x, z), dir);



            if (IsValidGridPosition(x, z))
            {

                Vector2Int rotatioOffset = placedObjectTypeSO.GetRotationOffset(dir);
                Vector3 buildingWorldPosition = grid.GetWorldPosition(x, z) + new Vector3(rotatioOffset.x, 0, rotatioOffset.y) * grid.GetCellSize();

                // Debug.Log("--------------");
                // Debug.Log(buildingWorldPosition);
                // Debug.Log("--------------");
                // Debug.Log(dir);
                // Debug.Log("--------------");
                // Debug.Log(placedObjectTypeSO);

                PlacedObject placedObject = PlacedObject.Create(buildingWorldPosition, new Vector2Int(x, z), dir, placedObjectTypeSO);

                foreach (Vector2Int gridPosition in gridPositionList)
                {
                    grid.GetObject(gridPosition.x, gridPosition.y).SetPlacedObject(placedObject);
                }
                OnObjectPlaced?.Invoke(placedObject, EventArgs.Empty);
                // return true;
            }
            else
            {
                Debug.Log("Cannot build");
            }
        }

        if(Input.GetMouseButtonDown(1)){
            GridObject gridObject = grid.GetObject(GetMouseWorld());
            PlacedObject placedObject = gridObject.GetPlacedObject();
            if(placedObject != null){
                
                placedObject.DestroySelf();
                
                List<Vector2Int> gridPositionList = placedObject.GetGridPositionList();
                
                foreach (Vector2Int gridPosition in gridPositionList)
                {
                    grid.GetObject(gridPosition.x, gridPosition.y).ClearPlacedObject();
                }

            }   
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            dir = placedObjectTypeSO.GetNextDir(dir);
        }


        if (Input.GetKeyDown(KeyCode.Alpha1)){
            placedObjectTypeSO = placedObjectTypeSOList[0];
            RefreshSelectedObjectType();
            }
        if (Input.GetKeyDown(KeyCode.Alpha2)){
            placedObjectTypeSO = placedObjectTypeSOList[1];
            RefreshSelectedObjectType();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)){
            placedObjectTypeSO = placedObjectTypeSOList[2];
            RefreshSelectedObjectType();
        }

    }


    // public bool TryPlaceObject(int x, int y, PlacedObjectTypeSO placedObjectTypeSO, PlacedObjectTypeSO.Dir dir) {
    //     return TryPlaceObject(new Vector2Int(x, y), placedObjectTypeSO, dir, out PlacedObject placedObject);
    // }

    // public bool TryPlaceObject(Vector2Int placedObjectOrigin, PlacedObjectTypeSO placedObjectTypeSO, PlacedObjectTypeSO.Dir dir) {
    //     return TryPlaceObject(placedObjectOrigin, placedObjectTypeSO, dir, out PlacedObject placedObject);
    // }

    // public bool TryPlaceObject(Vector2Int placedObjectOrigin, PlacedObjectTypeSO placedObjectTypeSO, PlacedObjectTypeSO.Dir dir, out PlacedObject placedObject) {
    //     // Test Can Build
    //     List<Vector2Int> gridPositionList = placedObjectTypeSO.GetGridPositionList(placedObjectOrigin, dir);
    //     bool canBuild = true;
    //     foreach (Vector2Int gridPosition in gridPositionList) {
    //         //bool isValidPosition = grid.IsValidGridPositionWithPadding(gridPosition);
    //         bool isValidPosition = grid.IsValidGridPosition(gridPosition);
    //         if (!isValidPosition) {
    //             // Not valid
    //             canBuild = false;
    //             break;
    //         }
    //         if (!grid.GetGridObject(gridPosition.x, gridPosition.y).CanBuild()) {
    //             canBuild = false;
    //             break;
    //         }
    //     }

    //     if (canBuild) {
    //         Vector2Int rotationOffset = placedObjectTypeSO.GetRotationOffset(dir);
    //         Vector3 placedObjectWorldPosition = grid.GetWorldPosition(placedObjectOrigin.x, placedObjectOrigin.y) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();

    //         placedObject = PlacedObject.Create(placedObjectWorldPosition, placedObjectOrigin, dir, placedObjectTypeSO);

    //         foreach (Vector2Int gridPosition in gridPositionList) {
    //             grid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedObject(placedObject);
    //         }


    //         OnObjectPlaced?.Invoke(placedObject, EventArgs.Empty);

    //         return true;
    //     } else {
    //         // Cannot build here
    //         placedObject = null;
    //         return false;
    //     }
    // }



    private void DeselectObjectType() {
        placedObjectTypeSO = null;
        isDemolishActive = false;
        RefreshSelectedObjectType();
    }


    private void RefreshSelectedObjectType() {
        //UpdateCanBuildTilemap();

        if (placedObjectTypeSO == null) {
            //TilemapVisual.Instance.Hide();
        } else {
            //TilemapVisual.Instance.Show();
        }

        OnSelectedChanged?.Invoke(this, EventArgs.Empty);
    }



    private Vector3 GetMouseWorld()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Declare a variable to store the hit information
        RaycastHit hit;
        // Cast the ray and check if it hits something
        if (Physics.Raycast(ray, out hit))
        {
            // Get the position of the hit point in world space
            Vector3 mousePosition = hit.point;

            return mousePosition;
        }
        return Vector3.zero;
    }

    public Quaternion GetPlacedObjectRotation() {
        if (placedObjectTypeSO != null) {
            return Quaternion.Euler(0, placedObjectTypeSO.GetRotationAngle(dir), 0);
        } else {
            return Quaternion.identity;
        }
    }

    public PlacedObjectTypeSO GetPlacedObjectTypeSO() {
        return placedObjectTypeSO;
    }


    public Vector3 GetMouseWorldSnappedPosition() {
        Vector3 mousePosition = GetMouseWorld();
        grid.GetXZ(mousePosition, out int x, out int z);

        if (placedObjectTypeSO != null) {
            Vector2Int rotationOffset = placedObjectTypeSO.GetRotationOffset(dir);
            Vector3 placedObjectWorldPosition = grid.GetWorldPosition(x, z) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();
            return placedObjectWorldPosition;
        } else {
            return mousePosition;
        }
    }


}

public class GridObject
{
    private Grid<GridObject> grid;
    private int x;
    private int z;
    private PlacedObject placedObject;

    public bool CanBuild()
    {
        return placedObject == null;
    }

    public void SetPlacedObject(PlacedObject placedObject)
    {
        this.placedObject = placedObject;
        grid.TriggerGridObjectChanged(x, z);
    }

    public PlacedObject GetPlacedObject(){
        return placedObject;
    }

    public void ClearPlacedObject()
    {
        placedObject = null;
        grid.TriggerGridObjectChanged(x, z);
    }

    public GridObject(Grid<GridObject> grid, int x, int z)
    {
        this.grid = grid;
        this.x = x;
        this.z = z;
    }

    public override string ToString()
    {
        return $"{x}{z}{placedObject == null}";
    }

}