using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterDrone : MonoBehaviour
{
    //발사 관련
    public Transform Projectile; //발사체
    public float launchSpeed; //발사속도
    public float launchinterval; //발사 간격
    public bool ifShoot = true; //발사중?
    public Transform launchCenter; //center 본 할당. 발사 방향

    private Coroutine shootMode; //코루틴 실행, 종료 관련

    //애니메이션 관련
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        shootMode = StartCoroutine(ShootingMode());
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) ShootModeChanger();
    }

    public void ShootModeChanger()
    {
         
        if (ifShoot) // -> 발사 종료
        {
            StopCoroutine(shootMode);
            shootMode = null;
            ifShoot = false;
            animator.SetBool("ifShoot", false);
        }
        else // -> 발사 시작
        {
            shootMode = StartCoroutine(ShootingMode());
            ifShoot= true;
            animator.SetBool("ifShoot", true);
        }

        
    }

    IEnumerator ShootingMode()
    {
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            //Vector3 launchPosition = transform.position + transform.forward * 2.0f;  // 1.0f는 오브젝트의 전방으로 1 유닛만큼 이동
            Transform projectileInstance = Instantiate(Projectile, launchCenter.position + launchCenter.forward * 2.0f, Quaternion.identity); //발사체 정의
            
            
            Rigidbody rb = projectileInstance.GetComponent<Rigidbody>();
            
            //발사 방향 및 회전 설정 : center 본을 따라가도록
            Vector3 direction = launchCenter.forward;
            projectileInstance.rotation = launchCenter.rotation;
            projectileInstance.Rotate(0, 0, 90);

            //발사 세기 및 유지 시간
            rb.AddForce(direction * launchSpeed, ForceMode.Impulse);
            Destroy(projectileInstance.gameObject, 3f);

            yield return new WaitForSeconds(launchinterval);
        }
    }

}
