using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2 : Stage
{
    // Start is called before the first frame update
    protected override void Start()
    {
        text = $"제 2장\n흰 머리의 악인은 보물이 있는 곳을\n알고 있다는 이형의 동물들을 찾아가서\n그들의 보금자리를 헤집기 시작했습니다.";
        base.Start();
        NextStageName = "Stage3";
        currentStage = 2;
        SetBGM(3);

    }

    // Update is called once per frame
    /*
    protected override void Update()
    {
        
    }
    */
    protected override IEnumerator StagePhase1()
    {
        //0: 방개B, 1:물개A, 2:물개C, 3:펭귄A, 4:펭귄B, 5:펭귄C, 6: 방개 A

        Debug.Log("Phase 1 Started");
        yield return new WaitForSeconds(6f);


        for (int i = 5; i >= -30; i -= 5)
        {
            SpawnEnemy(Enemy[0], -80, i, -50, true);
            yield return new WaitForSeconds(0.3f);
        }

        yield return new WaitForSeconds(5f);


        for (int i = 5; i >= -20; i -= 5)
        {
            SpawnEnemy(Enemy[4], 80, i, -50 - i, true);
            yield return new WaitForSeconds(0.3f);
        }

        yield return new WaitForSeconds(6f);


        for (int i = -20; i <= 10; i += 10)
        {
            SpawnEnemy(Enemy[0], 80, i, -50, true);
            SpawnEnemy(Enemy[0], -80, i, -50, true);
            yield return new WaitForSeconds(0.3f);
            SpawnEnemy(Enemy[4], 80, i + 5, -50, true);
            SpawnEnemy(Enemy[4], -80, i + 5, -50, true);
            yield return new WaitForSeconds(0.3f);
        }

        while (CheckEnemyExist())
        {
            yield return new WaitForSeconds(2f);
        }
        Debug.Log("Phase 1 Clear");
        StartCoroutine(StagePhase2());
    }

    protected override IEnumerator StagePhase2()
    {


        yield return new WaitForSeconds(5f);

        SpawnEnemy(Enemy[1], -30, 0, -50);
        //SpawnEnemy(Enemy[1], -40, -15, -50);
        SpawnEnemy(Enemy[1], 25, 5, -45);
        //SpawnEnemy(Enemy[1], 15, 10, -55);

        //SpawnEnemy(Enemy[6], -30, 10, -50);
        SpawnEnemy(Enemy[6], -40, -5, -50);
        //SpawnEnemy(Enemy[6], 25, 15, -45);
        SpawnEnemy(Enemy[6], 15, 0, -55);

        SpawnEnemy(Enemy[5], -70, 0, -30);
        SpawnEnemy(Enemy[5], 70, 0, -30);




        while (CheckEnemyExist())
        {
            yield return new WaitForSeconds(2f);
        }
        Debug.Log("Phase 2 Clear");

        yield return new WaitForSeconds(2f);
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
        SpawnEnemy(Enemy[3], 15, 0, -20);
        yield return new WaitForSeconds(0.1f);
        SpawnEnemy(Enemy[3], -15, -10, -20);
        yield return new WaitForSeconds(0.1f);
        SpawnEnemy(Enemy[1], 30, -10, -30);
        yield return new WaitForSeconds(0.1f);
        SpawnEnemy(Enemy[3], 0, -10, -40);
        yield return new WaitForSeconds(0.1f);
        SpawnEnemy(Enemy[1], -30, -10, -30);
        yield return new WaitForSeconds(0.1f);
        SpawnEnemy(Enemy[2], -60, 10, -70);
        yield return new WaitForSeconds(0.1f);
        SpawnEnemy(Enemy[2], 60, 10, -70);

        while (CheckEnemyExist())
        {
            yield return new WaitForSeconds(2f);
        }
        Debug.Log("Phase 4 Clear");

        if (BGM_Script != null) BGM_Script.StopBGM(true);
        yield return new WaitForSeconds(3f);

        SpawnBoss(Boss, 0, 0, -50);
        SetBGM(4);
    }
}
