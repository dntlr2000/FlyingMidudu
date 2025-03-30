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
    }

    protected override IEnumerator Phase1() //두 번째 패턴
    {
        Health = 800f;
        TimerCoroutine = StartCoroutine(PhaseTimer(60));
        //StartCoroutine(mainCameraController.BossCutScene(2f));
        CutScene(2f);
        PlaySFX(2);

        SpellName = "회춘의 비결";
        SpellCard(SpellName);

        yield return new WaitForSeconds(2f);
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