using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mulbangae_Phase2 : Enemy_Boss
{
    public GameObject GudokBadge;
    protected override void Start()
    {
        Life = 5;
        Health = 100f;
        BossName = "물방개의 진심";
        BossDescription = "보물상자의 정체가 뭐길래 그리 애쓰는가";
        animator = GetComponent<Animator>();
        gameObject.tag = "EnemyBoss";

        StartCoroutine(StartFight());
    }

    IEnumerator StartFight()
    {
        playerCharacter = FindPlayer();
        //ResetProjectile(); //등장 시 적 탄막 모두 삭제
        

        animator = GetComponent<Animator>();
        BossCollider = GetComponent<Collider>();

        mainCameraController = MainCamera.GetComponent<MainCameraController>();

        BGM_Script = FindObjectOfType<BGMController>();

        if (MyCamera != null) mainCameraController.camera_e = MyCamera.transform;

        yield return new WaitForSeconds(1f);

        CutScene(7f);

        yield return new WaitForSeconds(4.5f);
        GudokBadge.SetActive(true);

        yield return new WaitForSeconds(2.5f);
        gameObject.tag = "EnemyBoss";
        HealthBarObject.SetActive(true);
        healthBar = HealthBarObject.GetComponent<HealthBar>();
        healthBar.SetName("Sincerity of Mulbangae");


        
        if (BossText != null)
        {
            BossText.gameObject.SetActive(true);
            StartCoroutine(BossText.BossInformation(BossDescription, BossName));
        }

        PhaseSetter(Life);
        activatePointer(true);

        BossCollider.enabled = true;
        yield return null;
        yield return new WaitForSeconds(1f);
    }

    protected override void PhaseSetter(int remainLife)
    {
        base.PhaseSetter(remainLife);

        if (remainLife == 4)
        {
            Debug.Log($"Motion = {1}");
            StartCoroutine(skillMotion(1));
        }
        else if (remainLife == 3)
        {
            StartCoroutine(skillMotion(2));
        }
        else if (remainLife == 2)
        {
            StartCoroutine(skillMotion(3));
        }
        else if (remainLife == 1)
        {
            StartCoroutine(skillMotion(4));
        }

        else
        {
            StartCoroutine(skillMotion(0, 3f));
        }


        Debug.Log($"물방개의 남은 목숨: {Life}");

    }

    protected override IEnumerator Phase5() //패턴 1 : 통상
    {

        Health = 1600f;
        TimerCoroutine = StartCoroutine(PhaseTimer(40));
        //MainCamera.SetActive(false);
        BossCollider.enabled = false;
        yield return new WaitForSeconds(2f);
        BossCollider.enabled = true;

        while (true)
        {

            yield return new WaitForSeconds(0.5f);

        }
    }

    protected override IEnumerator Phase4() //패턴 2 : 기술
    {

        Health = 1200f;
        TimerCoroutine = StartCoroutine(PhaseTimer(40));
        //MainCamera.SetActive(false);
        yield return new WaitForSeconds(2f);

        CutScene(3f);
        PlaySFX(2);
        SpellName = "HeavyMachinegun@@@@@@@@Reloading";
        SpellCard(SpellName);

        while (true)
        {
           
            yield return new WaitForSeconds(1f);
        }
    }

    protected override IEnumerator Phase3() //패턴 3 : 기술
    {
        CutScene(3f);
        PlaySFX(2);
        SpellName = "HeavyMachinegun@@@@@@@@Reloading";
        SpellCard(SpellName);

        Health = 1200f;
        TimerCoroutine = StartCoroutine(PhaseTimer(40));
        //MainCamera.SetActive(false);
        yield return new WaitForSeconds(2f);

        while (true)
        {

            yield return new WaitForSeconds(1f);
        }
    }

    protected override IEnumerator Phase2() //패턴 4 : 기술
    {
        CutScene(3f);
        PlaySFX(2);
        SpellName = "HeavyMachinegun@@@@@@@@Reloading";
        SpellCard(SpellName);

        Health = 1200f;
        TimerCoroutine = StartCoroutine(PhaseTimer(40));
        //MainCamera.SetActive(false);
        yield return new WaitForSeconds(2f);

        while (true)
        {

            yield return new WaitForSeconds(1f);
        }
    }

    protected override IEnumerator Phase1() //패턴 5 : 기술
    {
        CutScene(3f);
        PlaySFX(2);
        SpellName = "HeavyMachinegun@@@@@@@@Reloading";
        SpellCard(SpellName);

        Health = 1200f;
        TimerCoroutine = StartCoroutine(PhaseTimer(40));
        //MainCamera.SetActive(false);
        yield return new WaitForSeconds(2f);

        while (true)
        {

            yield return new WaitForSeconds(1f);
        }
    }
}
