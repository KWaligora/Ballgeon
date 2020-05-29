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

    [Header("List")]
    static List<GameObject> enemys = new List<GameObject>();
    static int enemysCounter = 0;

    [Header("Other")]
    Material material;   

    protected virtual void Start()
    {
        enemys.Add(gameObject);
        enemysCounter++;

        nextPos = pos2.position;
        currentHealth = maxHealth;        

        material = GetComponent<SpriteRenderer>().material;
    }

    protected void Update()
    {
        SetMovement();
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

        if (enemysCounter <= 0)
            Respawn();
    }

    //Enable collision and spriteRenderer. maxhealth++
    private void Respawn()
    {        
        foreach(GameObject enemy in enemys)
        {
            enemy.GetComponent<Enemys>().maxHealth += 1;
            enemy.GetComponent<Enemys>().currentHealth = enemy.GetComponent<Enemys>().maxHealth;
            enemy.GetComponent<Collider2D>().enabled = true;
            enemy.GetComponent<SpriteRenderer>().enabled = true;
            enemysCounter++;
        }
    }
    #endregion
}
