using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject obj;
    public Grid grid;
    // Update is called once per frame
    private void OnMouseDown() {
        // Vector3 mousePos = Input.mousePosition;
        // Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        // Vector3 tilePos = grid.WorldToCell(worldPos);
        // Debug.Log(tilePos);
        // obj.transform.position = tilePos;

        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
        {
            Vector3 gridpos = SnapCoordinateToGrid(hit.point);
            obj.transform.position = gridpos + new Vector3(0,0.5f,0);
        }

    }

    public Vector3 SnapCoordinateToGrid(Vector3 position){
        
        Vector3Int cellPos = grid.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        return position;
    }
}
