using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaureControls : CharacterMovement
{
    int moves_remaining ;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        GameManager.Instance.minotaure = this.transform;
        EventManager.startMinotaureTurnEvent += startMinotaureTurn;
    }

    void startMinotaureTurn()
    {
        moves_remaining = 2;
        moves_stack.Push(this.transform.position);
        moveMinotaureHorizantally();
    }

    //try to move the minotaure horizently first if no moves available move horizentaly
    void moveMinotaureHorizantally()
    {
        float horizental_deff = this.transform.position.x - GameManager.Instance.player.transform.position.x;

        if (horizental_deff < 0)
            horizentalMovement(Vector2.right);

        else if (horizental_deff > 0)
            horizentalMovement(Vector2.left);
        else
            moveMinotaureVerticaly();
    }

    //try to move the minotaure vertically if no moves available then he is stuck end the turn
    void moveMinotaureVerticaly()
    {
        float vertical_deff = this.transform.position.y - GameManager.Instance.player.transform.position.y;

        if (vertical_deff < 0)
            verticalMovement(Vector2.up);

        else if (vertical_deff > 0)
            verticalMovement(Vector2.down);
        else
            endTheTurn();
    }

    void horizentalMovement(Vector2 diriction)
    {
        bool canMoveHorizentally=base.moveCharacter(diriction);
        if (canMoveHorizentally)
        {
            moves_remaining--;
        }
        else
        {
            moveMinotaureVerticaly();
        }
    }
    void verticalMovement(Vector2 diriction)
    {
        bool canMoveVertically = base.moveCharacter(diriction);
        if (canMoveVertically)
        {
            moves_remaining--;
        }
        else
        {
            endTheTurn();
        }
    }

    public override void EndMove(Vector2 previousMove)
    {
        if (moves_remaining > 0)
        {
            //moveMinotaurOneStep();
            moveMinotaureHorizantally();
            return;
        }
        
         endTheTurn();
    }

    public void endTheTurn()
    {
        GameManager.Instance.startPlayerTurn();
    }

    public override void OnDisable()
    {
        base.OnDisable();
        EventManager.startMinotaureTurnEvent -= startMinotaureTurn;
    }

    public override void rewindMove()
    {
        if (moves_stack.Count > 0)
        {
            if (!this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(true);
            }
            Vector2 previousPosition = moves_stack.Peek();
            this.transform.position = previousPosition;
            moves_stack.Pop();
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        string collidedObject_tag = other.tag;
        if (collidedObject_tag == "fire pit")
        {
            this.gameObject.SetActive(false);
            GameManager.Instance.startPlayerTurn();
        }
        else if (collidedObject_tag=="player")
        {
            EventManager.Instance.GameOver();
        }
    }
}
