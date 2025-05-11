using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mulbangae_Phase1 : Enemy_Boss
{
    [Header("프리팹 할당")]
    public GameObject MechaHand_L; // 왼손 프리팹
    public GameObject MechaHand_R; // 오른손 프리팹
    public GameObject HeavyMachinegun; //헤비머신건
    public GameObject HOS_Object; //히오스

    private GameObject LeftHand;
    private GameObject RightHand;

    private GameObject[] HeavyMachineguns;
    private HeavyMachinegun[] MachingunScripts;
    private HOS HOS_Script;



    protected override void Start()
    {
        Life = 9;
        Health = 100f;
        BossName = "물방개";
        BossDescription = "영원한 악연";
        animator = GetComponent<Animator>();
        //base.Start();
        //물방개 보스는 Start 메서드를 시간을 두고 실행시켜야 함
        StartCoroutine(StartFight());
    }

    public void ToOriginPos()
    {
        Vector3 newPose = transform.position;
        newPose.z = -60;
        StartCoroutine(ObjectMover(newPose, 6f));
    }

    IEnumerator StartFight()
    {
        yield return new WaitForSeconds(12f);
        animator.SetBool("ifStart", true);

        yield return new WaitForSeconds(4f);
        gameObject.tag = "EnemyBoss";
        base.Start();
        healthBar.SetName("Mulbangae");

        BossCollider.enabled = true;
        yield return null;

    }

    protected override void PhaseSetter(int remainLife)
    {
        base.PhaseSetter(remainLife);

        /*
        if (remainLife == 9)
        {
            Debug.Log($"Motion = {0}");
            StartCoroutine(skillMotion(0));
        }
        */

        if (remainLife == 8)
        {
            Debug.Log($"Motion = {1}");
            StartCoroutine(skillMotion(1));
        }
        else if (remainLife == 6)
        {
            StartCoroutine(skillMotion(2));
        }
        else if (remainLife == 4)
        {
            StartCoroutine(skillMotion(3));
        }
        else if (remainLife == 2)
        {
            StartCoroutine(skillMotion(4));
        }
        else if (remainLife == 1)
        {
            StartCoroutine(skillMotion(5));

        }

        else
        {
            StartCoroutine(skillMotion(0, 3f));
        }


        Debug.Log($"물방개의 남은 목숨: {Life}");

    }

    protected override IEnumerator Phase9() //패턴 1 : 통상
    {
        
        Health = 200f;
        TimerCoroutine = StartCoroutine(PhaseTimer(40));
        //MainCamera.SetActive(false);
        BossCollider.enabled = false;
        yield return new WaitForSeconds(2f);
        BossCollider.enabled = true;

        while (true)
        {
            for (int i = 0; i < 5; i++)
            {
                BasicAttack(200, 30, 2, playerCharacter, attackPrefab[0], 0, 133, 222);
                PlaySFX(4);
                yield return new WaitForSeconds(0.2f);
            }

            yield return new WaitForSeconds(0.5f);

            for (int i = 0; i < 10; i++)
            {
                PlaySFX(5);
                SlowdownAttack(transform.position + new Vector3(0, 5, 0), 100, 15, 3, playerCharacter, attackPrefab[3], 222, 81, 0, 5, 1f);
                SlowdownAttack(transform.position + new Vector3(0, -5, 0), 100, 15, 3, playerCharacter, attackPrefab[3], 222, 81, 0, 5, 1f);
                yield return new WaitForSeconds(0.2f);
            }

            yield return new WaitForSeconds(0.5f);

        }
    }

    protected override IEnumerator Phase8() //패턴 2 : 기술
    {
        Health = 200f;
        TimerCoroutine = StartCoroutine(PhaseTimer(60));
        //StartCoroutine(skillMotion(1));
        CutScene(3f);
        PlaySFX(2);
        SpellName = "아직은 더 고통받아야 해";
        SpellCard(SpellName);
        yield return new WaitForSeconds(4f);

        int xLoc;

        while (true)
        {
            for (int i = -70; i <= 70; i += 5)
            {
                for (int k = 0; k < 10; k++)
                {
                    xLoc = Random.Range(-50, 50);
                    ShootLasers(new Vector3(xLoc, -50, i), new Vector3(xLoc, 50, i), 1, attackPrefab[5], 1, 0, 133, 222);

                }
                PlaySFX(4);
                yield return new WaitForSeconds(0.1f);
            }

            AttackBlocks(attackPrefab[1], new Vector3(50, 50, -60), new Vector3(-50, 20, -60), 3f, 30);
            AttackBlocks(attackPrefab[1], new Vector3(50, -20, -60), new Vector3(-50, -50, -60), 3f, 30);
            
            yield return new WaitForSeconds(1f);

            for (int i = -70; i <= 70; i += 5)
            {
                for (int k = 0; k < 10; k++)
                {
                    xLoc = Random.Range(-50, 50);
                    ShootLasers(new Vector3(xLoc, -50, i), new Vector3(xLoc, 50, i), 1, attackPrefab[5], 1, 0, 133, 222);

                }
                PlaySFX(4);
                yield return new WaitForSeconds(0.1f);
            }

            AttackBlocks(attackPrefab[1], new Vector3(-20, 50, -60), new Vector3(-50, -50, -60), 3f, 30);
            AttackBlocks(attackPrefab[1], new Vector3(50, 50, -60), new Vector3(20, -50, -60), 3f, 30);
            yield return new WaitForSeconds(1f);
        }
    }

    protected override IEnumerator Phase7() //패턴 3 : 통상
    {
        Health = 200f;
        TimerCoroutine = StartCoroutine(PhaseTimer(30));
        //MainCamera.SetActive(false);
        yield return new WaitForSeconds(2f);

        ShootAround(playerCharacter, 120, attackPrefab[0], 30, 60, 0.3f, 0, 133, 222);
        PlaySFX(5);
        yield return new WaitForSeconds(1f);
        while (true)
        {
            ShootAround(playerCharacter, 120, attackPrefab[0], 30, 60, 0.3f, 0, 133, 222);
            PlaySFX(5);
            yield return new WaitForSeconds(0.2f);
            for (int i = 0; i < 3; i++)
            {
                SlowdownAttack(200, 90, 2f, playerCharacter, attackPrefab[4], 125, 125, 125, 0.2f, 0.3f);
                PlaySFX(4);
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    protected override IEnumerator Phase6() //패턴 4 : 기술
    {
        Health = 400f;
        TimerCoroutine = StartCoroutine(PhaseTimer(60));

        CutScene(3f);
        PlaySFX(2);
        SpellName = "웁하웁하웁하웁하웁하웁하";
        SpellCard(SpellName);
        yield return new WaitForSeconds(2f);
        LeftHand = SpawnEnemy(MechaHand_L, -15, -5, -60);
        RightHand = SpawnEnemy(MechaHand_R, 15, -5, -60);

        MechaHand LeftHandScript = LeftHand.GetComponent<MechahandParent>().returnChild();
        MechaHand RightHandScript = RightHand.GetComponent<MechahandParent>().returnChild();

        yield return new WaitForSeconds(2f);

        while (Health > 200f)
        {
            LeftHandScript.Punch(3, 0.5f, 0.5f, 5);
            yield return new WaitForSeconds(1f);
            RightHandScript.Punch(3, 0.5f, 0.5f, 5);

            yield return new WaitForSeconds(7f);
        }

        while (true)
        {
            LeftHandScript.Punch(5, 0.3f, 0.3f, 3);
            yield return new WaitForSeconds(0.5f);
            RightHandScript.Punch(5, 0.3f, 0.3f, 3);

            yield return new WaitForSeconds(8f);
        }

    }
    protected override IEnumerator Phase5() //패턴 5 : 통상
    {
        ClearHand();

        Health = 200f;
        TimerCoroutine = StartCoroutine(PhaseTimer(30));
        //MainCamera.SetActive(false);
        yield return new WaitForSeconds(2f);
    }

    protected override IEnumerator Phase4() //패턴 6 : 기술
    {
        Health = 400f;
        TimerCoroutine = StartCoroutine(PhaseTimer(60));

        CutScene(3f);
        PlaySFX(2);
        SpellName = "HeavyMachinegun@@@@@@@@Reloading";
        SpellCard(SpellName);

        HeavyMachineguns = new GameObject[6];

        yield return new WaitForSeconds(0.2f);
        HeavyMachineguns[0] = SpawnEnemy(HeavyMachinegun, -30, -20, -60, "down");
        yield return new WaitForSeconds(0.5f);
        HeavyMachineguns[1] = SpawnEnemy(HeavyMachinegun, 30, -20, -60, "down");
        yield return new WaitForSeconds(0.5f);
        HeavyMachineguns[2] = SpawnEnemy(HeavyMachinegun, 30, 20, -60, "upward");
        yield return new WaitForSeconds(0.5f);
        HeavyMachineguns[3] = SpawnEnemy(HeavyMachinegun, -30, 20, -60, "upward");

        MachingunScripts = new HeavyMachinegun[6];
        for (int i = 0; i < 4; i++)
        {
            MachingunScripts[i] = HeavyMachineguns[i].GetComponent<HeavyMachingunParent>().returnChild();
        }

        yield return new WaitForSeconds(2f);



        while (Health > 200)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    //SingleShot(HeavyMachineguns[k], 80, attackPrefab[1], playerCharacter, 200, 0, 0);
                    MachingunScripts[k].ShootSmall();
                }
                yield return new WaitForSeconds(0.1f);
                PlaySFX(4);
            }

            for (int i = 0; i < 50; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    //SingleShot(HeavyMachineguns[k], 80, attackPrefab[1], playerCharacter, 200, 0, 0);
                    MachingunScripts[k].ShootSmall();
                }

                if (i % 10 == 0)
                {
                    BasicSpin(150, 50, attackPrefab[2], 30, 0, 0, 200);
                    PlaySFX(5);
                }
                else PlaySFX(4);

                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(1f);
        }

        HeavyMachineguns[4] = SpawnEnemy(HeavyMachinegun, -30, -20, 60, "down");
        HeavyMachineguns[5] = SpawnEnemy(HeavyMachinegun, 30, -20, 60, "down");

        for (int i = 4; i < 6; i++)
        {
            MachingunScripts[i] = HeavyMachineguns[i].GetComponent<HeavyMachingunParent>().returnChild();
            if (MachingunScripts[i] == null) Debug.LogWarning($"No MachingunScripts in index{i}");
        }

        yield return new WaitForSeconds(1f);

        while (true)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int k = 0; k < 6; k++)
                {
                    //SingleShot(HeavyMachineguns[k], 80, attackPrefab[1], playerCharacter, 200, 0, 0);
                    MachingunScripts[k].ShootSmall();
                }
                yield return new WaitForSeconds(0.1f);
                PlaySFX(4);
            }

            for (int i = 0; i < 80; i++)
            {
                for (int k = 0; k < 6; k++)
                {
                    //SingleShot(HeavyMachineguns[k], 80, attackPrefab[1], playerCharacter, 200, 0, 0);
                    MachingunScripts[k].ShootSmall();
                }

                if (i % 10 == 0)
                {
                    BasicSpin(200, 50, attackPrefab[2], 30, 0, 0, 200);
                    PlaySFX(5);
                }
                else PlaySFX(4);

                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(1f);
        }
    }
    protected override IEnumerator Phase3() //패턴 7 : 통상
    {
        ClearMachinegun();
        Health = 200f;
        TimerCoroutine = StartCoroutine(PhaseTimer(30));
        //MainCamera.SetActive(false);
        yield return new WaitForSeconds(2f);
    }

    protected override IEnumerator Phase2() //패턴 8 : 기술
    {
        Health = 200f;
        TimerCoroutine = StartCoroutine(PhaseTimer(60));

        CutScene(4f);
        PlaySFX(2);
        yield return new WaitForSeconds(2f);
        SpellName = "시공의 폭풍 속으로";
        SpellCard(SpellName);
        HOS_Object.SetActive(true);
        HOS_Script = HOS_Object.GetComponent<HOS>();
        HOS_Script.PullPlayer(2f);
        yield return new WaitForSeconds(3f);

        while (true)
        {
            for (int i = 0; i < 10; i++)
            {
                ShootAround(playerCharacter, 15, attackPrefab[0], 10, 70, 0.2f ,0, 133, 222);
                yield return new WaitForSeconds(0.1f);
                PlaySFX(4);
            }

            for (int i = 0; i < 5; i++)
            {
                SlowdownAttack(150, 60, 2, playerCharacter, attackPrefab[1], 125, 125, 125, 0.2f, 0.5f);
                yield return new WaitForSeconds(0.2f);
                PlaySFX(5);
            }

        }

    }

    protected override IEnumerator Phase1() //패턴 9 : 기술
    {
        HOS_Script.ShutDown();
        Health = 200f;
        TimerCoroutine = StartCoroutine(PhaseTimer(60));

        CutScene(4f);
        PlaySFX(2);
        yield return new WaitForSeconds(2f);
        SpellName = "최종 비기 - 재도전 기원 1일차";
        SpellCard(SpellName);
        yield return new WaitForSeconds(2f);
    }

    private void ClearHand()
    {
        if (LeftHand != null) Destroy(LeftHand);
        if (RightHand != null) Destroy(RightHand);
        LeftHand = null; RightHand = null;
    }

    private void ClearMachinegun()
    {
        foreach (GameObject item in HeavyMachineguns)
            {
                if (item != null) Destroy(item);
            }
    }

    protected GameObject SpawnEnemy(GameObject enemy, int x, int y, int z, string direction)
    {
        GameObject spawnedEnemy;

        if (direction == "upward")
        {
            Vector3 spawnPosition = new Vector3(x, y + 50, z);
            spawnedEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
            StartCoroutine(ObjectMover(spawnedEnemy, spawnPosition, new Vector3(x, y, z), 1f));
        }

        else if (direction == "down")
        {
            Vector3 spawnPosition = new Vector3(x, y - 50, z);
            spawnedEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
            StartCoroutine(ObjectMover(spawnedEnemy, spawnPosition, new Vector3(x, y, z), 1f));
        }

        else
        {
            Vector3 spawnPosition = new Vector3(x, y, z - 50);
            spawnedEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
            StartCoroutine(ObjectMover(spawnedEnemy, spawnPosition, new Vector3(x, y, z), 2f));
        }
        return spawnedEnemy;
    }


}
