using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woopsoon : Enemy_Boss
{
    //private GameObject playerCharacter;
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

        //playerCharacter = FindPlayer();

        base.Start();

        healthBar.SetName("Woopsoon");

        sateliteController = Satelite.GetComponent<SateliteController>();
        satelite1 = sateliteController.Satelite1;
        satelite2 = sateliteController.Satelite2;
        //Satelite.SetActive(false);
    }


    protected override void PhaseSetter(int remainLife)
    {
        base.PhaseSetter(remainLife);
        if (remainLife == 5)
        {
            StartCoroutine(skillMotion(1));
        }
        else if (remainLife == 3)
        {
            StartCoroutine(skillMotion(2));

        }
        else if (remainLife == 1)
        {
            StartCoroutine(skillMotion(3));

        }
        else
        {
            StartCoroutine(skillMotion(0, 2f));
        }


        Debug.Log($"웁순이의 남은 목숨: {Life}");
    }

    protected override IEnumerator Phase6() //패턴 1 : 통상
    {
        Health = 700f;
        TimerCoroutine = StartCoroutine(PhaseTimer(40));
        skillMotion(0, 2f);

        yield return new WaitForSeconds(1f);
        //Satelite.SetActive(true);
        sateliteSetter();
        sateliteController.SetMotion(1);
        yield return new WaitForSeconds(1f);
        while (true)
        {
            RandomMove(10f, 2f);

            for (int k = 0; k < 2; k++)
            {
                for (int i = 0; i < 5; i++)
                {
                    BasicAttack(satelite1, 6, 40f, 12f, playerCharacter, attackPrefab[1], 179, 0, 134);
                    PlaySFX(4);
                    yield return new WaitForSeconds(0.1f);
                }
                yield return new WaitForSeconds(0.5f);
            }

            RandomMove(10f, 2f);
            for (int i = 0; i < 5; i++)
            {
                BasicAttack(60, 30f, 4f, playerCharacter, attackPrefab[0], 255, 153, 230);
                PlaySFX(5);
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(2f);
        }

    }

    protected override IEnumerator Phase5() //패턴 2 : 기술
    {
        sateliteController.SetMotion(0);
        //Satelite.SetActive(false);

        Health = 900f;
        TimerCoroutine = StartCoroutine(PhaseTimer(60));
        CutScene(2f);
        PlaySFX(2);
        SpellName = "인방계의 헤르마프로디토스";
        SpellCard(SpellName);
        yield return new WaitForSeconds(3f);
        while (true)
        {
            RandomMove(20f, 4f);
            for (int i = 0; i < 10; i++)
            {
                BasicAttack(satelite1, 30, 30f, 4f, playerCharacter, attackPrefab[0], 125, 125, 125);

                BasicAttack(satelite2, 30, 30f, 4f, playerCharacter, attackPrefab[0], 125, 125, 125);
                PlaySFX(4);
                yield return new WaitForSeconds(0.3f);
                BasicAttack(60, 40f, 3f, playerCharacter, attackPrefab[2], 0, 82, 204);
                PlaySFX(4);
                yield return new WaitForSeconds(0.3f);
            }


            yield return new WaitForSeconds(2f);
        }

    }

    protected override IEnumerator Phase4() //패턴 3 : 통상
    {
        //skillMotion(0, 2f);
        Health = 400f;
        TimerCoroutine = StartCoroutine(PhaseTimer(20));

        yield return new WaitForSeconds(2f);
        StartCoroutine(ObjectMover(new Vector3(0, 0, -50), 2f));
        while (true)
        {

            BasicSpin(100, 40f, attackPrefab[1], 30f, 125, 125, 125);
            PlaySFX(5);


            yield return new WaitForSeconds(0.5f);
            BasicSpin(100, 40f, attackPrefab[1], -30f, 0, 82, 204);
            PlaySFX(5);

            yield return new WaitForSeconds(0.5f);
        }

    }

    protected override IEnumerator Phase3() //패턴 4 : 기술
    {
        Health = 900f;
        TimerCoroutine = StartCoroutine(PhaseTimer(60));
        CutScene(3f);
        PlaySFX(2);
        SpellName = "츤데레 피그말리온";
        SpellCard(SpellName);

        yield return new WaitForSeconds(1.5f);
        sateliteController.SetMotion(2);
        yield return new WaitForSeconds(0.1f);
        sateliteController.TurnTrail();
        yield return new WaitForSeconds(0.9f);

        

        while (true)
        {

            for (int i = 0; i < 10; i++)
            {

                SlowdownAttack(satelite1, 40, 10f, 4, playerCharacter, attackPrefab[2], 125, 125, 125, 8f, 1.5f);
                SlowdownAttack(satelite2, 40, 10f, 4, playerCharacter, attackPrefab[2], 0, 82, 204, 8f, 1.5f);
                PlaySFX(4);
                yield return new WaitForSeconds(0.1f);
            }
            RandomMove(10f, 2f);
            yield return new WaitForSeconds(2.5f);
            
        }

    }

    protected override IEnumerator Phase2() //패턴 5 : 통상
    {
        Health = 700f;
        TimerCoroutine = StartCoroutine(PhaseTimer(40));
        sateliteController.TurnTrail();
        yield return new WaitForSeconds(2f);
        StartCoroutine(ObjectMover(new Vector3(0, 0, -50), 3f));

        while (true)
        {
           for (int i = 0; i < 10; i++)
            {
                BasicAttack(30, 50f, 4f, playerCharacter, attackPrefab[1], 0, 82, 204);
                BasicAttack(30, 30f, 4f, playerCharacter, attackPrefab[0], 125, 125, 125);
                PlaySFX(4);
                yield return new WaitForSeconds(0.2f);
            }
            RandomMove(10f, 3f);
            yield return new WaitForSeconds(1f);
        }

    }

    protected override IEnumerator Phase1() //패턴 6 : 기술
    {
        Health = 1100f;
        TimerCoroutine = StartCoroutine(PhaseTimer(40));
        CutScene(3f);
        PlaySFX(2);
        SpellName = "자존심을 버리고\n조회수를 선택한 자의 말로";
        SpellCard(SpellName);
        yield return new WaitForSeconds(4f);
        while (true)
        {
            for (int i = 0; i< 5; i++)
            {
                SlowdownAttack(100, 60, 2f, playerCharacter, attackPrefab[2], 255, 153, 230, 0.2f);
                PlaySFX(4);
                yield return new WaitForSeconds(0.2f);
            }

            yield return new WaitForSeconds(1f);

            PlaySFX(5);
            BasicSpin(100, 40f, attackPrefab[3], 30f, 125, 125, 125);
            BasicSpin(100, 40f, attackPrefab[3], -30f, 125, 125, 125);

            yield return new WaitForSeconds(1f);

            RandomMove(10f, 2f);
            for (int i = 0; i < 10; i++)
            {
                ShootAround(playerCharacter, 20, attackPrefab[0], 30, 50, 0.2f, 179, 0, 134);
                PlaySFX(4);
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(2f);
            RandomMove(10f, 2f);

        }

    }
    private void sateliteSetter()
    {
        if (sateliteController != null) return;
        if (Satelite != null)
        {
            sateliteController = Satelite.GetComponent<SateliteController>();
            satelite1 = sateliteController.Satelite1;
            satelite2 = sateliteController.Satelite2;
        }
    }

    private IEnumerator satelitePattern(int num, float duration = 1f)
    {
        Satelite.SetActive(true);
        sateliteSetter();
        sateliteController.SetMotion(num);
        yield return new WaitForSeconds(duration);
        Satelite.SetActive(false);
        yield return null;
    }
}
