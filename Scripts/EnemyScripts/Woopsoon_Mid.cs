using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woopsoon_Mid : Enemy_Boss
{
    private GameObject playerCharacter;
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

    protected override IEnumerator Phase2() //두번째 패턴
    {
        //StopCoroutine(TimerCoroutine);
        Health = 800f;
        TimerCoroutine = StartCoroutine(PhaseTimer(30));
        //StartCoroutine(mainCameraController.BossCutScene(2f));
        yield return new WaitForSeconds(3f);


        while (true)
        {
            for (int i = 0; i < 6; i++)
            {
                yield return new WaitForSeconds(0.3f);
                PlaySFX(4);
                BasicAttack(20, 40f, 5, playerCharacter, attackPrefab[0]);
                //SingleShot(30f, attackPrefab[2], playerCharacter);
            }

            yield return new WaitForSeconds(1f);
            for (int i = 0; i < 6; i++)
            {
                yield return new WaitForSeconds(0.3f);
                PlaySFX(4);
                ShootAround(playerCharacter, 20, attackPrefab[1], 20f, 30, 0.2f);
                //SingleShot(30f, attackPrefab[2], playerCharacter);
            }

            yield return new WaitForSeconds(1f);

            RandomMove(10, 2);
        }

    }

    protected override IEnumerator Phase1()
    {
        Health = 1200f;
        TimerCoroutine = StartCoroutine(PhaseTimer(60));
        //StartCoroutine(mainCameraController.BossCutScene(2f));
        CutScene(2f);
        PlaySFX(2);

        SpellName = "우우 쓰레기";
        SpellCard(SpellName);

        yield return new WaitForSeconds(3f);


        
    }


    protected override IEnumerator DeathCoroutine()
    {

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
