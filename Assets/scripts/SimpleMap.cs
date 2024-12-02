using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;


public class SimpleMap : MonoBehaviour
{
    public class CellData
    {
        public bool Passible;
    }
    
    
    private Tilemap tilemap;
    private Grid mapgrid;

    public PlayerSpawner Player;
    public int Width;
    public int Height;
    public Tile[] GroundTiles;
    public Tile[] WallTiles;
    private CellData[,] cellDatas;

    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponentInChildren<Tilemap>();
        mapgrid = GetComponentInChildren<Grid>();

        cellDatas = new CellData[Width, Height];

        for (int y = 0; y < Height; ++y)
        {
            for (int x = 0; x < Width; ++x)
            {
                Tile tile;

                if (x == 0 || y == 0 || x == Width - 1 || y == Height - 1)
                {
                    tile = WallTiles[Random.Range(0, WallTiles.Length)];
                   
                }
                else
                {
                    tile = GroundTiles[Random.Range(0, GroundTiles.Length)];
                    
                }

                tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
    }

    public Vector3 CellToWorld(Vector2Int cellIndex)
    {
        return mapgrid.GetCellCenterWorld((Vector3Int)cellIndex);
    }

    public CellData GetCellData(Vector2Int cellIndex)
    {
        if (cellIndex.x < 0 || cellIndex.x >= Width || cellIndex.y < 0 || cellIndex.y >= Height)
        {  
            return null; 
        }
        
        return cellDatas[cellIndex.x, cellIndex.y];
    }
}