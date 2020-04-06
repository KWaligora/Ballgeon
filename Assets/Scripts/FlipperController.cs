using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update

    private HingeJoint2D joint;
    private bool keyActive;
    public bool isRight;

    void Start()
    {
        joint = GetComponent<HingeJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if((!isRight && Input.GetAxis("LeftFire") > 0) || (!isRight && Input.GetAxis("RightFire") > 0))
            keyActive = true;
        else
            keyActive = false;
    }

    void FixedUpdate()
    {
        if (keyActive)
        {
            joint.useMotor = true;
        }
        else
        {
            joint.useMotor = false;
        }
    }
}
