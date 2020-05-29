using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemys : MonoBehaviour
{
    [Header ("Movement")]
    public float speed;
    public Transform pos1, pos2;
    protected bool isFaceingLeft;
    private Vector3 nextPos;

    protected virtual void Start()
    {
        nextPos = pos2.position;       
    }

    protected void Update()
    {
        SetMovement();
    }

    //flip character
    protected void Flip()
    {
        isFaceingLeft = !isFaceingLeft;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    //Set movement rules
    protected void SetMovement()
    {
        if (transform.position == pos1.position)
        {
            Flip();
            nextPos = pos2.position;
        }

        else if (transform.position == pos2.position)
        {
            Flip();
            nextPos = pos1.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    //Draw debug line between pos1 and pos2
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}
