using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BangaeKing : Enemy_Boss
{
    private GameObject playerCharacter;
    public GameObject Minion;
    

    protected override void Start()
    {
        Life = 3;
        Health = 100f;
        BossName = "방개킹";
        BossDescription = "스토리상 끼워넣을만한 건덕지가 없지만 그래도 추가한";

        playerCharacter = FindPlayer();

        base.Start();

        healthBar.SetName("Bangae King");
    }

    protected override void PhaseSetter(int remainLife)
    {
        base.PhaseSetter(remainLife);
        if (remainLife > 0 && remainLife < 3)
        {
            StartCoroutine(skillMotion(3 - remainLife));
            PlayerCamera.CameraShake(1);
        }

        Debug.Log($"방개킹의 남은 목숨: {Life}");
    }

    protected override IEnumerator Phase3() //첫 패턴
    {
        
        Health = 600f;
        TimerCoroutine = StartCoroutine(PhaseTimer(40));
        yield return new WaitForSeconds(3f);


        while (true)
        {
            yield return new WaitForSeconds(1f);
            RandomMove(10, 1.5f);
            for (int i = 0;i < 3; i++)
            {
                yield return new WaitForSeconds(0.5f); // 퍼지는 간격
                PlaySFX(4);
                BasicAttack(200, 20f, 2, playerCharacter, attackPrefab[1], 250, 0, 0);
            }
  

            yield return new WaitForSeconds(1f);
            PlaySFX(5);
            BasicSpin(200, 20f, attackPrefab[0], 20f, 0, 142, 224);
            
        }
    }

    protected override IEnumerator Phase2() //두번째 패턴
    {
        PlaySFX(2);
        SpellName = "이젠 누가 방송해주냐";
        SpellCard(SpellName);
        //StopCoroutine(TimerCoroutine);
        Health = 800f;
        TimerCoroutine = StartCoroutine(PhaseTimer(60));
        //StartCoroutine(mainCameraController.BossCutScene(2f));
        CutScene(2f);
        yield return new WaitForSeconds(3f);
        StartCoroutine(ObjectMover(new Vector3(0, 0, -50), 2));
        

        while (true)
        {
            for (int i = 0; i < 6; i++)
            {
                yield return new WaitForSeconds(0.5f);
                PlaySFX(4);
                BasicAttack(20, 40f, 4, playerCharacter, attackPrefab[1], 250, 0, 0);
                //SingleShot(30f, attackPrefab[2], playerCharacter);
            }

            yield return new WaitForSeconds(0.5f);
            RandomMove(10, 1);

            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(1f);
                PlaySFX(5);
                ShootAround(playerCharacter, 20, attackPrefab[0], 0.3f, 30f, 0.25f, 224, 146, 0);
                SingleShot(40f, attackPrefab[2], playerCharacter, 0, 38, 224);
            }
            yield return new WaitForSeconds(0.5f);

            RandomMove(10, 1);
        }



    }

    protected override IEnumerator Phase1() //세번째 패턴
    {
        PlaySFX(2);
        Health = 800f;
        TimerCoroutine = StartCoroutine(PhaseTimer(60));
        //StartCoroutine(mainCameraController.BossCutScene(3f));
        CutScene(3f);
        yield return new WaitForSeconds(2f);
        SpellName = "6:4:6의 황금비";
        SpellCard(SpellName);
        yield return new WaitForSeconds(2f);
        SpawnEnemy(Minion, -20, -20, -30);
        SpawnEnemy(Minion, 20, -20, -30);
        StartCoroutine(ObjectMover(new Vector3(0, 10, -50), 2));

        yield return new WaitForSeconds(1f);

        while (true)
        {
            
            for (int i = 0;i < 5;i++) {
                PlaySFX(4);
                SingleShot(60f, attackPrefab[0], playerCharacter, 255, 36, 0);
                SingleShot(50f, attackPrefab[0], playerCharacter, 255, 36, 0);
                SingleShot(40f, attackPrefab[0], playerCharacter, 255, 36, 0);
                yield return new WaitForSeconds(0.4f);
            }
            RandomMove(20, 2);
            yield return new WaitForSeconds(1f);

            //레이저
            //PlaySFX(4);
            ShootLasers(playerCharacter, 5, attackPrefab[4], 20f);
            yield return new WaitForSeconds(6f);
        }

    }
    protected override void Death()
    {
        ResetProjectile("Enemy"); 
        base.Death();
    }


}
