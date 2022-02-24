using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : CharacterMovement
{
    // Start is called before the first frame update
    public  override void Start()
    {
        base.Start();
        GameManager.Instance.player = this.transform;
    }


    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.Current_turn==turn.player_turn)
        {
            checkPlayerInput();
        }
    }

    void checkPlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
             moveCharacter(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
             moveCharacter(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
             moveCharacter(Vector2.right);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
             moveCharacter(Vector2.left);
        }

    }

    public override void EndMove(Vector2 previousMove)
    {
        moves_stack.Push(previousMove);
        EventManager.Instance.startMinotaureTurn();
    }

    public override bool moveCharacter(Vector2 diriction)
    {
        bool canMove = base.moveCharacter(diriction);
        if (!canMove)
            GameManager.Instance.startPlayerTurn();
        return canMove;
    }
    public override void rewindMove()
    {
        Debug.Log("player rewinded");
        if (moves_stack.Count>0)
        {
            if (!this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(true);
            }
            Vector2 previousMove=moves_stack.Peek();
            this.transform.position =(Vector2)this.transform.position-previousMove;
            moves_stack.Pop();
        }
    }


    public override void OnDisable()
    {
        base.OnDisable();
    }
    public override void OnTriggerEnter2D(Collider2D other)
    {
        string collidedObject_tag = other.tag;
        if (collidedObject_tag == "fire pit")
        {
            this.gameObject.SetActive(false);
            EventManager.Instance.GameOver();
        }
        else if (collidedObject_tag == "exit")
        {
            EventManager.Instance.playerWon();
        }
    }

}
