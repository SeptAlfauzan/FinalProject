using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class TileSystem : MonoBehaviour
{
    public Tile tile;
    public Grid grid;
    public Tilemap tilemap;
    private Vector3Int previous;

    // Update is called once per frame
    private void OnMouseOver() {
        // Debug.Log("asdasd");
        // return;
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
        {
            Vector3Int currentCell = SnapCoordinateToGrid(hit.point);
            if(currentCell != previous){
                // set the new tile
                tilemap.SetTile(currentCell, tile);
                // erase previous
                tilemap.SetTile(previous, null);
                // save the new position for next frame
                previous = currentCell;
            }

        }
    }
    private Vector3Int InvertZToXposition(Vector3 originPos){
        return Vector3Int.FloorToInt(new Vector3(originPos.x, originPos.z, originPos.y));
    }
    private Vector3Int SnapCoordinateToGrid(Vector3 position){
        Vector3Int cellPos = grid.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        return InvertZToXposition(position);
    }
}
