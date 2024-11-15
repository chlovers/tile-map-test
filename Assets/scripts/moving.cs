using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class moving : MonoBehaviour
{
    public Tilemap tilemap;
    private Vector3Int currentTilePosition;
    private Vector3Int nextTilePosition;

    public float moveSpeed = 0.16f;

    void Start()
    {
        currentTilePosition = tilemap.WorldToCell(transform.position);
    }

    void Update()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            movement = Vector3.up;
        else if (Input.GetKey(KeyCode.S))
            movement = Vector3.down;
        else if (Input.GetKey(KeyCode.A))
            movement = Vector3.left;
        else if (Input.GetKey(KeyCode.D))
            movement = Vector3.right;

        nextTilePosition = currentTilePosition + new Vector3Int((int)movement.x, (int)movement.y, 0);


        if (IsFloor(nextTilePosition))
        {
            currentTilePosition = nextTilePosition;
            transform.position = tilemap.CellToWorld(currentTilePosition);
        }
    }

    // check if the next tile is a floor tile and not a wall or chest
    bool IsFloor(Vector3Int position)
    {
        TileBase tile = tilemap.GetTile(position);
        return tile != null && tile.name != "wallTile" && tile.name != "chestTile"; // Can't walk on walls or chests
    }
}
