using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MulbumWoop : Enemy_Boss
{
    //private GameObject playerCharacter;
    public Image Filter;

    // Start is called before the first frame update
    protected override void Start()
    {
        Life = 4;
        Health = 100f;
        BossName = "물범웁";
        BossDescription = "심해를 싫어하는 수중생물";

        //playerCharacter = FindPlayer();

        base.Start();

        healthBar.SetName("Mulbum Woop");
        Filter.gameObject.SetActive(false);
    }

    protected override void PhaseSetter(int remainLife)
    {
        base.PhaseSetter(remainLife);
        if (remainLife > 2 && remainLife < 4)
        {
            StartCoroutine(skillMotion(3));
            PlayerCamera.CameraShake(1);
        }
        else if (remainLife == 1)
        {            
            StartCoroutine(skillMotion(4));
            PlayerCamera.CameraShake(1);
        }

        else
        {
            StartCoroutine(skillMotion(0, 2f));
        }


        Debug.Log($"물범웁의 남은 목숨: {Life}");
    }
    protected override IEnumerator Phase4() //패턴 1 : 통상
    {
        Health = 600f;
        TimerCoroutine = StartCoroutine(PhaseTimer(40));
        while (true)
        {
            yield return new WaitForSeconds(2f);
            RandomMove(10f, 2f);
            for (int i = 0; i < 10; i++)
            {
                ShootAround(playerCharacter, 10, attackPrefab[0], 30, 50, 0.2f, 108, 230, 227);
                PlaySFX(4);
                SingleShot(60, attackPrefab[1], playerCharacter, 9, 41, 214);
                yield return new WaitForSeconds(0.3f);
            }
            
        }

    }
    protected override IEnumerator Phase3() //패턴 2 : 기술
    {
        
        Health = 700f;
        TimerCoroutine = StartCoroutine(PhaseTimer(60));
        PlaySFX(2);
        //yield return new WaitForSeconds(0.5f);
        CutScene(1f);


        SpellName = "물범웁 탈출기 재시동";
        SpellCard(SpellName);

        //Vector3 toRight = new Vector3(45, 0, -50);
        //Vector3 toLeft = new Vector3(-45, 0, -50);
        yield return new WaitForSeconds(3f);
        int X = 0;
        int Y = 0;
        while (true)
        {
            /*
            yield return new WaitForSeconds(3f);
            StartCoroutine(ObjectMover(toLeft, 2f));
            yield return new WaitForSeconds(1f);
            MakeBlocksLeft();

            yield return new WaitForSeconds(3f);
            StartCoroutine(ObjectMover(toRight, 2f));
            yield return new WaitForSeconds(1f);
            MakeBlocksRight();
            */
            X = Random.Range(-40, 40);
            Y = Random.Range(-40, 40);

            Vector3 nextPosition = new Vector3(X, Y, -60);
            StartCoroutine(ObjectMover(nextPosition, 2f));
            yield return new WaitForSeconds(1f);

            if (Random.Range(0, 10) % 2 == 0)
            {
                AroundBlocks(nextPosition, 10);
            }
            else
            {
                AroundBlocksVertical(nextPosition, 10);
            }

            yield return new WaitForSeconds(1f);

            for (int i = 0; i < 5; i++)
            {
                PlaySFX(4);
                BasicAttack(30, 15, 3, playerCharacter, attackPrefab[0]);
                yield return new WaitForSeconds(0.4f);
            }


        }
        
    }

    protected override IEnumerator Phase2() //패턴 3 : 통상
    {
        Health = 600f;
        ResetProjectile("Block");
        TimerCoroutine = StartCoroutine(PhaseTimer(40));
        Vector3 originPoint = new Vector3(0, 0, -50);
        
        yield return new WaitForSeconds(2f);
        StartCoroutine(ObjectMover(originPoint, 2f));
        while (true)
        {
            
            for (int i = 0; i < 15; i++)
            {
                ShootAround(playerCharacter, 20, attackPrefab[0], 40, 60, 0.2f, 108, 230, 227);
                SingleShot(75, attackPrefab[1], playerCharacter, 9, 41, 214);
                PlaySFX(4);
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(2f);
            RandomMove(30f, 3f);
        }
    }

    protected override IEnumerator Phase1() //패턴 4 : 기술
    {
        Health = 900f;
        TimerCoroutine = StartCoroutine(PhaseTimer(60));
        CutScene(3f);
        PlaySFX(2);

        SpellName = "심해 속 생물의 습격";
        SpellCard(SpellName);

        yield return new WaitForSeconds(1.5f);
        
        StartCoroutine(FadeIn(3f));
        yield return new WaitForSeconds(2.5f);

        Vector3 newPosition = new Vector3(0, 0, -60);

        while (true)
        {
            
            animator.SetInteger("Motion", 2);
            yield return new WaitForSeconds(1f);
            newPosition = new Vector3(transform.position.x, transform.position.y, 60);
            StartCoroutine(ObjectMover(newPosition, 2f));
            for (int i = 0; i < 5; i++)
            {
                PlaySFX(5);
                BasicAttack(40, 20, attackPrefab[3], 66, 203, 245);
                yield return new WaitForSeconds(0.3f);
            }
            animator.SetInteger("Motion", 0);
            yield return new WaitForSeconds(2f);


            newPosition = new Vector3(playerCharacter.transform.position.x, playerCharacter.transform.position.y, -60);
            StartCoroutine(ObjectMover(newPosition, 4f));

            for (int i = 0; i < 10; i++)
            {
                PlaySFX(4);
                SingleShot(30f, attackPrefab[0], playerCharacter, 255, 0, 0);
                yield return new WaitForSeconds(0.2f);
            }

            yield return new WaitForSeconds(2f);
        }

    }

    private void MakeBlocksRight(float speed = 30f)
    {
        int StarterX = Random.Range(0, 35);
        int StarterY = Random.Range(-30, 60);
        int StarterZ = Random.Range(-100, -85);
        int EndZ = Random.Range(-100, -85);

        Vector3 startPosition;
        Vector3 endPosition;

        if (StarterZ >= EndZ)
        {
            startPosition = new Vector3(StarterX, StarterY, StarterZ);
            endPosition = new Vector3(-60, -60, EndZ);
        }

        else
        {
            startPosition = new Vector3(StarterX, StarterY, EndZ);
            endPosition = new Vector3(-60, -60, StarterZ);
        }

        AttackBlocks(attackPrefab[2], startPosition, endPosition, 5f, speed);
    }

    private void MakeBlocksLeft(float speed = 30f)
    {
        int EndX = Random.Range(-35, 0);
        int EndY = Random.Range(-60, 30);
        int StarterZ = Random.Range(-100, -85);
        int EndZ = Random.Range(-100, -85);

        Vector3 startPosition;
        Vector3 endPosition;

        if (StarterZ >= EndZ)
        {
            startPosition = new Vector3(60, 60, StarterZ);
            endPosition = new Vector3(EndX, EndY, EndZ);
        }

        else
        {
            startPosition = new Vector3(60, 60, EndZ);
            endPosition = new Vector3(EndX, EndY, StarterZ);
        }

        AttackBlocks(attackPrefab[2], startPosition, endPosition, 5f, speed);
    }

    private void AroundBlocks(Vector3 Position, int gap = 5, float speed = 30f)
    {
        int X = (int)(Position.x);
        int Y = (int)(Position.y);

        int Height1 = Random.Range(Y + gap, 60);
        int Height2 = Random.Range(-60, Y - gap);

        Vector3 StartPosition = new Vector3(60, Height1, -90);
        Vector3 EndPosition = new Vector3(X + gap, Height2, -90);


        AttackBlocks(attackPrefab[2], StartPosition, EndPosition, 5f, speed);

        StartPosition = new Vector3(X - gap, Height1, -90);
        EndPosition = new Vector3(-60, Height2, -90);

        AttackBlocks(attackPrefab[2], StartPosition, EndPosition, 5f, speed);

    }

    private void AroundBlocksVertical(Vector3 Position, int gap = 5, float speed = 30f)
    {
        int X = (int)(Position.x);
        int Y = (int)(Position.y);
        int Z = (int)(Position.z);

        int HorizentalLeft = Random.Range(X + gap, 60);
        int HorizentalRight = Random.Range(-60, X - gap);

        Vector3 StartPosition = new Vector3(HorizentalLeft, 60, -90);
        Vector3 EndPosition = new Vector3(HorizentalRight, Y + gap, -90);


        AttackBlocks(attackPrefab[2], StartPosition, EndPosition, 5f, speed);

        StartPosition = new Vector3(HorizentalLeft, Y - gap, -90);
        EndPosition = new Vector3(HorizentalRight, -60, -90);

        AttackBlocks(attackPrefab[2], StartPosition, EndPosition, 5f, speed);

    }

    private IEnumerator FadeIn(float durationTime)
    {
        Filter.gameObject.SetActive(true);
        float elapsedTime = 0f;

        Color imageColor = Filter.color;
        imageColor.a = 0f; //alpha
        Filter.color = imageColor;

        while (elapsedTime < durationTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / durationTime);

            imageColor.a = alpha;
            Filter.color = imageColor;

            yield return null;

        }

        imageColor.a = 1f;
        Filter.color = imageColor;

        //StartCoroutine(FadeOut(4f));
    }

    protected override void Death()
    {
        Filter.gameObject.SetActive(false);
        ResetProjectile("Enemy");
        base.Death();
    }
}
