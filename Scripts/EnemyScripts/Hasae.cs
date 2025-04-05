using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Hasae : Enemy_Boss
{
    private GameObject playerCharacter;
    public GameObject Satelite001;
    public GameObject Satelite002;
    public AimConstraint SateliteAim;
    public ShooterDroneAiming PlayerFollower;

    // Start is called before the first frame update
    protected override void Start()
    {
        Life = 8;
        Health = 100f;
        BossName = "하세";
        BossDescription = "남들보다 나이를 빠르게 먹는";

        playerCharacter = FindPlayer();

        base.Start();

        healthBar.SetName("Hasae");
    }

    protected override void PhaseSetter(int remainLife)
    {
        base.PhaseSetter(remainLife);
        if (remainLife == 7)
        {
            StartCoroutine(skillMotion(1));
        }
        else if (remainLife == 5)
        {
            StartCoroutine(skillMotion(2));
        }
        else if (remainLife == 4)
        {
            StartCoroutine(skillMotion(-1, 4.5f));
        }
        else if (remainLife == 3)
        {
            StartCoroutine(skillMotion(3));

        }
        else if (remainLife == 1)
        {
            StartCoroutine(skillMotion(4));

        }
        else
        {
            StartCoroutine(skillMotion(0, 2f));
        }


        Debug.Log($"하세의 남은 목숨: {Life}");
    }


    protected override IEnumerator Phase8() //패턴 1 : 통상
    {
        Health = 700f;
        TimerCoroutine = StartCoroutine(PhaseTimer(60));
        //MainCamera.SetActive(false);


        yield return new WaitForSeconds(3f);

    }
    protected override IEnumerator Phase7() //패턴 2 : 기술
    {
        Health = 900f;
        TimerCoroutine = StartCoroutine(PhaseTimer(60));

        CutScene(2f);
        PlaySFX(2);
        SpellName = "본인을 제외한 주변이 회춘하는 마법";
        SpellCard(SpellName);
        yield return new WaitForSeconds(3f);

    }
    protected override IEnumerator Phase6() //패턴 3 : 통상
    {
        
        Health = 400f;
        TimerCoroutine = StartCoroutine(PhaseTimer(60));


        yield return new WaitForSeconds(3f);
        

    }
    protected override IEnumerator Phase5() //패턴 4 : 기술
    {
        Health = 900f;
        TimerCoroutine = StartCoroutine(PhaseTimer(60));
        CutScene(2f);
        PlaySFX(2);
        SpellName = "자이언트 오리오리빔";
        SpellCard(SpellName);
        yield return new WaitForSeconds(3f);

    }
    protected override IEnumerator Phase4() //패턴 5 : 통상, 슈터 추가
    {
        
        Health = 600f;
        TimerCoroutine = StartCoroutine(PhaseTimer(60));
        CutScene(3f);
        yield return new WaitForSeconds(1.1f);
        ActivateShooter();


        yield return new WaitForSeconds(3f);
        SateliteAim.constraintActive= true;
        PlayerFollower.enabled= true;

        
        

    }
    protected override IEnumerator Phase3() //패턴 6 : 기술
    {
        Health = 900f;
        TimerCoroutine = StartCoroutine(PhaseTimer(60));
        CutScene(3f);
        PlaySFX(2);
        SpellName = "슈퍼 자이언트 개to끼";
        SpellCard(SpellName);
        yield return new WaitForSeconds(3f);

    }
    protected override IEnumerator Phase2() //패턴 7 : 통상
    {
        Health = 700f;
        TimerCoroutine = StartCoroutine(PhaseTimer(60));


        yield return new WaitForSeconds(3f);
    }

        protected override IEnumerator Phase1() //패턴 8 : 기술
    {
        Health = 900f;
        TimerCoroutine = StartCoroutine(PhaseTimer(60));
        CutScene(4f);
        PlaySFX(2);
        SpellName = "이러면 자존심 상하는데..";
        SpellCard(SpellName);
        yield return new WaitForSeconds(3f);

    }

    void ActivateShooter()
    {
        Satelite001.SetActive(true);
        Satelite002.SetActive(true);

        Animator animator1 = Satelite001.GetComponent<Animator>();
        Animator animator2 = Satelite002.GetComponent<Animator>();

        animator1.SetInteger("Motion", 1);
        animator2.SetInteger("Motion", 1);
    }
}

