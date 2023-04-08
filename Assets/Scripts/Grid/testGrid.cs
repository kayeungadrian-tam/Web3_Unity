using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testGrid : MonoBehaviour
{

    [SerializeField] private List<PlacedObjectTypeSO> placedObjectTypeSOList;
    
    private PlacedObjectTypeSO placedObjectTypeSO;
    private Grid<GridObject> grid;
    private PlacedObjectTypeSO.Dir dir = PlacedObjectTypeSO.Dir.Down;

    private void Awake()
    {
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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.GetXZ(GetMouseWorld(), out int x, out int z);
            // GridObject gridObject = grid.GetObject(x, z);



            bool canBuild = true;
            List<Vector2Int> gridPositionList = placedObjectTypeSO.GetGridPositionList(new Vector2Int(x, z), dir);

            foreach (Vector2Int gridPosition in gridPositionList)
            {
                if (!grid.GetObject(gridPosition.x, gridPosition.y).CanBuild())
                {
                    canBuild = false;
                    break;
                }
            }

            if (canBuild)
            {

                Vector2Int rotatioOffset = placedObjectTypeSO.GetRotationOffset(dir);
                Vector3 buildingWorldPosition = grid.GetWorldPosition(x, z) + new Vector3(rotatioOffset.x, 0, rotatioOffset.y) * grid.GetCellSize();

                PlacedObject placedObject = PlacedObject.Create(buildingWorldPosition, new Vector2Int(x, z), dir, placedObjectTypeSO);
                // Transform builtTransform = Instantiate(placedObjectTypeSO.prefab, buildingWorldPosition, Quaternion.Euler(0f, placedObjectTypeSO.GetRotationAngle(dir), 0f));

                foreach (Vector2Int gridPosition in gridPositionList)
                {
                    grid.GetObject(gridPosition.x, gridPosition.y).SetPlacedObject(placedObject);
                }
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

        if (Input.GetKeyDown(KeyCode.Alpha1)){placedObjectTypeSO = placedObjectTypeSOList[0];}
        if (Input.GetKeyDown(KeyCode.Alpha2)){placedObjectTypeSO = placedObjectTypeSOList[1];}

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
        // grid.SetValue(GetMouseWorldPosition(), 56);
        return Vector3.zero;


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