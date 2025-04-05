using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5 : Stage
{
    // Start is called before the first frame update
    protected override void Start()
    {
        text = $"제 5장\n날개 옷을 얻어 높은 곳으로\n날아오른 흰 머리 악인.\n하지만 보물에 관심을 가진 자는\n흰 머리 악인만이 아니었습니다.";
        base.Start();
        NextStageName = "Stage6";
        currentStage = 5;
        SetBGM(7);

    }

    protected override IEnumerator StagePhase1()
    {
        //0: 방개D(금손), 1:방개E(십덕), 2:방개F(십덕), 3:방개G(웁똥딸), 4:방개B

        Debug.Log("Phase 1 Started");
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

       

        while (CheckEnemyExist())
        {
            yield return new WaitForSeconds(2f);
        }
        Debug.Log("Phase 2 Clear");

        StartCoroutine(StagePhase4());
    }

    protected IEnumerator StagePhase4()
    {
     


        while (CheckEnemyExist())
        {
            yield return new WaitForSeconds(2f);
        }

        Debug.Log("Phase 4 Clear");

        if (BGM_Script != null) BGM_Script.StopBGM(true);
        yield return new WaitForSeconds(3f);

        SpawnBoss(Boss, 0, 0, -50);
        SetBGM(8);
    }
}
