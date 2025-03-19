using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woopsoon_Mid : Enemy_Boss
{
    private GameObject playerCharacter;
    public GameObject Minion;
    // Start is called before the first frame update
    protected override void Start()
    {
        Life = 2;
        playerCharacter = FindPlayer();

        base.Start();
        healthBar.SetName("???");
    }

    protected override void PhaseSetter(int remainLife)
    {
        base.PhaseSetter(remainLife);
        if (remainLife == 1)
        {
            StartCoroutine(skillMotion(1));
        }

        Debug.Log($"선?녀의 남은 목숨: {Life}");
    }

    protected override IEnumerator Phase2() //첫 번째 패턴
    {
        //StopCoroutine(TimerCoroutine);
        Health = 600f;
        TimerCoroutine = StartCoroutine(PhaseTimer(30));
        //StartCoroutine(mainCameraController.BossCutScene(2f));
        yield return new WaitForSeconds(1.5f);


        while (true)
        {
            for (int i = 0; i < 3; i++)
            {
                RandomMove(10, 1f);
                PlaySFX(4);
                BasicAttack(80, 40, 4, playerCharacter, attackPrefab[0]);

                yield return new WaitForSeconds(1f);
            }

            PlaySFX(5);
            BasicSpin(50, 50, attackPrefab[1], 20f);
            BasicSpin(50, 40, attackPrefab[1], 15f);
            BasicSpin(50, 30, attackPrefab[1], 10f);

            yield return new WaitForSeconds(3f);
        }

    }

    protected override IEnumerator Phase1() //두 번째 패턴
    {
        Health = 800f;
        TimerCoroutine = StartCoroutine(PhaseTimer(60));
        //StartCoroutine(mainCameraController.BossCutScene(2f));
        CutScene(2f);
        PlaySFX(2);

        SpellName = "100마리의 방개들..어셈블";
        SpellCard(SpellName);

        int X = 0 ;
        int Y = 0 ;

        yield return new WaitForSeconds(4f);

        for (int i = 0; i < 5; i++)
        {
            for (int k = 0; k < 20; k++)
            {
                X = Random.Range(-40, 40);
                Y = Random.Range(-40, 40);
                //Vector3 spawnPosition = new Vector3(X, Y, -50);
                SpawnEnemy(Minion, X, Y, -50);
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(2f);
            RandomMove(20, 2f);

            for (int k = 0; k < 8; k++)
            {
                PlaySFX(5);
                SlowdownAttack(20, 20, 3, playerCharacter, attackPrefab[1], 4, 1);
                yield return new WaitForSeconds(0.5f);
            }

            RandomMove(20, 2f);
            yield return new WaitForSeconds(2f);


        }

        Vector3 originPosition = new Vector3(X, Y, -50);
        StartCoroutine(ObjectMover(originPosition, 3f));

        while (true)
        {
            PlaySFX(5);
            SlowdownAttack(20, 20, 3, playerCharacter, attackPrefab[1], 4, 1);
            yield return new WaitForSeconds(0.5f);
        }
        //SpellName = "너희 중 죄 없는 자만\n 그에게 돌을 던져라.";
        //SpellCard(SpellName);


    }


    protected override IEnumerator DeathCoroutine()
    {
        ResetProjectile("Enemy");
        PlayerCamera.CameraShake(2);
        ResetProjectile();
        activatePointer(false);
        Instantiate(DeathEffect2, transform.position, Quaternion.identity);
        HealthBarObject.SetActive(false);
        //stageScript.toNextStage("Stage2");
        if (BGM_Script != null) PlaySFX(3);
        
        
        //Destroy(gameObject);
        BoxCollider myCollider = GetComponent<BoxCollider>();
        myCollider.enabled = false;

        yield return new WaitForSeconds(1f);
        Vector3 exitPosition = new Vector3(0, 0, -90);
        StartCoroutine(ObjectMover(exitPosition, 2f));
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        yield return null;
    }

}
