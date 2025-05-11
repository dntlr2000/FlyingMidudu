using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6 : Stage
{
    public SpaceCraft_st6 SpaceCraft;

    // Start is called before the first frame update
    protected override void Start()
    {
        text = $"최종장\n드디어 보물이 있는 곳에 도달했지만\n어째선지 악인의 앞을 가로막아\n온 힘을 짜내는 방개들..";
        base.Start();
        NextStageName = "EndScene";
        currentStage = 6;
        SetBGM(9);

    }

    protected override IEnumerator StagePhase1()
    {
        //0: 방개H(공격속도 빨라짐), 1:퍼그A, 2:퍼그B, 3:방개I(5:1), 4:방개J(B상위호환)

        Debug.Log("Phase 1 Started");

        while (CheckEnemyExist())
        {
            yield return new WaitForSeconds(2f);
        }
        Debug.Log("Phase 1 Clear");
        StartCoroutine(StagePhase2());
    }

    protected override IEnumerator StagePhase2()
    {

        while (CheckEnemyExist())
        {
            yield return new WaitForSeconds(2f);
        }
        Debug.Log("Phase 2 Clear");


        StartCoroutine(StagePhase3());
    }

    protected IEnumerator StagePhase3()
    {


        while (CheckEnemyExist("Enemy"))
        {
            yield return new WaitForSeconds(2f);
        }
        Debug.Log("Phase 3 Clear");



        StartCoroutine(StagePhase4());
    }

    protected IEnumerator StagePhase4()
    {


        while (CheckEnemyExist())
        {
            yield return new WaitForSeconds(2f);
        }

        Debug.Log("Phase 4 Clear");

        StartCoroutine(StagePhase5());
    }

    protected IEnumerator StagePhase5()
    {

        while (CheckEnemyExist())
        {
            yield return new WaitForSeconds(2f);
        }

        Debug.Log("Phase 5 Clear");

        if (BGM_Script != null) BGM_Script.StopBGM(true);
        yield return new WaitForSeconds(2f);

        //SpawnBoss(Boss, 0, 0, -900);
        SpawnMulbangae(Boss, 0, 0, -900);
        yield return new WaitForSeconds(1f);
        SpaceCraft.DoorSwitch();
        SpawnMulbangae(Boss, 0, 0, -900);

        yield return new WaitForSeconds(3f);
        MoveSpaceCraft();

        yield return new WaitForSeconds(7f);
        if (BGM_Script != null) BGM_Script.StopBGM(true);

        yield return new WaitForSeconds(4f);
        SetBGM(10);
    }

    void SpawnMulbangae(GameObject enemy, int x, int y, int z)
    {
        enemy.transform.position = new Vector3(x, y, z);
        enemy.SetActive(true);

    }

    void MoveSpaceCraft()
    {
        Mulbangae_Phase1 bangaeScript = Boss.GetComponent<Mulbangae_Phase1>();
        bangaeScript.ToOriginPos();
        SpaceCraft.MoveSpaceCraft();
    }
}

