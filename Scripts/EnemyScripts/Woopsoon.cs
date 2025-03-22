using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woopsoon : Enemy_Boss
{
    private GameObject playerCharacter;
    public GameObject Satelite;
    private SateliteController sateliteController;

    private GameObject satelite1;
    private GameObject satelite2;

    protected override void Start()
    {
        Life = 6;
        Health = 100f;
        BossName = "웁순이";
        BossDescription = "웁님보다도 모션에 공을 들인 선녀";

        playerCharacter = FindPlayer();

        base.Start();

        healthBar.SetName("Woopsoon");
        sateliteController = Satelite.GetComponent<SateliteController>();
        satelite1 = sateliteController.Satelite1;
        satelite2 = sateliteController.Satelite2;
    }

    protected override void PhaseSetter(int remainLife)
    {
        base.PhaseSetter(remainLife);
        if (remainLife == 5)
        {
            StartCoroutine(skillMotion(1));
        }
        if (remainLife == 3)
        {
            StartCoroutine(skillMotion(2));

        }
        if (remainLife == 1)
        {
            StartCoroutine(skillMotion(3));

        }


        Debug.Log($"웁순이의 남은 목숨: {Life}");
    }

    protected override IEnumerator Phase6() //패턴 1 : 통상
    {
        Health = 700f;
        TimerCoroutine = StartCoroutine(PhaseTimer(40));
        while (true)
        {
            yield return new WaitForSeconds(2f);
            RandomMove(10f, 2f);
            
        }

    }

    protected override IEnumerator Phase5() //패턴 2 : 기술
    {
        Health = 900f;
        TimerCoroutine = StartCoroutine(PhaseTimer(60));
        CutScene(2f);
        PlaySFX(2);
        SpellName = "자존심을 버리고\n조회수를 선택한 자의 말로";
        SpellCard(SpellName);
        while (true)
        {
            yield return new WaitForSeconds(3f);
            RandomMove(10f, 2f);
            

        }

    }

    protected override IEnumerator Phase4() //패턴 3 : 통상
    {
        Health = 700f;
        TimerCoroutine = StartCoroutine(PhaseTimer(10));

        while (true)
        {
            yield return new WaitForSeconds(3f);
            RandomMove(10f, 2f);

        }

    }

    protected override IEnumerator Phase3() //패턴 4 : 기술
    {
        Health = 900f;
        TimerCoroutine = StartCoroutine(PhaseTimer(60));
        CutScene(3f);
        PlaySFX(2);
        SpellName = "모두의 아이돌은 죽지않아";
        SpellCard(SpellName);
        sateliteController.SetMotion(2);
        while (true)
        {
            yield return new WaitForSeconds(3f);
            RandomMove(10f, 2f);

        }

    }

    protected override IEnumerator Phase2() //패턴 5 : 통상
    {
        Health = 700f;
        TimerCoroutine = StartCoroutine(PhaseTimer(40));
        
        while (true)
        {
            yield return new WaitForSeconds(3f);
            RandomMove(10f, 2f);

        }

    }

    protected override IEnumerator Phase1() //패턴 6 : 기술
    {
        Health = 1100f;
        TimerCoroutine = StartCoroutine(PhaseTimer(40));
        CutScene(3f);
        PlaySFX(2);
        SpellName = "수많은 팬아트의 힘을 빌려...!";
        SpellCard(SpellName);
        while (true)
        {
            yield return new WaitForSeconds(3f);
            RandomMove(10f, 2f);

        }

    }
}
