using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3 : Stage
{
    // Start is called before the first frame update
    protected override void Start()
    {
        text = $"제 3장\n세상과 격리된 장소, 무릉에 내려온다는\n흰 머리의 선녀로부터 날개옷을 구해야 한다는\n정보를 입수한 흰 머리 악인은 선녀를\n 사모하는 방개들에게 둘러싸였습니다.";
        base.Start();
        NextStageName = "Stage3";
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
        //0: 방개D(금손), 1:방개E(십덕), 2:방개F(십덕), 3:방개G(웁똥딸)

        Debug.Log("Phase 1 Started");
        yield return new WaitForSeconds(6f);

        SpawnEnemy(Enemy[0], 30, 0, -40);
        yield return new WaitForSeconds(0.3f);
        SpawnEnemy(Enemy[3], -20, 5, -40);  
        yield return new WaitForSeconds(0.3f);
        SpawnEnemy(Enemy[3], -25, -5, -40);
        yield return new WaitForSeconds(0.3f);
        SpawnEnemy(Enemy[3], -30, 0, -40);
        yield return new WaitForSeconds(0.3f);
        SpawnEnemy(Enemy[3], -25, 5, -40);
        yield return new WaitForSeconds(9f);

        SpawnEnemy(Enemy[0], -30, 0, -40);
        yield return new WaitForSeconds(0.3f);
        SpawnEnemy(Enemy[3], 20, 5, -40);
        yield return new WaitForSeconds(0.3f);
        SpawnEnemy(Enemy[3], 25, -5, -40);
        yield return new WaitForSeconds(0.3f);
        SpawnEnemy(Enemy[3], 30, 0, -40);
        yield return new WaitForSeconds(0.3f);
        SpawnEnemy(Enemy[3], 25, 5, -40);
        yield return new WaitForSeconds(9f);


        SpawnEnemy(Enemy[0], 30, 0, -40);
        yield return new WaitForSeconds(0.2f);
        SpawnEnemy(Enemy[0], 30, 30, -50);
        yield return new WaitForSeconds(0.2f);
        SpawnEnemy(Enemy[0], -30, -30, -50);
        yield return new WaitForSeconds(0.2f);
        SpawnEnemy(Enemy[0], -30, 30, -50);
        yield return new WaitForSeconds(0.2f);


        


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

        yield return new WaitForSeconds(2f);
        StartCoroutine(StagePhase3());
    }

    protected IEnumerator StagePhase3()
    {
        

        while (CheckEnemyExist())
        {
            yield return new WaitForSeconds(2f);
        }
        Debug.Log("Phase 3 Clear");


        StartCoroutine(StagePhase4());

    }

    protected IEnumerator StagePhase4()
    {
        SpawnEnemy(Enemy[3], 15, 0, -20);
        SpawnEnemy(Enemy[3], -15, -10, -20);
        SpawnEnemy(Enemy[1], 30, -10, -30);
        SpawnEnemy(Enemy[3], 0, -10, -40);
        SpawnEnemy(Enemy[1], -30, -10, -30);
        SpawnEnemy(Enemy[2], -60, 10, -70);
        SpawnEnemy(Enemy[2], 60, 10, -70);

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
