using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class CharacterMovement : MonoBehaviour
{
    public Stack<Vector2> moves_stack;
    public float mAnimation_time=1;

    public virtual void Start()
    {
        moves_stack = new Stack<Vector2>();
        EventManager.rewindMoveEvent += rewindMove;
    }

    public virtual bool moveCharacter(Vector2 diriction)
    {
        GameManager.Instance.startTurn();
        RaycastHit2D raycastHit2D = Physics2D.Raycast(this.transform.position, diriction, 1);
        if (raycastHit2D.collider != null)
        {
            return false;
        }
        StartCoroutine(movementAnimation(diriction));
        return true;
    }
    

    public virtual IEnumerator movementAnimation(Vector2 diriction)
    {
        Vector2 startingposition = this.transform.position;
        Vector2 endingposition = (Vector2)this.transform.position + diriction;
        float elapsedtime = 0;
        while (elapsedtime<mAnimation_time)
        {
            elapsedtime += Time.deltaTime;
            //this.transform.position = Vector2.Lerp(startingposition,endingposition,(elapsedtime/mAnimation_time));
            this.transform.Translate(diriction*Time.deltaTime/mAnimation_time);
            yield return null;
        }
        this.transform.position= startingposition + diriction;
        //Debug.Log(this.transform.name+"position after moving = " + this.transform.position);
        EndMove(diriction);
    }

    public abstract void EndMove(Vector2 previousMove);

    public abstract void rewindMove();

    public abstract void OnTriggerEnter2D(Collider2D collision);

    public virtual void OnDisable()
    {
        EventManager.rewindMoveEvent -= rewindMove;
        this.gameObject.SetActive(false);
    }

}