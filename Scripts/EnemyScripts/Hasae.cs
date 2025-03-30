using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hasae : Enemy_Boss
{
    private GameObject playerCharacter;
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
        SpellName = "슈퍼 자이언트 개to끼";
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
        SpellName = "슈퍼 자이언트 개to끼";
        SpellCard(SpellName);
        yield return new WaitForSeconds(3f);

    }
    protected override IEnumerator Phase4() //패턴 5 : 통상, 슈터 추가
    {
        Health = 600f;
        TimerCoroutine = StartCoroutine(PhaseTimer(60));
        CutScene(2f);


        yield return new WaitForSeconds(3f);

    }
    protected override IEnumerator Phase3() //패턴 6 : 기술
    {
        Health = 900f;
        TimerCoroutine = StartCoroutine(PhaseTimer(60));
        CutScene(2f);
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
        CutScene(2f);
        PlaySFX(2);
        SpellName = "슈퍼 자이언트 개to끼";
        SpellCard(SpellName);
        yield return new WaitForSeconds(3f);

    }
}

