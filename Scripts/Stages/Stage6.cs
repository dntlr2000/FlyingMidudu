using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6 : Stage
{
    // Start is called before the first frame update
    protected override void Start()
    {
        text = $"최종장\n드디어 보물이 있는 곳에 도달했지만\n흰 머리 악인의 앞을 가로막는 건\n복수의 칼날을 가다듬는\n어느 낯익은 방개..";
        base.Start();
        NextStageName = "EndScene";
        currentStage = 6;
        SetBGM(9);

    }

    protected override IEnumerator StagePhase1()
    {
        //0: 방개H(공격속도 빨라짐), 1:퍼그A, 2:퍼그B, 3:방개I(5:1), 4:방개J(B상위호환)

        Debug.Log("Phase 1 Started");
        yield return new WaitForSeconds(6f);


        SpawnEnemy(Enemy[1], 30, 10, -50);
        yield return new WaitForSeconds(3f);
        SpawnEnemy(Enemy[1], -30, 10, -50);
        yield return new WaitForSeconds(6f);

        SpawnEnemy(Enemy[2], 40, -5, -50);
        yield return new WaitForSeconds(0.6f);
        SpawnEnemy(Enemy[2], 35, -15, -50);
        yield return new WaitForSeconds(6f);

        SpawnEnemy(Enemy[2], -40, -5, -50);
        yield return new WaitForSeconds(0.6f);
        SpawnEnemy(Enemy[2], -35, -15, -50);
        yield return new WaitForSeconds(6f);


        while (CheckEnemyExist())
        {
            yield return new WaitForSeconds(2f);
        }
        Debug.Log("Phase 1 Clear");
        StartCoroutine(StagePhase2());
    }

    protected override IEnumerator StagePhase2()
    {
        int y;
        for (int i = 0; i < 8; i++)
        {
            y = Random.Range(-10, 10);
            SpawnEnemy(Enemy[4], -60, y, -50, true);
            yield return new WaitForSeconds(0.15f);
        }
        yield return new WaitForSeconds(9f);

        for (int i = 0; i < 8; i++)
        {
            y = Random.Range(-10, 10);
            SpawnEnemy(Enemy[4], 60, y, -50, true);
            yield return new WaitForSeconds(0.15f);

            if (i == 3)
            {
                SpawnEnemy(Enemy[1], -30, 10, -50);
            }
            else if (i == 6)
            {
                SpawnEnemy(Enemy[1], -35, -10, -50);
            }

        }
        yield return new WaitForSeconds(6f);


        while (CheckEnemyExist())
        {
            yield return new WaitForSeconds(2f);
        }
        Debug.Log("Phase 2 Clear");


        StartCoroutine(StagePhase3());
    }

    protected IEnumerator StagePhase3()
    {

        SpawnBoss(MidBoss, 0, 0, -50);

        while (CheckEnemyExist("EnemyBoss"))
        {
            yield return new WaitForSeconds(2f);
        }
        Debug.Log("Phase 3 Clear");



        StartCoroutine(StagePhase4());
    }

    protected IEnumerator StagePhase4()
    {
        int q = -1;
        for (int i = -40; i < 45; i += 20)
        {
            SpawnEnemy(Enemy[3], i, q * 30, -50);
            q *= -1;
            yield return new WaitForSeconds(1.2f);
            if (i == 20) SpawnEnemy(Enemy[0], -30, 10, -40);
        }

        yield return new WaitForSeconds(4f);
        SpawnEnemy(Enemy[0], 30, -10, -40);

        yield return new WaitForSeconds(2f);

        while (CheckEnemyExist())
        {
            yield return new WaitForSeconds(2f);
        }

        Debug.Log("Phase 4 Clear");

        StartCoroutine(StagePhase5());
    }

    protected IEnumerator StagePhase5()
    {
        SpawnEnemy(Enemy[0], -30, -10, -50);
        yield return new WaitForSeconds(0.7f);
        SpawnEnemy(Enemy[0], 30, 15, -50);
        yield return new WaitForSeconds(2f);
        SpawnEnemy(Enemy[0], 25, -15, -50);
        yield return new WaitForSeconds(0.7f);
        SpawnEnemy(Enemy[0], -25, 15, -50);
        yield return new WaitForSeconds(2f);
        SpawnEnemy(Enemy[1], -30, 0, -55);
        yield return new WaitForSeconds(0.7f);
        SpawnEnemy(Enemy[1], 30, 0, -45);


        while (CheckEnemyExist())
        {
            yield return new WaitForSeconds(2f);
        }

        Debug.Log("Phase 5 Clear");

        if (BGM_Script != null) BGM_Script.StopBGM(true);
        yield return new WaitForSeconds(3f);

        SpawnBoss(Boss, 0, 0, -50);
        SetBGM(9);
    }
}

