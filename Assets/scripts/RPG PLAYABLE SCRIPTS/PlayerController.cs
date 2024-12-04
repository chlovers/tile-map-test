using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
   private MapManager map;
    private Vector2Int cellspot;

    public void Spawn(MapManager mapManager, Vector2Int cell)
    {
        map = mapManager;
        MoveTo(cell);

        

    }
    
    public void MoveTo(Vector2Int cell)
    {
        cellspot = cell;
        transform.position = map.CellToWorld(cell);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2Int newCell = cellspot;
        bool hasmoved = false;

        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            newCell.y += 1;
            hasmoved = true;

        }
        else if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            newCell.y -= 1;
            hasmoved = true;
        }
        else if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            newCell.x += 1;
            hasmoved = true;
        }
        else if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            newCell.x -= 1;
            hasmoved = true;
        }

        if (hasmoved) 
        {
            MapManager.CellData cellData = map.GetCellData(newCell);

            if (cellData != null && cellData.passable)
            {
                GameManager.Instance.TurnManager.Nextturn();
                MoveTo(newCell);
            }
        }
    }
}
