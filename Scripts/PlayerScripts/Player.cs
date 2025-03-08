using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int life = 3; //목숨
    private int bomb = 2; //봄

    private PlayerMovement moveScript;
    private PlayerAnimator animator;
    private Collider playerCollider;

    public GameObject Shooter;
    public CameraController CameraScript;
    private Animator CameraAnimator; //카메라 거리 조절용

    public GameObject meshObject;

    public WoopLifeUI woopUI;

    private Coroutine Invincible;

    //public GameObject HitEffect;

    public GameOver overScript;

    private BGMController BGM_Script;

    //private PlayerSetting PlayerSettingScript; //나중에 스테이지 간 목숨 공유가 필요하다고 느낄 때 수정 시작하기로

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _ = GetComponent<Rigidbody>();
        moveScript = GetComponent<PlayerMovement>();
        animator = GetComponent<PlayerAnimator>(); 
        playerCollider = GetComponent<Collider>();
        //PlayerSettingScript = FindObjectOfType<PlayerSetting>();

        woopUI.SetLife(Life);
        woopUI.SetBomb(Bomb);

        CameraAnimator = CameraScript.gameObject.GetComponent<Animator>();
        BGM_Script = FindAnyObjectByType<BGMController>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    public int Life
    {
        get { return life; }
        set { life = value; }
    }

    public int Bomb
    {
        get { return bomb; }
        set { bomb = value; }
    }

    protected virtual void OnTriggerEnter(Collider other) //피격 시
    {
        if (other.tag == "EnemyAttack") //기본 탄막
        {
            if (other.transform.parent != null) //레이저의 경우
            {
                Destroy(other.transform.parent.gameObject);
            }
            else //일반 탄막
            {
                Destroy(other.gameObject);
            }
            if (BGM_Script != null) BGM_Script.PlaySFX(3);
            getHit();
        }

        else if (other.tag == "Enemy" || other.tag == "Block") 
        {
            getHit();
        }

        else
        {

        }
    }

    protected virtual void getHit()
    {
        
       if (Life > 1)
        {
            Life -= 1;
            woopUI.SetLife(Life);
            if (Bomb < 2)
            {
                Bomb= 2;
                woopUI.SetBomb(Bomb);
            }
            CameraScript.CameraShake(2);
            Invincible = StartCoroutine(Respawn());
        }

       else
        {
            CameraScript.CameraShake(2);
            woopUI.SetLife(0);

            overScript.PauseGame();

            gameObject.SetActive(false);
            Shooter.gameObject.SetActive(false);
        }
    }


    public virtual IEnumerator Respawn()
    {
        moveScript.enabled= false;
        playerCollider.enabled= false;
        animator.StartHitMotion();
        yield return new WaitForSeconds(2f);
        StartCoroutine(BlinkingMesh());
        moveScript.enabled= true;
        yield return new WaitForSeconds(2f);
        playerCollider.enabled = true;
    }

    private IEnumerator BlinkingMesh()
    {
        for (int i = 0;i < 7; i++)
        {
            meshObject.SetActive(false);
            yield return new WaitForSeconds(0.125f);
            meshObject.SetActive(true);
            yield return new WaitForSeconds(0.125f);
        }
        
    }

    public IEnumerator UltInvincible()
    {
        if (Invincible != null) StopCoroutine(Invincible);
        if (BGM_Script != null) BGM_Script.PlaySFX(2);
        woopUI.SetBomb(Bomb);
        CameraScript.CameraShake(1);
        playerCollider.enabled = false;
        yield return new WaitForSeconds(3f);
        StartCoroutine(BlinkingMesh());
        yield return new WaitForSeconds(2f);
        playerCollider.enabled = true;
    }

    /*
    private IEnumerator askContinue()
    {
        yield return new WaitForSeconds(2f);
        overScript.PauseGame();
    }
    */
    
}
