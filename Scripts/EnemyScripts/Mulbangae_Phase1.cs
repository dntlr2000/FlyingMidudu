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
        StartCoroutine(skillMotion(0, 3f));
        Health = 200f;
        TimerCoroutine = StartCoroutine(PhaseTimer(40));
        //MainCamera.SetActive(false);
        yield return new WaitForSeconds(2f);
    }

    protected override IEnumerator Phase8() //패턴 2 : 기술
    {
        Health = 200f;
        TimerCoroutine = StartCoroutine(PhaseTimer(60));
        //StartCoroutine(skillMotion(1));
        CutScene(3f);
        PlaySFX(2);
        SpellName = "고통받는 웁님이 보고 싶다.";
        SpellCard(SpellName);
        yield return new WaitForSeconds(2f);
    }

    protected override IEnumerator Phase7() //패턴 3 : 통상
    {
        Health = 200f;
        TimerCoroutine = StartCoroutine(PhaseTimer(30));
        //MainCamera.SetActive(false);
        yield return new WaitForSeconds(2f);
    }

    protected override IEnumerator Phase6() //패턴 4 : 기술
    {
        Health = 200f;
        TimerCoroutine = StartCoroutine(PhaseTimer(60));

        CutScene(3f);
        PlaySFX(2);
        SpellName = "웁하웁하웁하웁하웁하웁하";
        SpellCard(SpellName);
        yield return new WaitForSeconds(2f);
        LeftHand = SpawnEnemy(MechaHand_L, -15, -5, -60);
        RightHand = SpawnEnemy(MechaHand_R, 15, -5, -60);
        

        yield return new WaitForSeconds(1f);

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
        Health = 200f;
        TimerCoroutine = StartCoroutine(PhaseTimer(60));

        CutScene(3f);
        PlaySFX(2);
        SpellName = "HeavyMachinegun@@@@@@@@Reloading";
        SpellCard(SpellName);

        HeavyMachineguns = new GameObject[6];

        yield return new WaitForSeconds(0.5f);
        HeavyMachineguns[0] = SpawnEnemy(HeavyMachinegun, -30, -20, -60);
        yield return new WaitForSeconds(0.5f);
        HeavyMachineguns[1] = SpawnEnemy(HeavyMachinegun, 30, -20, -60);
        yield return new WaitForSeconds(0.5f);
        HeavyMachineguns[2] = SpawnEnemy(HeavyMachinegun, 30, 20, -60);
        yield return new WaitForSeconds(0.5f);
        HeavyMachineguns[3] = SpawnEnemy(HeavyMachinegun, -30, 20, -60);

        yield return new WaitForSeconds(2f);
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


        yield return new WaitForSeconds(2f);
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
}
