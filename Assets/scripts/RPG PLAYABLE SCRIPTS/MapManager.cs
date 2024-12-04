using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{

    private Tilemap tilemap;
    public int Width;
    public int Height;
    public Tile[] floortiles;
    public Tile[] walltiles;
    private CellData[,] tiledata;
    public Grid grid;
    public PlayerController player;


    public class CellData
    {
        public bool passable;
    }

     public void Init()
    {
        tilemap = GetComponentInChildren<Tilemap>();
       grid = GetComponentInChildren<Grid>();
        
        tiledata = new CellData[Width, Height];

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                Tile tile;
                tiledata[x, y] = new CellData();

                if (x == 0 || y == 0 || x == Width -1 || y == Height -1)
                {
                    tile = walltiles[Random.Range(0, walltiles.Length)];
                    tiledata[x, y].passable = false;
                }
            
                else
                {
                    tile = floortiles[Random.Range(0, floortiles.Length)];
                    tiledata[x, y].passable = true;
                }

                tilemap.SetTile(new Vector3Int(x,y,0), tile);

            }  
        }
        player.Spawn(this, new Vector2Int(1,1));
    }

    public Vector3 CellToWorld(Vector2Int cellIndex)
    {
        return grid.GetCellCenterWorld((Vector3Int)cellIndex);
    }


    public CellData GetCellData(Vector2Int cellIndex)
    {
        if (cellIndex.x < 0 || cellIndex.x >= Width || cellIndex.y < 0 || cellIndex.y >= Height)
        {
            return null;
        }

        return tiledata[cellIndex.x, cellIndex.y];
    }

}
