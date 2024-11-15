using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Movin : MonoBehaviour
{
    [SerializeField]
    public Tilemap groundTileMap;
    [SerializeField]
    public Tilemap collisionTileMap;
    private PlayerMovement controls;


    private void Awake()
    {
        controls = new PlayerMovement();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();

    }

    private void Start()
    {
        controls.Main.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
    }

    private void Move(Vector2 direction)
    {
        if (CanMove(direction))
            transform.position += (Vector3)direction;


    }

    private bool CanMove(Vector2 direction)
    {
        Vector3Int gridPosition = groundTileMap.WorldToCell(transform.position + (Vector3)direction);
        if (!groundTileMap.HasTile(gridPosition) || collisionTileMap.HasTile(gridPosition) )
            return false;
        return true;
    }

}
