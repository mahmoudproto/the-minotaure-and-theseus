using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiControls : MonoBehaviour
{
    [SerializeField]
    GameObject winnigScreen;
    [SerializeField]
    GameObject LosingScreen;
    private void Start()
    {
        EventManager.playerWonEvent += displayWinningScreen;
        EventManager.gameoverEvent += displayLosingScreen;
    }
    public void Rewind()
    {
        if (GameManager.Instance.Current_turn != turn.movement_execution)
            EventManager.Instance.rewindLastMove();
    }

    public void SkipTurn()
    {
        if (GameManager.Instance.Current_turn != turn.movement_execution)
            EventManager.Instance.startMinotaureTurn();
    }

    public void restartLevel()
    {
        Destroy(GameManager.Instance.levelManager.container);
        GameManager.Instance.levelManager.loadLevel(GameManager.Instance.levelManager.currenLevelIndex);
        winnigScreen.SetActive(false);
        LosingScreen.SetActive(false);
    }

    void displayWinningScreen()
    {
        winnigScreen.SetActive(true);
    }

    void displayLosingScreen()
    {
        LosingScreen.SetActive(true);
    }

    private void OnDestroy()
    {
        EventManager.playerWonEvent -= displayWinningScreen;
        EventManager.gameoverEvent -= displayLosingScreen;
    }

}
