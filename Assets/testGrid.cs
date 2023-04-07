using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testGrid : MonoBehaviour
{

    [SerializeField] private Transform building;
    private Grid<GridObject> grid;

    private void Awake()
    {
        int gridWidth = 4;
        int gridHeight = 2;
        float cellSize = 10f;
        // grid = new Grid<bool>(gridWidth, gridHeight, cellSize, Vector3.zero);
        grid = new Grid<GridObject>(gridWidth, gridHeight, cellSize, Vector3.zero, (Grid<GridObject> g, int x, int z) => new GridObject(g, x, z));
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
            GridObject gridObject = grid.GetObject(x, z);
            if (gridObject.CanBuild())
            {
                Instantiate(building, grid.GetWorldPosition(x, z), Quaternion.Euler(0f, -90f, 0f));
            }
        }


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
    private Transform transform;

    public bool CanBuild()
    {
        Debug.Log(transform == null);
        return transform == null;
    }

    public void SetTransform(Transform transform)
    {
        this.transform = transform;
        grid.TriggerGridObjectChanged(x, z);
    }

    public void ClearTransform()
    {
        transform = null;
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
        return $"{x}, {z}";
    }

}