using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CellObject
{
    public int Health = 3;

    private int CurrentHealth;

    private void Awake()
    {
        GameManager.Instance.TurnManager.OnTick += TurnHappened;
    }

    private void OnDestroy()
    {
        GameManager.Instance.TurnManager.OnTick -= TurnHappened;
    }

    public override void Init(Vector2Int coord)
    {
        base.Init(coord);
        CurrentHealth = Health;
    }

    /*public override bool PlayerWantsToEnter()
    {
        CurrentHealth -= 1;

        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }

        return false;
    }
    */
    bool MoveTo(Vector2Int coord)
    {
        var board = GameManager.Instance.MapManager;
        var targetCell = board.GetCellData(coord);

      

       
        var currentCell = board.GetCellData(m_Cell);
       

       
        m_Cell = coord;
        transform.position = board.CellToWorld(coord);

        return true;
    }

    void TurnHappened()
    {
        // Ensure playerCell is of type Vector2Int
        Vector2Int playerCell = (Vector2Int)GameManager.Instance.PlayerController.Cell;

        int xDist = playerCell.x - m_Cell.x;
        int yDist = playerCell.y - m_Cell.y;

        int absXDist = Mathf.Abs(xDist);
        int absYDist = Mathf.Abs(yDist);

        if ((xDist == 0 && absYDist == 1)
            || (yDist == 0 && absXDist == 1))
        {
            // Your movement logic here (if applicable)
        }
        else
        {
            if (absXDist > absYDist)
            {
                if (!TryMoveInX(xDist))
                {
                    TryMoveInY(yDist);
                }
            }
            else
            {
                if (!TryMoveInY(yDist))
                {
                    TryMoveInX(xDist);
                }
            }
        }
    }

    bool TryMoveInX(int xDist)
    {
        
        if (xDist > 0)
        {
            return MoveTo(m_Cell + Vector2Int.right);
        }

        
        return MoveTo(m_Cell + Vector2Int.left);
    }

    bool TryMoveInY(int yDist)
    {
        
        if (yDist > 0)
        {
            return MoveTo(m_Cell + Vector2Int.up);
        }

       
        return MoveTo(m_Cell + Vector2Int.down);
    }
}
