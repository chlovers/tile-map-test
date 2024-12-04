using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MapManager  MapManager;
    public PlayerController PlayerController;

    private TurnManager turnManager;

    public static GameManager Instance { get; private set; }
    public TurnManager TurnManager { get; private set; }

   

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


    void Start()
    {
        turnManager = new TurnManager();
        TurnManager = turnManager;

        MapManager.Init();
        PlayerController.Spawn(MapManager, new Vector2Int(1, 1));
    }
}
