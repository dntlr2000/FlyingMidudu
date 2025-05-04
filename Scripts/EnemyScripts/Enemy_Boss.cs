using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;
using TMPro;

public class Enemy_Boss : Enemy
{
    //보스의 경우 스테이지에 미리 불러낸 상태에서 비활성화된 채로 있다가, 보스전이 시작될 때 활성화하는 방식으로 가야할듯.
    // Start is called before the first frame update
    //[Header("CamearController 스크립트 할당")]
    public CameraController PlayerCamera;
    //public Camera MainCamera;
    public GameObject MainCamera;
    public GameObject MyCamera;
    protected MainCameraController mainCameraController;
    //public Camera BossCamera;
    //protected Animator animator;

    public GameObject pointer;

    protected Collider BossCollider;

    private int timer;

    public GameObject[] attackPrefab;

    public GameObject HealthBarObject; //체력바 오브젝트
    protected HealthBar healthBar; //체력바 스크립트

    protected Coroutine ph1; //목숨1개 남았을 때
    protected Coroutine ph2; //2개 남았을 때
    protected Coroutine ph3; //3개 남았을 때
    protected Coroutine ph4; // ...
    protected Coroutine ph5;
    protected Coroutine ph6;
    protected Coroutine ph7;
    protected Coroutine ph8;
    protected Coroutine ph9;
    protected Coroutine ph10;

    public GameObject DeathEffect1;
    public GameObject DeathEffect2;

    protected Coroutine TimerCoroutine;
    
    public StageText SpellText;
    protected string SpellName;

    public BossText BossText;
    protected string BossName ="";
    protected string BossDescription = "";

    public Stage stageScript;

    protected GameObject playerCharacter;
    
    

    protected override void Start()
    {
        //Life = 3;
        //Health = 100;
        playerCharacter = FindPlayer();
        HealthBarObject.SetActive(true);
        healthBar = HealthBarObject.GetComponent<HealthBar>();

        ResetProjectile(); //등장 시 적 탄막 모두 삭제
        activatePointer(true);

        animator = GetComponent<Animator>();
        BossCollider = GetComponent<Collider>();

        PhaseSetter(Life);

        mainCameraController = MainCamera.GetComponent<MainCameraController>();

        if (BossText != null)
        {
            BossText.gameObject.SetActive(true);
            StartCoroutine(BossText.BossInformation(BossDescription, BossName));
        }

        BGM_Script = FindObjectOfType<BGMController>();

        if (MyCamera != null) mainCameraController.camera_e = MyCamera.transform;
    }

    // Update is called once per frame
    protected override void Update()
    {

    }

    public int Timer
    {
        get { return timer; }
        set { timer = value; }
    }

    protected IEnumerator skillMotion(int num, float duration = 3f, bool ifInvincible = true) //스킬 모션 및 판정
    {

        if (ifInvincible) BossCollider.enabled = false;
        animator.SetInteger("Motion", num);
        yield return new WaitForSeconds(1f);
        animator.SetInteger("Motion", 0);
        yield return new WaitForSeconds(duration);
        if (ifInvincible) BossCollider.enabled = true;
    }

    protected override void getHit(float damage) //피격 스크립트
    {

        Health -= damage;
        healthBar.SetHealth(Health); 
        if (Health <= 0)
        {
            Life -= 1;
            PhaseSetter(Life);
        }
    }

    protected override void PhaseSetter(int remainLife) //페이즈 전환, 목숨 개수 처리는 getHit에서 관리함
    {
        //SpellText.gameObject.SetActive(false);
        SpellText.SetText("");
        if (remainLife <= 0)
        {
            if (ph1 != null) StopCoroutine(ph1);
            Death();
            return;
        }

        else
        {
            StartCoroutine(skillMotion(0, 2f));
        }

        ResetProjectile();
        if (TimerCoroutine != null) StopCoroutine(TimerCoroutine);
        //PlayerCamera.CameraShake(1);

        if (remainLife == 10) 
        {
            ph10 = StartCoroutine(Phase10());
        }
        else if (remainLife == 9) 
        {
            if (ph10 != null) StopCoroutine(ph10);
            ph9 = StartCoroutine(Phase9());
        }

        else if (remainLife == 8) //남은 목숨 8개, 페이즈 추가가 필요할 시 위에 7부터 추가하면 될듯
        {
            if (ph9 != null) StopCoroutine(ph9);
            ph8 = StartCoroutine(Phase8());
        }
        else if (remainLife == 7) //남은 목숨 7개, 페이즈 추가가 필요할 시 위에 7부터 추가하면 될듯
        {
            if (ph8 != null) StopCoroutine(ph8);
            ph7 = StartCoroutine(Phase7());
        }

        else if (remainLife == 6) //남은 목숨 6개, 페이즈 추가가 필요할 시 위에 7부터 추가하면 될듯
        {
            if (ph7 != null) StopCoroutine(ph7);
            ph6 = StartCoroutine(Phase6());
        }
        else if (remainLife == 5)
        {
            if (ph6 != null) StopCoroutine(ph6);
            ph5 = StartCoroutine(Phase5());
        }

        else if (remainLife == 4)
        {
            if (ph5 != null) StopCoroutine(ph5);
            ph4 = StartCoroutine(Phase4());
        }

        else if (remainLife == 3)
        {
            if (ph4 != null) StopCoroutine(ph4);
            ph3 = StartCoroutine(Phase3());
        }

        else if (remainLife == 2)
        {
            if (ph3 != null) StopCoroutine(ph3);
            ph2 = StartCoroutine(Phase2());
        }

        else if (remainLife == 1) //남은 목숨 1개
        {
            if (ph2 != null) StopCoroutine(ph2);
            ph1 = StartCoroutine(Phase1());
        }

        healthBar.SetMaxHealth(Health);
        healthBar.SetLife(Life - 1);
    }

    protected override void Death() //사망 연출
    {
        StartCoroutine(DeathCoroutine());
        //스테이지를 클리어했다는 것을 Stage 스크립트에 넘겨야 함
    }

    protected virtual IEnumerator DeathCoroutine()
    {
        BossCollider.enabled= false;
        Instantiate(DeathEffect1, transform.position, Quaternion.identity);
        for (int i = 0;i < 2; i++) {
            PlayerCamera.CameraShake(1);
            PlaySFX(2);
            yield return new WaitForSeconds(0.6f);
        }
        PlayerCamera.CameraShake(2);
        ResetProjectile();
        activatePointer(false);
        Instantiate(DeathEffect2, transform.position, Quaternion.identity);
        HealthBarObject.SetActive(false);
        //stageScript.toNextStage("Stage2");
        stageScript.StageLoader(false);
        PlaySFX(3);
        Destroy(gameObject);
    }

    protected IEnumerator PhaseTimer(int time) //타이머
    {
        healthBar.SetTimer(time);
        yield return new WaitForSeconds(3f);
        //Timer = time;
        for (int i = time; i > 0; i--)
        {
            healthBar.SetTimer(i);
            yield return new WaitForSeconds(1f);
        }
       
        getHit(10000f);
    }


    //페이즈 별 공격 패턴
    protected virtual IEnumerator Phase1()
    {
        Health = 100f;
        StartCoroutine(PhaseTimer(60));
        yield return new WaitForSeconds(1f);
    }
    protected virtual IEnumerator Phase2()
    {
        Health = 100f;
        StartCoroutine(PhaseTimer(60));
        yield return new WaitForSeconds(1f);


    }
    protected virtual IEnumerator Phase3()
    {
        Health = 100f;
        StartCoroutine(PhaseTimer(60));
        yield return new WaitForSeconds(1f);

    }

    protected virtual IEnumerator Phase4()
    {
        Health = 100f;
        StartCoroutine(PhaseTimer(60));
        yield return new WaitForSeconds(1f);
    }
    protected virtual IEnumerator Phase5()
    {
        Health = 100f;
        StartCoroutine(PhaseTimer(60));
        yield return new WaitForSeconds(1f);

    }
    protected virtual IEnumerator Phase6()
    {
        Health = 100f;
        StartCoroutine(PhaseTimer(60));
        yield return new WaitForSeconds(1f);

    }
    protected virtual IEnumerator Phase7()
    {
        Health = 100f;
        StartCoroutine(PhaseTimer(60));
        yield return new WaitForSeconds(1f);

    }
    protected virtual IEnumerator Phase8()
    {
        Health = 100f;
        StartCoroutine(PhaseTimer(60));
        yield return new WaitForSeconds(1f);

    }
    protected virtual IEnumerator Phase9()
    {
        Health = 100f;
        StartCoroutine(PhaseTimer(60));
        yield return new WaitForSeconds(1f);

    }
    protected virtual IEnumerator Phase10()
    {
        Health = 100f;
        StartCoroutine(PhaseTimer(60));
        yield return new WaitForSeconds(1f);

    }

    //플레이어가 보스의 위치를 알려주는 포인터 활성화
    protected void activatePointer(bool onPoint)
    {
        if (onPoint)
        {
            pointer.SetActive(true);
            AimConstraint pointerAim = pointer.GetComponent<AimConstraint>();
            if (pointerAim == null)
            {
                pointerAim = pointer.AddComponent<AimConstraint>();
            }
            else
            {
                pointerAim.constraintActive = false;

                if (pointerAim.sourceCount > 0) // 소스가 존재하는 경우에만 제거
                {
                    pointerAim.RemoveSource(0);
                }
            }

            ConstraintSource source = new ConstraintSource
            {
                sourceTransform = gameObject.transform,
                weight = 1.0f
            };
            pointerAim.AddSource(source);

            pointerAim.constraintActive = true;
            //aimConstraint.locked = true;
            pointerAim.rotationAtRest = Vector3.zero;
            pointerAim.worldUpType = AimConstraint.WorldUpType.SceneUp;
        }

        else
        {
            pointer.SetActive(false);
        }
    }

    protected virtual void SpawnEnemy(GameObject enemy, int x, int y, int z)
    {
        Vector3 spawnPosition = new Vector3(x, y, z - 50);
        GameObject spawnedEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
        StartCoroutine(ObjectMover(spawnedEnemy, spawnPosition, new Vector3(x, y, z), 2f));
    }

    protected virtual void SpawnEnemy(GameObject enemy, Vector3 newPos, bool isInstant = true)
    {
        GameObject spawnedEnemy;
        if (isInstant)
        {
           spawnedEnemy = Instantiate(enemy, newPos, Quaternion.identity);
        }

        else
        {
            spawnedEnemy = Instantiate(enemy, transform.position - new Vector3(0, 0, -50), Quaternion.identity);
            StartCoroutine(ObjectMover(spawnedEnemy, newPos, 2f));
        }


    }

    protected virtual void CutScene(float time)
    {
        Player playerScript = FindObjectOfType<Player>();
        MainCamera.SetActive(true);
        StartCoroutine(mainCameraController.BossCutScene(time));
        if (playerScript != null)
        {
            StartCoroutine(playerScript.shooterSwitch(time));
        }
        else
        {
            Debug.LogWarning("playerScript not found!");
        }
        ResetProjectile("PlayerAttackA");
        
    }

    protected void SpellCard(string spellName)
    {
        //SpellText.gameObject.SetActive(true);
        StartCoroutine(SpellText.SpellCardAnimation(spellName));
    }

    
}

