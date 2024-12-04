using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MapManager  MapManager;
    public PlayerController PlayerController;
    private int Healthamount = 100;
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
        TurnManager = new TurnManager();
        TurnManager.OnTick += OnTurnHappen;

        MapManager.Init();
        PlayerController.Spawn(MapManager, new Vector2Int(1, 1));
    }

    void OnTurnHappen()
    {
        Healthamount -= 1;
        Debug.Log("Current amount of health : " + Healthamount);
    }
}
