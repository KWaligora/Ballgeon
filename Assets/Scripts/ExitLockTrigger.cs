using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLockTrigger : MonoBehaviour
{
    public GameObject exitLockObject;

    private void OnTriggerExit2D(Collider2D collision)
    {
        exitLockObject.GetComponent<SpriteRenderer>().enabled = true;
        exitLockObject.GetComponent<PolygonCollider2D>().enabled = true;
    }
}
