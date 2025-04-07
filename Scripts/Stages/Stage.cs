using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    //각 스테이지는 이 스크립트의 클래스에서 파생하여 제작.
    //여기선 protected virtual로 잡몹 생성 로직 틀을 짜놓기

    public GameObject[] Enemy; //적들
    public GameObject Boss;
    public GameObject MidBoss;

    public StageText Text; //시작 시 등장하는 스크립트

    protected string text = $"제 1장\n저 높은 곳에 보물이 있다는 소문을 들은 흰 머리\n 악인의 수하들은 그들의 두목이 떠나는 것을 막기\n 위해 나섰습니다.";

    //private bool ifCleared = false;

    public StageLoader loader;

    private string nextStageName = "Stage1";

    public StageText toNextStage1;
    public StageText toNextStage2;

    //private GameObject AudioObject;
    public BGMController BGM_Script;
    //protected int BGM_Index = 0;

    public SaveManager saveScript;
    protected int currentStage = 1; //이후 nextStageName과 통합할 수 있으면 좋을듯

    protected virtual void Start()
    {

        //text = $"제 1장\n저 높은 곳에 보물이 있다는 소문을 들은 흰 머리\n 악인의 수하들은 그들의 두목이 떠나는 것을 막기\n 위해 나섰습니다.";
        //StageBuilder();
        //StartCoroutine(StagePhase1());
        SpawnBoss(Boss, 0, 0, -50);
        
        Text.gameObject.SetActive(true);
        StartCoroutine(Text.StageTextAnimation(text));

        BGM_Script = FindObjectOfType<BGMController>();

        saveScript = GetComponent<SaveManager>();

    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    protected string NextStageName
    {
        get { return nextStageName; }
        set { nextStageName = value; }
    }

    protected virtual bool CheckEnemyExist(string Tag = "Enemy")
    {
        GameObject[] remainsEnemy = GameObject.FindGameObjectsWithTag(Tag);
        if (remainsEnemy.Length > 0) return true;
        else return false;

    }

    protected virtual void OnTriggerExit(Collider other)
    {
        // 특정 태그를 가진 오브젝트만 삭제
        if (other.CompareTag("EnemyAttack")
            || other.CompareTag("UltimateA")
            || other.CompareTag("PlayerAttackA")
            || other.CompareTag("Enemy")
            //|| other.CompareTag("Item")
            )
        {
            Destroy(other.gameObject);
        }

    }


    protected virtual IEnumerator StagePhase1()
    {
        Debug.Log("Phase 1 Started");
        yield return new WaitForSeconds(6f);
        SpawnEnemy(Enemy[0], -10, 0, -40);
        SpawnEnemy(Enemy[0], 10, 0, -40);
        SpawnEnemy(Enemy[1], 0, -20, -40);

        while (CheckEnemyExist())
        {
            yield return new WaitForSeconds(2f);
        }
        Debug.Log("Phase 1 Clear");
        ResetProjectile();
        StartCoroutine(StagePhase2());
    }

    protected virtual IEnumerator StagePhase2()
    {
        yield return new WaitForSeconds(1f);
        SpawnEnemy(Enemy[1], -10, 0, -40);
        SpawnEnemy(Enemy[1], 10, 0, -40);
        yield return new WaitForSeconds(5f);
        SpawnEnemy(Enemy[0], 0, 10, -40);
        SpawnEnemy(Enemy[0], 0, -10, -40);
        while (CheckEnemyExist())
        {
            yield return new WaitForSeconds(2f);
        }
        Debug.Log("Phase 2 Clear");
        //ResetProjectile();

        yield return new WaitForSeconds(3f);
        SpawnBoss(Boss, 0, 0, -50);
    }

    protected virtual GameObject SpawnEnemy(GameObject enemy, int x, int y, int z, bool isInstant = false)
    {
        Vector3 spawnPosition;
        if (!isInstant) spawnPosition = new Vector3(x, y, z - 50);
        else spawnPosition = new Vector3(x, y, z);

        GameObject spawnedEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
        StartCoroutine(ObjectMover(spawnedEnemy, spawnPosition, new Vector3(x, y, z), 1f));
        return spawnedEnemy;
    }

    protected virtual void SpawnBoss(GameObject enemy, int x, int y, int z)
    {
        //Vector3 spawnPosition = new Vector3(x, y, z);
        //Instantiate(enemy, spawnPosition, Quaternion.identity); //보스는 생성이 아니라 존재하는걸 활성화하는 방법으로 구현하기로
        //등장 전용 이펙트랑 함께 생성하도록 하는게 나을듯

        enemy.transform.position = new Vector3(x, y, z - 50);
        enemy.SetActive(true);
        StartCoroutine(ObjectMover(enemy, new Vector3(x, y, z - 50), new Vector3(x, y, z), 1f));
        
    }

    //Enemy 스크립트에서 임시?로 가져온 상태
    protected virtual void ResetProjectile(string tag = "EnemyAttack")
    {
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag(tag);
        Debug.Log($"Found {objectsToDestroy.Length} objects with tag {tag}");
        foreach (GameObject obj in objectsToDestroy) 
        {
            Destroy(obj);
            obj.transform.parent = null; //오브젝트 뒤처리
        }
    }

    protected IEnumerator ObjectMover(GameObject target, Vector3 from, Vector3 to, float time)   //Enemy_Boss에도 동일한 스크립트 존재
    {
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;

            // 이동 비율 계산
            float t = Mathf.Clamp01(elapsedTime / time); //균일한 속도로 이동
            t = 1f - Mathf.Pow(1f - t, 2f); // Ease-Out 적용

            //float t = Mathf.SmoothStep(0f, 1f, elapsedTime / time); //부드?럽게

            // 위치 보간
            target.transform.position = Vector3.Lerp(from, to, t);

            // 다음 프레임까지 대기
            yield return null;
        }
    }

    protected IEnumerator ObjectMover(GameObject target, Vector3 to, float time)   //from = 원래 자리
    {
        float elapsedTime = 0f;
        Vector3 from = target.transform.position;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;

            // 이동 비율 계산
            float t = Mathf.Clamp01(elapsedTime / time); //균일한 속도로 이동
            t = 1f - Mathf.Pow(1f - t, 2f); // Ease-Out 적용

            // 위치 보간
            target.transform.position = Vector3.Lerp(from, to, t);

            // 다음 프레임까지 대기
            yield return null;
        }
    }

    public void StageLoader(bool isInstant = true)
    {
        if (NextStageName== null) { return; }
        //isInstant: 메뉴 등에서 바로 넘어갈 필요가 있을 때 true로 설정
        Debug.Log("Stage Cleared!");
        //IfCleared = false;
        // 다음 스테이지로 넘어가기
        //StageLoader loader = FindObjectOfType<StageLoader>();
        if (BGM_Script != null) { BGM_Script.PauseBGM(); }

        if (isInstant)
        {
            loader.LoadSceneAsync(NextStageName);
        }
        else
        {
            StartCoroutine(toNextStageCoroutine(NextStageName));
        }
        
    }

    private IEnumerator toNextStageCoroutine(string sceneName)
    {
        yield return new WaitForSeconds(1f);
        //Text.gameObject.SetActive(true);
        //text = "스테이지 클리어!\n 잠시 후 다음 스테이지로 이동합니다.";
        //StartCoroutine(Text.StageTextAnimation(text));
        toNextStage1.gameObject.SetActive(true);
        StartCoroutine(toNextStage1.StageClearAnimation("스테이지 클리어!"));

        yield return new WaitForSeconds(2f);
        toNextStage2.gameObject.SetActive(true);
        StartCoroutine(toNextStage2.StageClearAnimation("잠시 후 다음 스테이지로 이동합니다."));


        yield return new WaitForSeconds(5f); //보스 사망 연출 시작으로부터 지연 시간

        saveScript.SaveProgress(currentStage);
        
        loader.LoadSceneAsync(sceneName);
    }


    
    protected void SetBGM(int index)
    { 
        
        if (BGM_Script == null) {
            Debug.Log("BGMScript NOT FOUND");
            return; }
        BGM_Script.BgmIndex = index;
        BGM_Script.ChangeBGM(BGM_Script.BgmIndex);
        BGM_Script.PlayBGM();
    }

    private IEnumerator spawnBossAfter1Second()
    {
        yield return new WaitForSeconds(1f);
        SpawnBoss(Boss, 0, 0, -50);
        yield return null;
    }
}
    
