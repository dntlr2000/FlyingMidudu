using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mulbangae_Phase2 : Enemy_Boss
{
    public GameObject GudokBadge;
    private IEnumerator finalPhase;
    protected override void Start()
    {
        Life = 5;
        Health = 100f;
        BossName = "물방개의 진심";
        BossDescription = "대체 그를 이렇게까지 막는 이유는 무엇인가";
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
        BGM_Script.ChangeBGM(12);
        BGM_Script.PlayBGM();
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

        Health = 1200f;
        TimerCoroutine = StartCoroutine(PhaseTimer(40));
        //MainCamera.SetActive(false);
        BossCollider.enabled = false;
        yield return new WaitForSeconds(1f);
        BossCollider.enabled = true;

        while (true)
        {
            for (int i = 0; i < 5; i++)
            {
                BasicAttack(transform.position + new Vector3(-10, 0, 0), 100, 70f, 4, playerCharacter, attackPrefab[2], 200, 0, 0);
                PlaySFX(5);
                yield return new WaitForSeconds(0.5f);
                BasicAttack(transform.position + new Vector3(10, 0, 0), 100, 70f, 4, playerCharacter, attackPrefab[2], 0, 0, 200);
                PlaySFX(5);
                yield return new WaitForSeconds(0.5f);
            }

            yield return new WaitForSeconds(0.5f);

            for (int i = -15; i <= 15; i+= 3)
            {
                SlowdownAttack(transform.position + new Vector3(i, i, 0), 40, 10f, 3, playerCharacter.transform.position + new Vector3(i, i, 0), attackPrefab[3], 100, 0, 0, 8f, 1f);
                PlaySFX(4);
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(0.5f);

            for (int i = -15; i <= 15; i += 3)
            {
                SlowdownAttack(transform.position + new Vector3(-i, i, 0), 40, 10f, 3, playerCharacter.transform.position + new Vector3(-i, i, 0), attackPrefab[3], 0, 0, 100, 8f, 1f);
                PlaySFX(4);
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    protected override IEnumerator Phase4() //패턴 2 : 기술
    {

        Health = 1400f;
        TimerCoroutine = StartCoroutine(PhaseTimer(40));
        //MainCamera.SetActive(false);
        CutScene(2.5f);
        PlaySFX(2);
        SpellName = "하늘, 아크, 그타 3대 충의 압박";
        SpellCard(SpellName);
        AimingObject(playerCharacter);

        yield return new WaitForSeconds(1.6f);
        //StartCoroutine(divideBullets_1(gameObject.transform.position + new Vector3(0, 0, +50), attackPrefab[2], attackPrefab[0]));
        while (true)
        {
            StartCoroutine(divideBullets_1(playerCharacter.transform.position, attackPrefab[2], attackPrefab[3]));
            yield return new WaitForSeconds(2f);
            RandomMove(20, 1f);
            for (int i = 0; i < 5; i++)
            {
                ShootAround(playerCharacter, 50, attackPrefab[0], 30, 60, 0.2f, 184, 24, 24);
                PlaySFX(4);
                yield return new WaitForSeconds(0.2f);
            }
        }
    }

    protected override IEnumerator Phase3() //패턴 3 : 기술
    {
        CutScene(3f);
        PlaySFX(2);
        SpellName = "전략적 후퇴";
        SpellCard(SpellName);
        AimingObject(playerCharacter.gameObject, false);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        Health = 1600f;
        TimerCoroutine = StartCoroutine(PhaseTimer(60));
        //MainCamera.SetActive(false);
        yield return new WaitForSeconds(2f);
        StartCoroutine(ObjectMover(new Vector3(0, 70, -50), 1.5f));
        yield return new WaitForSeconds(2.5f);

        for (int i = 0; i < 5; i++)
        {
            AttackBlocks(attackPrefab[1], new Vector3(50, -60, 75), new Vector3(-50, -60, -75), new Vector3(0, 1, 0), 5, 40, 150, 0, 0);
            yield return new WaitForSeconds(0.4f);
        }
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 5; i++)
        {
            AttackBlocks(attackPrefab[1], new Vector3(52.5f, 50, -85f), new Vector3(-52.5f, -50, -85f), new Vector3(0, 0, 1), 5, 40, 0, 0, 150);
            yield return new WaitForSeconds(0.4f);
        }

        yield return new WaitForSeconds(3f);

        for (int i = 0; i < 10; i++)
        {
            if (i%2==0)
            {
                AttackBlocks(attackPrefab[1], new Vector3(50, -60, 75), new Vector3(-50, -60, -75), new Vector3(0, 1, 0), 5, 40, 150, 0, 0);
            }
            else
            {
                AttackBlocks(attackPrefab[1], new Vector3(52.5f, 50, -85f), new Vector3(-52.5f, -50, -85f), new Vector3(0, 0, 1), 5, 40, 0, 0, 150);
            }
            yield return new WaitForSeconds(0.6f);
        }

        yield return new WaitForSeconds(5f);
        ResetProjectile();

        for (int i = -75; i <= 75; i+=3)
        {
            for (int k = -50; k <= 50; k+=3)
            {
                SlowdownAttack(new Vector3(50, k, i), 1, 10, 1, new Vector3(0, k, -25 + i/2), attackPrefab[3], 74, 161, 137, 8f, 1f);
                SlowdownAttack(new Vector3(-50, k - 1.5f, -i), 1, 10, 1, new Vector3(0, k, + i/2), attackPrefab[3], 74, 161, 137, 8f, 1f);
            }
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(3f);

        for (int i = -75; i <= 75; i += 3)
        {
            for (int k = -50; k <= 50; k += 3)
            {
                SlowdownAttack(new Vector3(50, k, i), 1, 20, 1, new Vector3(0, k, -20 - i/2), attackPrefab[3], 74, 161, 137, 4f, 1f);
                SlowdownAttack(new Vector3(-50, k - 1.5f, -i), 1, 10, 1, new Vector3(0, k, 20 + i/2), attackPrefab[3], 74, 161, 137, 8f, 1f);
            }
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(3f);

        ResetProjectile();

        for (int i = 0; i < 3; i++)
        {
            BasicAttack(new Vector3(-50, 50, -75), 60, 50, 4, new Vector3(0, 0, 0), attackPrefab[1], 150, 0, 0);
            PlaySFX(4);
            yield return new WaitForSeconds(0.4f);
            BasicAttack(new Vector3(50, 50, -75), 60, 50, 4, new Vector3(0, 0, 0), attackPrefab[1], 150, 0, 0);
            PlaySFX(4);
            yield return new WaitForSeconds(0.4f);
            BasicAttack(new Vector3(50, -50, -75), 60, 50, 4, new Vector3(0, 0, 0), attackPrefab[1], 150, 0, 0);
            PlaySFX(4);
            yield return new WaitForSeconds(0.4f);
            BasicAttack(new Vector3(-50, -50, -75), 60, 50, 4, new Vector3(0, 0, 0), attackPrefab[1], 150, 0, 0);
            PlaySFX(4);
            yield return new WaitForSeconds(0.4f);

            BasicAttack(new Vector3(-50, -50, 75), 60, 50, 4, new Vector3(0, 0, 0), attackPrefab[1], 150, 0, 0);
            PlaySFX(4);
            yield return new WaitForSeconds(0.4f);
            BasicAttack(new Vector3(-50, 50, 75), 60, 50, 4, new Vector3(0, 0, 0), attackPrefab[1], 150, 0, 0);
            PlaySFX(4);
            yield return new WaitForSeconds(0.4f);
            BasicAttack(new Vector3(50, 50, 75), 60, 50, 4, new Vector3(0, 0, 0), attackPrefab[1], 150, 0, 0);
            PlaySFX(4);
            yield return new WaitForSeconds(0.4f);
            BasicAttack(new Vector3(50, -50, 75), 60, 50, 4, new Vector3(0, 0, 0), attackPrefab[1], 150, 0, 0);
            PlaySFX(4);
            yield return new WaitForSeconds(0.4f);
        }

        yield return new WaitForSeconds(3f);
        ResetProjectile();

        for (int i = -50; i <= 50; i+= 3)
        {
            for (int k = - 75; k <= 75; k += 3)
            {
                ShootLasers(new Vector3(i, -50, k), new Vector3(i, 50, k), 1, attackPrefab[5], 1, 102, 30, 150);
                
            }
            PlaySFX(5);
            yield return new WaitForSeconds(0.2f);
            if (i >= 45)
            {
                yield return new WaitForSeconds(4f);
            }
        }
        yield return new WaitForSeconds(1f);

    }

    protected override IEnumerator Phase2() //패턴 4 : 기술
    {
        StartCoroutine(ObjectMover(new Vector3(0, 0, -50), 1f));
        CutScene(2f);
        PlaySFX(2);
        SpellName = "화려한 조명이 수령님을 감싸네";
        SpellCard(SpellName);

        Health = 1800f;
        TimerCoroutine = StartCoroutine(PhaseTimer(40));
        //MainCamera.SetActive(false);
        yield return new WaitForSeconds(4f);

        while (true)
        {
            StartCoroutine(SpawnJailObject(playerCharacter.transform, 8, 40, 2f, attackPrefab[0]));
            yield return new WaitForSeconds(1f);

            StartCoroutine(SpawnJailObject(playerCharacter.transform, 8, 40, 2f, attackPrefab[0]));
            Vector3 TargetPosition = playerCharacter.transform.position;
            for (int i = 0; i < 5; i++)
            {
                BasicAttack(transform.position, 120, 50f, 6f, TargetPosition + new Vector3(-10 + i * 5, 0, 0), attackPrefab[0], 150, 0, 0);
                PlaySFX(4);
                yield return new WaitForSeconds(0.2f);
            }

            StartCoroutine(SpawnJailObject(playerCharacter.transform, 80, 40, 2f, attackPrefab[0]));

            TargetPosition = playerCharacter.transform.position;
            for (int i = 0; i < 5; i++)
            {
                BasicAttack(transform.position + new Vector3(0, -10, 0), 90, 50f, 4f, TargetPosition + new Vector3(0, 10 - i * 5, 0), attackPrefab[3], 0, 0, 150);
                BasicAttack(transform.position + new Vector3(0, +10, 0), 90, 50f, 4f, TargetPosition + new Vector3(0, -10 + i * 5, 0), attackPrefab[3], 0, 0, 150);
                PlaySFX(4);
                yield return new WaitForSeconds(0.2f);
            }

            RandomMove(10f, 1f);
        }
    }

    protected override IEnumerator Phase1() //패턴 5 : 기술
    {
        CutScene(8f);
        PlaySFX(2);
        
        GudokBadge.SetActive(false);


        Health = 3000f;
        TimerCoroutine = StartCoroutine(PhaseTimer(99));
        //MainCamera.SetActive(false);
        yield return new WaitForSeconds(6f);
        GatherEffect(transform.position);
        yield return new WaitForSeconds(1f);
        SpellName = "진짜진짜 마지막 발악";
        SpellCard(SpellName);
        GudokBadge.SetActive(true);
        //모든 탄막 유형 총출동
        yield return new WaitForSeconds(1f);

        while (Health > 2500 && Timer > 80)
        {
            SlowdownAttack(120, 70, 3, playerCharacter, attackPrefab[3], 125, 0, 0, 0.2f, 0.5f);
            PlaySFX(4);
            yield return new WaitForSeconds(1f);
        }

        while (Health > 2000 && Timer > 60)
        {
            SlowdownAttack(120, 70, 3, playerCharacter, attackPrefab[3], 125, 0, 0, 0.2f, 0.5f);
            PlaySFX(4);
            yield return new WaitForSeconds(0.5f);
            ShootAround(playerCharacter, 80, attackPrefab[0], 30, 40, 0.2f, 0, 125, 0);
            PlaySFX(4);
            yield return new WaitForSeconds(0.5f);
        }

        while (Health > 1500 && Timer > 40)
        {
            SlowdownAttack(120, 70, 3, playerCharacter, attackPrefab[3], 125, 0, 0, 0.2f, 0.5f);
            PlaySFX(4);
            yield return new WaitForSeconds(0.25f);
            BasicAttack(120, 60, 3, playerCharacter, attackPrefab[4], 0, 0, 125);
            PlaySFX(5);
            yield return new WaitForSeconds(0.25f);
            ShootAround(playerCharacter, 80, attackPrefab[0], 30, 40, 0.2f, 0, 125, 0);
            PlaySFX(4);
            yield return new WaitForSeconds(0.25f);
            //BasicSpin(100, 40f, attackPrefab[2], 50, 100, 100, 100);
            yield return new WaitForSeconds(0.25f);

        }

        while (Health > 1000 && Timer > 20)
        {
            SlowdownAttack(120, 70, 3, playerCharacter, attackPrefab[3], 125, 0, 0, 0.2f, 0.5f);
            //ShootLasers(playerCharacter, 5, attackPrefab[5], 184, 48, 191);
            PlaySFX(4);
            yield return new WaitForSeconds(0.25f);
            BasicAttack(120, 60, 3, playerCharacter, attackPrefab[4], 0, 0, 125);
            PlaySFX(5);
            yield return new WaitForSeconds(0.25f);
            ShootAround(playerCharacter, 80, attackPrefab[0], 30, 40, 0.2f, 0, 125, 0);
            PlaySFX(4);
            yield return new WaitForSeconds(0.25f);
            BasicSpin(100, 40f, attackPrefab[2], 30, 100, 100, 100);
            PlaySFX(5);
            yield return new WaitForSeconds(0.25f);

        }

        finalPhase = FinalPattern();
        StartCoroutine(finalPhase);
        while (true)
        {
            SlowdownAttack(150, 70, 2, playerCharacter, attackPrefab[3], 125, 0, 0, 0.2f, 0.5f);
            //ShootLasers(playerCharacter, 5, attackPrefab[5], 184, 48, 191);
            PlaySFX(4);
            yield return new WaitForSeconds(0.25f);
            BasicAttack(120, 60, 2, playerCharacter, attackPrefab[4], 0, 0, 125);
            PlaySFX(5);
            yield return new WaitForSeconds(0.25f);
            ShootAround(playerCharacter, 80, attackPrefab[0], 30, 40, 0.2f, 0, 125, 0);
            PlaySFX(4);
            yield return new WaitForSeconds(0.25f);
            BasicSpin(100, 40f, attackPrefab[2], 30, 100, 100, 100);
            //ShootLasers(playerCharacter, 3, attackPrefab[5], 30, 184, 48, 191);
            PlaySFX(5);
            yield return new WaitForSeconds(0.25f);

        }
    }

    

    protected IEnumerator divideBullets_1(Vector3 target, GameObject attackPrefab1, GameObject attackPrefab2)
    {
        float distance = 60f; // 원하는 거리
        float angleOffset = 25f; // 회전 각도

        // 1. 기본 방향
        Vector3 direction = (target - transform.position).normalized;

        // 2. 정면 방향 좌표
        Vector3 forwardPos = transform.position + direction * distance;

        // 3. 왼쪽으로 30도 회전한 방향 좌표
        Vector3 leftDir = Quaternion.AngleAxis(-angleOffset, Vector3.up) * direction;
        Vector3 leftPos = transform.position + leftDir * distance;

        // 4. 오른쪽으로 30도 회전한 방향 좌표
        Vector3 rightDir = Quaternion.AngleAxis(angleOffset, Vector3.up) * direction;
        Vector3 rightPos = transform.position + rightDir * distance;

        GameObject[] bullets = new GameObject[3];
        
        for (int i = 0; i < 3; i++)
        {
            bullets[i] = Instantiate(attackPrefab1, transform.position, transform.rotation);
        }

        StartCoroutine(ObjectMover(bullets[0], forwardPos, 1.2f));
        StartCoroutine(ObjectMover(bullets[1], leftPos, 1.2f));
        StartCoroutine(ObjectMover(bullets[2], rightPos, 1.2f));
        PlaySFX(5);
        yield return new WaitForSeconds(1.5f);

        Vector3[] eachVector = new Vector3[3];
        for (int i = 0; i < 3;i++)
        {
            eachVector[i] = bullets[i].transform.position;
            Destroy(bullets[i]);
        }

        for (int i = 0; i < 3; i++)
        {
            BasicAttack(eachVector[i], 80, 40, 3, target, attackPrefab2, 0, 150, 0);
        }
        PlaySFX(4);

        yield return null;
    }

    IEnumerator SpawnJailObject(Transform target, float radius, int num, float duration, GameObject attackPrefab)
    {
        GameObject[] bullets = new GameObject[num];
        for (int i = 0; i < num; i++)
        {
            // 구 안의 랜덤한 방향과 거리
            Vector3 randomOffset = Random.onUnitSphere * radius;

            // 최종 위치 = 기준 위치 + 랜덤 오프셋
            Vector3 randomPosition = target.position + randomOffset;

            bullets[i] = Instantiate(attackPrefab, randomPosition, attackPrefab.transform.rotation);
            StartCoroutine(RotateAroundCenter(bullets[i].transform, 120, target));
        }
        
        yield return new WaitForSeconds(duration);

        for (int i = 0; i < num; i++)
        {
            Destroy(bullets[i]);
        }
        bullets = null;


    }

    IEnumerator FinalPattern()
    {
        while (true)
        {
            for (int i = 0; i <= 5; i++)
            {
                SlowdownAttack(transform.position, 40, 10, 6, new Vector3(-25 + 10 * i, 30, 20), attackPrefab[3], 184, 130, 39, 8f, 2f);
                PlaySFX(4);
                yield return new WaitForSeconds(0.2f);
            }

            for (int i = 0; i <= 5; i++)
            {
                SlowdownAttack(transform.position, 40, 10, 6, new Vector3(25 - 10 * i, -30, 20), attackPrefab[3], 184, 130, 39, 8f, 2f);
                PlaySFX(4);
                yield return new WaitForSeconds(0.2f);
            }
        }
    }

    protected override IEnumerator DeathCoroutine()
    {
        StartCoroutine(finalPhase);
        return base.DeathCoroutine();
    }
}
