using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SateliteController : MonoBehaviour
{
    public GameObject Satelite1;
    public GameObject Satelite2;

    private Animator animator;

    public GameObject SateliteTrail1;
    public GameObject SateliteTrail2;

    private bool trailSwitch = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetMotion(int motionNumber, float speed = 1)
    {
        animator.SetInteger("Motion", motionNumber);
        animator.speed = speed;
    }

    public void TurnTrail()
    {
        if (trailSwitch)
        {
            trailSwitch = false;
            SateliteTrail1.SetActive(false);
            SateliteTrail2.SetActive(false);
        }
        else
        {
            trailSwitch= true;
            SateliteTrail1.SetActive(true);
            SateliteTrail2.SetActive(true);
        }
    }


}
