using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MapManager MapManager;
    public PlayerController PlayerController;
    private int Healthamount = 100;

    public static GameManager Instance { get; private set; }
    public TurnManager TurnManager { get; private set; }

    private void Awake()
    {
        // Ensure only one instance of GameManager
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Initialize TurnManager in Awake to ensure it's ready for use
        TurnManager = new TurnManager();
        TurnManager.OnTick += OnTurnHappen;
    }

    void Start()
    {
        // Initialize MapManager and spawn PlayerController
        MapManager.Init();
        PlayerController.Spawn(MapManager, new Vector2Int(1, 1));
    }

    void OnTurnHappen()
    {
        Debug.Log("Current amount of health: " + Healthamount);
    }
}