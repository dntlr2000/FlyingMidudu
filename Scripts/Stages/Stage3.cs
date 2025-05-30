using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3 : Stage
{
    // Start is called before the first frame update
    protected override void Start()
    {
        text = $"제 3장\n세상과 격리된 어느 구역에만 나타난다는\n하얀 선녀로부터 날개옷을 구해야 한다는\n정보를 입수한 흰 머리 악인은 선녀를\n 사모하는 방개들에게 둘러싸였습니다.";
        base.Start();
        NextStageName = "Stage5";
        currentStage = 3;
        SetBGM(5);

    }

    // Update is called once per frame
    /*
    protected override void Update()
    {
        
    }
    */
    protected override IEnumerator StagePhase1()
    {
        //0: 방개D(금손), 1:방개E(십덕), 2:방개F(십덕), 3:방개G(웁똥딸), 4:방개B

        Debug.Log("Phase 1 Started");
        yield return new WaitForSeconds(6f);

        SpawnEnemy(Enemy[0], 30, 0, -50);
        yield return new WaitForSeconds(0.3f);
        SpawnEnemy(Enemy[3], -20, 5, -50);  
        yield return new WaitForSeconds(0.3f);
        SpawnEnemy(Enemy[3], -25, -5, -50);
        yield return new WaitForSeconds(0.3f);
        SpawnEnemy(Enemy[3], -30, 0, -50);
        yield return new WaitForSeconds(0.3f);
        SpawnEnemy(Enemy[3], -25, 5, -50);

        yield return new WaitForSeconds(4f);
        SpawnEnemy(Enemy[0], -30, 0, -50);
        yield return new WaitForSeconds(0.3f);
        SpawnEnemy(Enemy[3], 20, 5, -50);
        yield return new WaitForSeconds(0.3f);
        SpawnEnemy(Enemy[3], 25, -5, -50);
        yield return new WaitForSeconds(0.3f);
        SpawnEnemy(Enemy[3], 30, 0, -50);
        yield return new WaitForSeconds(0.3f);
        SpawnEnemy(Enemy[3], 25, 5, -50);
        yield return new WaitForSeconds(9f);

        /*
        SpawnEnemy(Enemy[0], 30, 30, -50);
        yield return new WaitForSeconds(0.2f);
        SpawnEnemy(Enemy[0], 30, 30, -50);
        yield return new WaitForSeconds(0.2f);
        SpawnEnemy(Enemy[0], -30, -30, -50);
        yield return new WaitForSeconds(0.2f);
        SpawnEnemy(Enemy[0], -30, 30, -50);
        yield return new WaitForSeconds(9f);
        */

        while (CheckEnemyExist())
        {
            yield return new WaitForSeconds(2f);
        }
        Debug.Log("Phase 1 Clear");
        StartCoroutine(StagePhase2());
    }

    protected override IEnumerator StagePhase2()
    {

        SpawnBoss(MidBoss, 0, 0, -50);

        while (CheckEnemyExist("EnemyBoss"))
        {
            yield return new WaitForSeconds(2f);
        }
        Debug.Log("Phase 2 Clear");

        StartCoroutine(StagePhase3());
    }

    protected IEnumerator StagePhase3()
    {

        SpawnEnemy(Enemy[1], -30, 0, -40);
        yield return new WaitForSeconds(0.3f);
        SpawnEnemy(Enemy[0], 20, 5, -50);
        yield return new WaitForSeconds(0.3f);
        SpawnEnemy(Enemy[0], 25, -5, -50);
        yield return new WaitForSeconds(0.3f);
        SpawnEnemy(Enemy[0], 30, 0, -50);
        yield return new WaitForSeconds(0.3f);
        SpawnEnemy(Enemy[0], 25, 5, -50);
        yield return new WaitForSeconds(12f);
        

        for (int i = 0; i < 4;i++)
        {
            SpawnEnemy(Enemy[3], 20 - i * 10, 5, -50);
            yield return new WaitForSeconds(0.3f);
            SpawnEnemy(Enemy[3], 25 - i * 10, -5, -50);
            yield return new WaitForSeconds(0.3f);
            SpawnEnemy(Enemy[3], 30 - i * 10, 0, -50);
            yield return new WaitForSeconds(0.3f);
            SpawnEnemy(Enemy[3], 25 - i * 10, 5, -50);
            yield return new WaitForSeconds(0.3f);
            if (i == 1) SpawnEnemy(Enemy[1], -20, 0, -50);
            if (i == 3) SpawnEnemy(Enemy[1], 20, 0, -50);
        }



        while (CheckEnemyExist())
        {
            yield return new WaitForSeconds(2f);
        }
        Debug.Log("Phase 2 Clear");

        StartCoroutine(StagePhase4());
    }

    protected IEnumerator StagePhase4()
    {
        SpawnEnemy(Enemy[2], 0, 10, -50);
        //SpawnEnemy(Enemy[2], 0, -10, -50);


        yield return new WaitForSeconds(0.2f);
        SpawnEnemy(Enemy[0], 30, 10, -40);
        yield return new WaitForSeconds(0.2f);
        SpawnEnemy(Enemy[0], -30, -10, -40);
        yield return new WaitForSeconds(4f);

        SpawnEnemy(Enemy[0], -30, 10, -40);
        yield return new WaitForSeconds(0.2f);
        SpawnEnemy(Enemy[0], 30, -10, -40);
        yield return new WaitForSeconds(0.2f);



        while (CheckEnemyExist())
        {
            yield return new WaitForSeconds(2f);
        }
        Debug.Log("Phase 4 Clear");

        if (BGM_Script != null) BGM_Script.StopBGM(true);
        yield return new WaitForSeconds(3f);

        SpawnBoss(Boss, 0, 0, -50);
        SetBGM(6);
    }
}
