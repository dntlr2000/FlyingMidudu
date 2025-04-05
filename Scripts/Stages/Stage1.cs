using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : Stage
{
    // Start is called before the first frame update
    protected override void Start()
    {
        text = $"제 1장\n저 높은 곳에 보물이 있다는 소문을 들은 흰 머리\n 악인의 수하들은 그들의 두목이 자신들을 두고\n 떠나는 것을 막기 위해 가로막았습니다.";
        base.Start();
        NextStageName = "Stage2";
        currentStage = 1;
        SetBGM(1);
        
    }


    protected override IEnumerator StagePhase1()
    {
        Debug.Log("Phase 1 Started");
        yield return new WaitForSeconds(6f);

        
        SpawnEnemy(Enemy[0], -30, 0, -50);
        yield return new WaitForSeconds(1f);
        SpawnEnemy(Enemy[0], -30, 0, -50);
        yield return new WaitForSeconds(1f);
        SpawnEnemy(Enemy[0], -20, -5, -50);
        yield return new WaitForSeconds(1f);
        SpawnEnemy(Enemy[0], -25, 0, -55);
        yield return new WaitForSeconds(2f);

        SpawnEnemy(Enemy[0], 30, 0, -50);
        yield return new WaitForSeconds(1f);
        SpawnEnemy(Enemy[0], 25, 5, -45);
        yield return new WaitForSeconds(1f);
        SpawnEnemy(Enemy[0], 20, -5, -50);
        yield return new WaitForSeconds(1f);
        SpawnEnemy(Enemy[0], 25, 0, -55);
        yield return new WaitForSeconds(1f);


        while (CheckEnemyExist())
        {
            yield return new WaitForSeconds(2f);
        }
        Debug.Log("Phase 1 Clear");
        StartCoroutine(StagePhase2());
    }

    protected override IEnumerator StagePhase2()
    {

        for (int i = 5; i >= - 30; i -= 5)
        {
            SpawnEnemy(Enemy[1], -80, i, -30, true);
            yield return new WaitForSeconds(0.3f);
        }

        yield return new WaitForSeconds(10f);

        SpawnEnemy(Enemy[0], -30, 0, -50);
        //SpawnEnemy(Enemy[0], -30, 0, -50);
        SpawnEnemy(Enemy[0], -20, -5, -50);
        //SpawnEnemy(Enemy[0], -25, 0, -55);

        //SpawnEnemy(Enemy[0], 30, 0, -50);
        SpawnEnemy(Enemy[0], 25, 5, -45);
        //SpawnEnemy(Enemy[0], 20, -5, -50);
        SpawnEnemy(Enemy[0], 25, 0, -55);


        for (int i = 5; i >= -30; i -= 5)
        {
            SpawnEnemy(Enemy[1], 80, i, -30, true);
            yield return new WaitForSeconds(0.3f);
        }



        while (CheckEnemyExist())
        {
            yield return new WaitForSeconds(2f);
        }
        Debug.Log("Phase 2 Clear");

        yield return new WaitForSeconds(3f);
        StartCoroutine(StagePhase3());
    }

    protected IEnumerator StagePhase3()
    {
        SpawnEnemy(Enemy[2], 0, 0, -30);
        SpawnEnemy(Enemy[3], -30, -10, -30);
        SpawnEnemy(Enemy[3], 30, -10, -30);

        while (CheckEnemyExist())
        {
            yield return new WaitForSeconds(2f);
        }
        Debug.Log("Phase 3 Clear");

        if (BGM_Script != null) BGM_Script.StopBGM(true);
        yield return new WaitForSeconds(3f);

        SpawnBoss(Boss, 0, 0, -50);
        SetBGM(2);
        
    }
}
