using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerSpawner : MonoBehaviour
{
    private SimpleMap board;
    private Vector2Int cellspot;
    
    
    public void Spawn(SimpleMap map,Vector2Int cell)
    {
        board = map;
        MoveTo(cell);
    }

    public void MoveTo(Vector2Int cell)
    {
        cellspot = cell;
        transform.position = board.CellToWorld(cellspot);
    }
    // Update is called once per frame
    void Update()
    {
        
        
        Vector2Int newCell = cellspot;
        bool hasmoved = false;

        if(Keyboard.current.wKey.wasPressedThisFrame)
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
        if (hasmoved == true)
        { 
            SimpleMap.CellData celldata = board.GetCellData(newCell);

            if (celldata != null && celldata.Passible)
            {
                MoveTo(newCell);
            }
        }
    }
}
