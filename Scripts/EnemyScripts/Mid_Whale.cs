using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mid_Whale : Enemy_Boss
{
    //private GameObject playerCharacter;
    // Start is called before the first frame update
    //public Image GoraeImage;
    public CanvasGroup Gorae_Image;


    protected override void Start()
    {
        Life = 2;
        //playerCharacter = FindPlayer();

        base.Start();
        healthBar.SetName("Gorae");
        StartCoroutine(ImageAppear());
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
        Debug.Log($"고래의 남은 목숨: {Life}");
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
                BasicAttack(20, 40f, 5, playerCharacter, attackPrefab[0], 0, 115, 255);
                //SingleShot(30f, attackPrefab[2], playerCharacter);
            }

            yield return new WaitForSeconds(1f);
            for (int i = 0; i < 6; i++)
            {
                yield return new WaitForSeconds(0.3f);
                PlaySFX(4);
                ShootAround(playerCharacter, 20, attackPrefab[1], 20f, 30, 0.2f, 116, 179, 255);
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

        SpellName = "창조주에 대한 분노 표출";
        SpellCard(SpellName);

        yield return new WaitForSeconds(3f);


        while (true)
        {
            for (int i = 0; i < 10; i++)
            {
                ShootandFall(20, attackPrefab[0]);
                yield return new WaitForSeconds(0.2f);
                PlaySFX(4);
            }
            RandomMove(10, 1);
        }

    }


    protected override IEnumerator DeathCoroutine()
    {
        
        PlayerCamera.CameraShake(2);
        ResetProjectile();
        activatePointer(false);
        Instantiate(DeathEffect2, transform.position, Quaternion.identity);
        HealthBarObject.SetActive(false);
        //stageScript.toNextStage("Stage2");
        PlaySFX(3);
        Destroy(gameObject);
        yield return null;
    }

    private void ShootandFall(int num, GameObject prefab)
    {
        float radius = 20f;
        float randSpeed = 0.2f;
        float speed = 30f;

        Vector3 targetPosition = gameObject.transform.position;
        targetPosition.y = 30;
        
        for (int i = 0; i < num; i++)
        {
            GameObject projectile = Instantiate(prefab, transform.position, Quaternion.identity);
            AttackColor attackColor = projectile.GetComponent<AttackColor>();
            attackColor.SetAttackColor(0, 116, 184);
            //주변을 향해 발사
            Vector3 randomOffset = Random.onUnitSphere * radius; //(5: 반지름 크기)
            Vector3 targetDirection = (targetPosition + randomOffset - transform.position).normalized;

            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.useGravity = true;

            float randomSpeedFactor = Random.Range(1 - randSpeed, 1 + randSpeed);
            rb.velocity = targetDirection * speed * randomSpeedFactor;
        }

    }
    
    private IEnumerator ImageAppear()
    {
        Gorae_Image.alpha = 0f;
        float fadeInDuration = 0.2f;
        float fadeOutDuration = 2f;

        float t = 0f;
        while (t < fadeInDuration)
        {
            t += Time.deltaTime;
            Gorae_Image.alpha = Mathf.Clamp01((t / fadeInDuration) * 0.6f);
            yield return null;

        }

        t = 0f;
        while (t < fadeOutDuration)
        {
            t += Time.deltaTime;
            Gorae_Image.alpha = Mathf.Clamp01(1f - t / fadeOutDuration);
            yield return null;
        }

        Gorae_Image.gameObject.SetActive(false);
    }

}
