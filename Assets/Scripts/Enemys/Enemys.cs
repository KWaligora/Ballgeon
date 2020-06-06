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
    bool immortal = false;

    [Header("List")]
    static List<GameObject> enemys = new List<GameObject>();
    static int enemysCounter = 0;

    [Header("Level")]
    List<Vector4> lvlColors = new List<Vector4>();
    static int currentLvl = 0;
    static int currentColorIndex;
    static int colorCount;

    [Header("Other")]
    Material material;
    const int BONUS_POINTS = 50;

    protected void Start()
    {
        enemys.Add(gameObject);
        enemysCounter++;

        nextPos = pos2.position;
        currentHealth = maxHealth;        

        material = GetComponent<SpriteRenderer>().material;
        LoadColors();
        colorCount = lvlColors.Count;
        currentColorIndex = 0;        
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
        if (!immortal)
        {
            currentHealth -= 1;
            if (currentHealth <= 0)
                Die();
            else
            {
                StartCoroutine(Immortality());
                StartCoroutine(SetDamageTint());
            }
        }
    }

    //Object is untouchable for a moment after hit
    private IEnumerator Immortality()
    {
        immortal = true;
        yield return new WaitForSeconds(0.25f);
        immortal = false;
    }

    private IEnumerator SetDamageTint()
    {
        material.SetVector("_Vector4", lvlColors[currentColorIndex] * 2);
        yield return new WaitForSeconds(0.25f);
        material.SetVector("_Vector4", lvlColors[currentColorIndex]);
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
        SetNewLvl();
        foreach(GameObject enemy in enemys)
        {            
            enemy.GetComponent<Enemys>().maxHealth += 1;
            enemy.GetComponent<Enemys>().currentHealth = enemy.GetComponent<Enemys>().maxHealth;
            enemy.GetComponent<Collider2D>().enabled = true;
            enemy.GetComponent<SpriteRenderer>().enabled = true;       
            enemy.GetComponent<Enemys>().material.SetVector("_Vector4", lvlColors[currentColorIndex]);

            enemysCounter++;
        }
    }

    #endregion

    //Set new value of currentLvl
    static void SetNewLvl()
    {
        currentLvl++;
        currentColorIndex = currentLvl % colorCount;

        ScoreManager.Instance.SetLevel(currentLvl + 1);
        ScoreManager.Instance.AddScore(BONUS_POINTS); //bonus score for leveling up
    }

    //Reset static variables on lvl reload
    public static void ResetStaticVariables()
    {
        enemys.Clear();
        enemysCounter = 0;
        currentLvl = 0;
    }

    //Load color to lvlcolours list
    void LoadColors()
    {
        lvlColors.Clear();
        lvlColors.Add(new Vector4(1, 1, 1, 1));
        lvlColors.Add(new Vector4(1, 0.5f, 0.5f, 1));
        lvlColors.Add(new Vector4(0.5f, 0.5f, 1, 1));
    }
}
