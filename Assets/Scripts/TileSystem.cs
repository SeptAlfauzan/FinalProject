using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class TileSystem : MonoBehaviour
{
    public Tile tile;
    public Grid grid;
    public Tilemap tilemap;

    [SerializeField] GameObject player;
    [SerializeField] GameObject plantObject;
    [SerializeField] int maximumPlantingDistance;

    private bool isNearPlayer;
    private Vector3Int currentCell;
    private Vector3Int activeCell;
    private Vector3 activeCellPos;
    private bool canPlanting = false;
    [SerializeField] private Vector3 plantingOffset;
    public PlantLocationData plantLocationData;
    // Update is called once per frame
    private void Update(){
        plantObject = player.GetComponent<Planting>().GetItemInHand();
        if(plantObject) canPlanting = true;
    }
    private void OnMouseOver() {
        // Debug.Log("asdasd");
        // return;
        if(canPlanting){
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                currentCell = SnapCoordinateToGrid(hit.point);//this also swap z to x position
                Vector3 currentCellPosition = SnapObjCoordinateToGrid(hit.point);
                
                float mouseToPlayerMagnitude = Mathf.Floor((currentCellPosition - player.transform.position).magnitude);

                if(currentCell != activeCell && mouseToPlayerMagnitude <= maximumPlantingDistance){
                    // set the new tile
                    tilemap.SetTile(currentCell, tile);
                    // erase activeCell
                    tilemap.SetTile(activeCell, null);
                    // save the new position for next frame
                    activeCell = currentCell;
                    activeCellPos = currentCellPosition;
                }

            }
        }

    }
    private Vector3Int SwapZToXposition(Vector3 originPos){
        return Vector3Int.FloorToInt(new Vector3(originPos.x, originPos.z, originPos.y));
    }
    private Vector3Int SnapCoordinateToGrid(Vector3 position){
        Vector3Int cellPos = grid.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        return SwapZToXposition(position);
    }

    public Vector3 SnapObjCoordinateToGrid(Vector3 position){
        
        Vector3Int cellPos = grid.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        return position;
    }

    private void OnMouseDown() {
        
        if(canPlanting){
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                Vector3 gridpos = SnapObjCoordinateToGrid(hit.point);
                plantObject = Instantiate(plantObject, this.transform);
                plantObject.transform.position = activeCellPos + plantingOffset;
                Debug.Log(activeCell);
            }
        }


    }
}
