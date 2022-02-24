using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EventManager
{
    public delegate void playerTurn();
    public static event playerTurn startPlayerTurnEvent;
    public delegate void minotaureTurn();
    public static event minotaureTurn startMinotaureTurnEvent; 
    public delegate void gameover();
    public static event gameover gameoverEvent;
    public delegate void hunmanWon();
    public static event hunmanWon playerWonEvent;
    public delegate void rewindMove();
    public static event rewindMove rewindMoveEvent;

    static EventManager instance;

    public EventManager()
    {
    }

    public static EventManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EventManager();
            }
            return instance;
        }
    }

    public void startMinotaureTurn()
    {
        if (startMinotaureTurnEvent == null)
        {
            startPlayerTurn();
            return;
        }
        GameManager.Instance.startMinotaureTurn();
        startMinotaureTurnEvent();
    }

    public void startPlayerTurn()
    {
        if (startPlayerTurnEvent == null)
            return;
        GameManager.Instance.startPlayerTurn();
        startPlayerTurnEvent();
    }

    public void rewindLastMove()
    {
        rewindMoveEvent();
        GameManager.Instance.startPlayerTurn();
    }

    public void GameOver()
    {
        gameoverEvent();
        Debug.Log("Game Over");
    }

    public void playerWon()
    {
        playerWonEvent();
        Debug.Log("player Won");
    }

}