using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager 
{
    static GameManager instance;
    public Transform player;
    public Transform minotaure;
    private turn current_turn;
    public turn Current_turn { get => current_turn;  }

    public LevelManager levelManager;

    public GameManager()
    {
        EventManager.startPlayerTurnEvent += startPlayerTurn;
    }
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }

    public void startTurn() {
        current_turn = turn.movement_execution;
    }

    public void startPlayerTurn()
    {
        current_turn = turn.player_turn;
    }

    public void startMinotaureTurn()
    {
        current_turn = turn.minotaur_turn;
    }

}

public enum turn
{
    player_turn,
    minotaur_turn,
    movement_execution
}
