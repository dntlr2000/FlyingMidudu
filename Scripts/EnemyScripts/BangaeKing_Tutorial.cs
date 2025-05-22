using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangaeKing_Tutorial : Enemy_Boss
{
    // Start is called before the first frame update
    protected override void Start()
    {
        Life = 2;
        //playerCharacter = FindPlayer();

        base.Start();
        healthBar.SetName("Alenen Lover");
    }

    protected override void PhaseSetter(int remainLife)
    {
        base.PhaseSetter(remainLife);
        if (remainLife == 1)
        {
            StartCoroutine(skillMotion(1));
            //PlayerCamera.CameraShake(1);
        }
        else
        {
            StartCoroutine(skillMotion(0, 2f));
        }
        Debug.Log($"미두두러버의 남은 목숨: {Life}");
    }


    protected override IEnumerator Phase2() //첫번째 패턴
    {
        Health = 400f;
        TimerCoroutine = StartCoroutine(PhaseTimer(40));


        while (true)
        {
            SingleShot(transform.position + new Vector3(0, 1.5f, 0),30, attackPrefab[1], playerCharacter, 125, 0, 0);
            yield return new WaitForSeconds(0.5f);
            PlaySFX(4);


        }
    }

    protected override IEnumerator Phase1() //두번째 패턴
    {
        PlaySFX(2);
        Health = 500f;
        TimerCoroutine = StartCoroutine(PhaseTimer(60));
        //StartCoroutine(mainCameraController.BossCutScene(3f));
        //CutScene(3f);
        yield return new WaitForSeconds(2f);
        SpellName = "광과민성 주의\"해줘\"";
        SpellCard(SpellName);

        yield return new WaitForSeconds(1f);

        while (true)
        {
            BasicAttack(transform.position + new Vector3(0, 1.5f, 0), 30, 30f, 4f, playerCharacter, attackPrefab[0], 125, 125, 125);
            yield return new WaitForSeconds(0.5f);
            PlaySFX(4);
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
