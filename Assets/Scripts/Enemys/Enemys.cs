using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemys : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    public Transform pos1, pos2;
    protected bool isFaceingLeft;
    private Vector3 nextPos;

    [Header("Health")]
    public int maxHealth;
    private int currentHealth;    

    [Header("Other")]
    Material material;
    static int enemysCounter = 0;
    bool respawning = false;

    protected virtual void Start()
    {
        enemysCounter++;
        nextPos = pos2.position;
        currentHealth = maxHealth;

        material = GetComponent<SpriteRenderer>().material;
    }

    protected void Update()
    {
        SetMovement();
        if (enemysCounter <= 0)
        {
            Respawn();
        }
    }

    #region Movement
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
    #endregion

    #region HealthAndDamage
    //Take damage when hit
    protected void TakeDamage()
    {
        currentHealth -= 1;
        if (currentHealth <= 0)
            Die();
        else
            StartCoroutine(SetDamageTint());
    }

    private IEnumerator SetDamageTint()
    {
        material.SetVector("_Vector4", new Vector4(2, 2, 2, 1));
        yield return new WaitForSeconds(0.25f);
        material.SetVector("_Vector4", new Vector4(1, 1, 1, 1));
    }

    //Disable collision and spriteRenderer
    private void Die()
    {
        enemysCounter--;
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        CheckEnemysCounter();
    }

    //if there is no enemys give 0.1s time for respawn
    private void CheckEnemysCounter()
    {
        if (enemysCounter <= 0)
            StartCoroutine(ResetEnemyCounter());
    }

    //Enable collision and spriteRenderer
    private void Respawn()
    {        
        currentHealth = maxHealth;        
        gameObject.GetComponent<Collider2D>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    IEnumerator ResetEnemyCounter()
    {
        yield return new WaitForSeconds(0.1f);
        enemysCounter = 5;

    }
    #endregion
}
