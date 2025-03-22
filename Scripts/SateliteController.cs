using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SateliteController : MonoBehaviour
{
    public GameObject Satelite1;
    public GameObject Satelite2;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetMotion(int motionNumber, float speed = 0)
    {
        animator.SetInteger("Motion", motionNumber);
        animator.speed = speed;
    }


}
