using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorpion : Enemy_Boss
{
    private GameObject playerCharacter;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        Life = 2;
        playerCharacter = FindPlayer();

        base.Start();
        healthBar.SetName("Geokdong Jungal");

    }

    protected override void PhaseSetter(int remainLife)
    {
        base.PhaseSetter(remainLife);
        if (remainLife == 1)
        {
            StartCoroutine(skillMotion(2));
        }

        Debug.Log($"전갈의 남은 목숨: {Life}");
    }

    protected override IEnumerator Phase2() //통상
    {
        Health = 800f;
        TimerCoroutine = StartCoroutine(PhaseTimer(40));
        //StartCoroutine(mainCameraController.BossCutScene(2f));
        yield return new WaitForSeconds(2f);

        while (true)
        {
            for (int k = 0; k < 2; k++)
            {
                for (int i = 0; i < 7; i++)
                {
                    SlowdownAttack(20, 40 + i * 10, 3, playerCharacter, attackPrefab[0], 16, 33, 171, 0.2f, 0.2f);
                    PlaySFX(4);
                    yield return new WaitForSeconds(0.1f);
                }
            }
            RandomMove(10, 2f);
            for (int i = 0; i < 5; i++)
            {
                BasicSpin(40, 40 + 5 * i, attackPrefab[2], i * 5, 64, 64, 64);
                PlaySFX(5);
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(1f);
            
        }
    }

    protected override IEnumerator Phase1() //두 번째 패턴
    {
        Health = 800f;
        TimerCoroutine = StartCoroutine(PhaseTimer(60));
        //StartCoroutine(mainCameraController.BossCutScene(2f));
        CutScene(2f);
        PlaySFX(2);

        SpellName = "캐나다에서 배운 방식";
        SpellCard(SpellName);
        yield return new WaitForSeconds(3f);

        StartCoroutine(ObjectMover(new Vector3(0, 0, -50), 2f));
        while (true)
        {
            for (int i = 10; i > -10; i -= 2)
            {
                PlaySFX(5);
                ShootAround(transform.position + new Vector3(-10, i, -5), playerCharacter, 10, attackPrefab[1], 10, 50, 0.1f, 222, 18, 18);
                ShootAround(transform.position + new Vector3(10, i, -5), playerCharacter, 10, attackPrefab[1], 10, 50, 0.1f, 222, 18, 18);
                BasicAttack(transform.position + new Vector3(0, i, -5), 20, 70, 3, playerCharacter, attackPrefab[0], 158, 158, 158);
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(0.5f);

            for (int i = 0; i < 10; i++)
            {
                PlaySFX(4);
                SingleShot(80, attackPrefab[2], playerCharacter, 222, 18, 18);
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(0.5f);
            RandomMove(10, 3f);
        }

    }

    protected override IEnumerator DeathCoroutine()
    {

        PlayerCamera.CameraShake(2);
        ResetProjectile();
        activatePointer(false);
        Instantiate(DeathEffect2, transform.position, Quaternion.identity);
        HealthBarObject.SetActive(false);
        PlaySFX(3);
        Destroy(gameObject);
        yield return null;
    }
}