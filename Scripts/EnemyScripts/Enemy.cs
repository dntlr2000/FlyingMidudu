using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    private float health = 100f; //체력
    private int life = 1; //목숨( 남은 페이즈 수)

    public GameObject HitEffect;

    protected Animator animator;

    protected BGMController BGM_Script;

    
    protected virtual void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        PhaseSetter(life);
        BGM_Script = FindObjectOfType<BGMController>();
    }

    protected virtual void Update()
    {
        
    }

    protected float Health //체력
    {
        get { return health; }
        set { health = value; }
    }

    protected int Life //남은 목숨(페이즈 개수)
    {
        get { return life; }
        set { life = value; }
    }

    protected virtual void OnTriggerEnter(Collider other) //피격 시
    {

        if (other.tag == "PlayerAttackA") //기본 탄막
        {
            HitEffectGen(other);

            Destroy(other.gameObject);
            getHit(5f);
        }
        if (other.tag == "UltimateA") //기본 탄막
        {

            for (int i = 0; i < 3; i++) HitEffectGen(other);

            Destroy(other.gameObject);
            getHit(20f);
        }
    }

    protected virtual void getHit(float damage) //피격 함수
    {
        Health -= damage;
        if (Health <= 0)
        {
            if (Life <= 1)
            {
                Death();
            }
            else
            {
                PhaseSetter(Life);
            }
        }
    }

    protected virtual void PhaseSetter(int remainLife) //페이즈 전환
    {
        if (remainLife > 1)
        {
            Health = 50f;
        }
        else
        {
            Health = 100f;
        }

    }

    protected virtual void Death() //사망 연출
    {
        Destroy(gameObject);
    }

    protected GameObject FindPlayer() //태그를 기반으로 플레이어 탐색
    {
        GameObject target = GameObject.FindWithTag("Player");
        if (target != null) Debug.Log("PlayerFound");
        //if (target != null) Debug.Log("Player Found");
        return target != null ? target : gameObject;
    }

    protected void AimingObject(GameObject obj, bool face = true) //(플레이어 탐색 후) 플레이어 바라보기
    {
        AimConstraint aimConstraint = GetComponent<AimConstraint>();
        if (aimConstraint == null)
        {
            aimConstraint = gameObject.AddComponent<AimConstraint>();
        }
        else
        {
            aimConstraint.constraintActive = false; 
            aimConstraint.RemoveSource(0);
        }

        ConstraintSource source = new ConstraintSource
        {
            sourceTransform = obj.transform,
            weight = 1.0f
        };
        aimConstraint.AddSource(source);

        if (face == true) aimConstraint.constraintActive = true;
        else aimConstraint.constraintActive = false;

        //aimConstraint.locked = true;
        aimConstraint.rotationAtRest = Vector3.zero;
        aimConstraint.worldUpType = AimConstraint.WorldUpType.SceneUp;
    }

    protected virtual Vector3 AimingWithoutLooking(GameObject obj1)
    {
        Vector3 targetDirection = (obj1.transform.position - transform.position).normalized;
        return targetDirection;
    }



    protected virtual IEnumerator ObjectRemoveTImer(GameObject Item, float Time) //대부분의 오브젝트는 맵 밖을 벗어나야 삭제됨. 테스트용이라 보면 됨
    {
        yield return new WaitForSeconds(Time);
        Destroy(Item.gameObject);
    }


    protected virtual void ResetProjectile(string tag = "EnemyAttack") //탄막 삭제
    {
        // 특정 태그를 가진 모든 오브젝트 찾기
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag(tag); //지정한 태그의 오브젝트를 전부 찾고
        Debug.Log($"Found {objectsToDestroy.Length} objects with tag {tag}");
        // 찾은 모든 오브젝트를 foreach로 제거한다
        foreach (GameObject obj in objectsToDestroy) //objectsToDestroy의 각 obj에 대하여 반복문
        {
            //Debug.Log($"Destroying object: {obj.name}");
            Destroy(obj);
            obj.transform.parent = null; //오브젝트 뒤처리
        }
    }

    protected void HitEffectGen(Collider other)
    {
        Vector3 oppositeDirection = -other.transform.forward;
        Vector3 spawnPosition = other.transform.position + oppositeDirection * 5f; //얼마나 멀리 생성할지

        Vector3 randomOffset = new Vector3(
            Random.Range(-0.5f, 0.5f), // X축 랜덤 오프셋
            Random.Range(-0.5f, 0.5f), // Y축 랜덤 오프셋
            Random.Range(-0.5f, 0.5f)  // Z축 랜덤 오프셋
        );

        // 랜덤 오프셋 적용
        Vector3 randomDirection = (oppositeDirection + randomOffset).normalized;

        if (HitEffect != null)
        {
            //Quaternion spawnRotation = Quaternion.LookRotation(oppositeDirection);
            Quaternion spawnRotation = Quaternion.LookRotation(randomDirection) * Quaternion.Euler(90f, 0f, 0f);
            GameObject spawnedEffect = Instantiate(HitEffect, spawnPosition, spawnRotation);

            Destroy(spawnedEffect, 0.3f);
        }
        else
        {
            Debug.LogWarning("Prefab to spawn is not assigned!");
        }
    }

    //공격 오브젝트 색상
    //공격 패턴
    protected virtual void BasicAttack(int projectileNum, float launchForce, float angleDivision, GameObject target, GameObject prefab, float R = 125f, float G = 125f, float B = 125f)
    {
        Vector3 targetDirection = AimingWithoutLooking(target);
        Quaternion baseRotation = Quaternion.LookRotation(targetDirection);

        // 균등하게 퍼지도록 방향 조절
        float goldenRatio = (1 + Mathf.Sqrt(5)) / 2;
        float angleIncrement = Mathf.PI * 2 * goldenRatio;

        for (int i = 0; i < projectileNum; i++)
        {
            float t = (float)i / projectileNum;
            float inclination = Mathf.Acos(1 - 2 * t) / angleDivision; // 반구 형태로 제한 (180 / ?)
            float azimuth = angleIncrement * i;

            Vector3 direction = new Vector3(
                Mathf.Sin(inclination) * Mathf.Cos(azimuth),
                Mathf.Sin(inclination) * Mathf.Sin(azimuth),
                Mathf.Cos(inclination)
            );

            // 방향을 플레이어를 향한 방향 기준으로 회전
            direction = baseRotation * direction;

            // 발사체 생성 및 발사
            Quaternion rotation = Quaternion.LookRotation(direction);
            GameObject redBall = Instantiate(prefab, transform.position, rotation);
            Rigidbody rb = redBall.GetComponent<Rigidbody>();
            AttackColor attackColor = redBall.GetComponent<AttackColor>();
            if (attackColor != null) attackColor.SetAttackColor(R, G, B);

            rb.AddForce(direction * launchForce, ForceMode.Impulse);
        }
    }

    protected virtual void BasicAttack(GameObject Spawner, int projectileNum, float launchForce, float angleDivision, GameObject target, GameObject prefab, float R = 125f, float G = 125f, float B = 125f)
    {
        //Vector3 targetDirection = (target.transform.position - Spawner.transform.position).normalized;
        // 이 버전은 그냥 주변 쏘는 느낌으로??
        Vector3 targetDirection = AimingWithoutLooking(target);

        Quaternion baseRotation = Quaternion.LookRotation(targetDirection);

        // 균등하게 퍼지도록 방향 조절
        float goldenRatio = (1 + Mathf.Sqrt(5)) / 2;
        float angleIncrement = Mathf.PI * 2 * goldenRatio;

        for (int i = 0; i < projectileNum; i++)
        {
            float t = (float)i / projectileNum;
            float inclination = Mathf.Acos(1 - 2 * t) / angleDivision; // 반구 형태로 제한 (180 / ?)
            float azimuth = angleIncrement * i;

            Vector3 direction = new Vector3(
                Mathf.Sin(inclination) * Mathf.Cos(azimuth),
                Mathf.Sin(inclination) * Mathf.Sin(azimuth),
                Mathf.Cos(inclination)
            );

            // 방향을 플레이어를 향한 방향 기준으로 회전
            direction = baseRotation * direction;

            // 발사체 생성 및 발사
            Quaternion rotation = Quaternion.LookRotation(direction);
            GameObject redBall = Instantiate(prefab, Spawner.transform.position, rotation);
            Rigidbody rb = redBall.GetComponent<Rigidbody>();
            AttackColor attackColor = redBall.GetComponent<AttackColor>();
            if (attackColor != null) attackColor.SetAttackColor(R, G, B);

            rb.AddForce(direction * launchForce, ForceMode.Impulse);
        }
    }

    protected virtual void BasicAttack(Transform Spawner, int projectileNum, float launchForce, float angleDivision, GameObject target, GameObject prefab, float R = 125f, float G = 125f, float B = 125f)
    {
        Vector3 targetDirection = (target.transform.position - Spawner.position).normalized;
        Quaternion baseRotation = Quaternion.LookRotation(targetDirection);

        // 균등하게 퍼지도록 방향 조절
        float goldenRatio = (1 + Mathf.Sqrt(5)) / 2;
        float angleIncrement = Mathf.PI * 2 * goldenRatio;

        for (int i = 0; i < projectileNum; i++)
        {
            float t = (float)i / projectileNum;
            float inclination = Mathf.Acos(1 - 2 * t) / angleDivision; // 반구 형태로 제한 (180 / ?)
            float azimuth = angleIncrement * i;

            Vector3 direction = new Vector3(
                Mathf.Sin(inclination) * Mathf.Cos(azimuth),
                Mathf.Sin(inclination) * Mathf.Sin(azimuth),
                Mathf.Cos(inclination)
            );

            // 방향을 플레이어를 향한 방향 기준으로 회전
            direction = baseRotation * direction;

            // 발사체 생성 및 발사
            Quaternion rotation = Quaternion.LookRotation(direction);
            GameObject redBall = Instantiate(prefab, Spawner.position, rotation);
            Rigidbody rb = redBall.GetComponent<Rigidbody>();
            AttackColor attackColor = redBall.GetComponent<AttackColor>();
            if (attackColor != null) attackColor.SetAttackColor(R, G, B);

            rb.AddForce(direction * launchForce, ForceMode.Impulse);
        }
    }

    protected virtual void BasicAttack(int projectileNum, float launchForce, GameObject prefab, float R = 125f, float G = 125f, float B = 125f)
    {
        // 자기 자신을 기준으로 구의 형태로 그대로 발사
        // 균등하게 퍼지도록 방향 조절
        float goldenRatio = (1 + Mathf.Sqrt(5)) / 2;
        float angleIncrement = Mathf.PI * 2 * goldenRatio;

        for (int i = 0; i < projectileNum; i++)
        {
            float t = (float)i / projectileNum;
            float inclination = Mathf.Acos(1 - 2 * t); 
            float azimuth = angleIncrement * i;

            Vector3 direction = new Vector3(
                Mathf.Sin(inclination) * Mathf.Cos(azimuth),
                Mathf.Sin(inclination) * Mathf.Sin(azimuth),
                Mathf.Cos(inclination)
            );

            // 발사체 생성 및 발사
            Quaternion rotation = Quaternion.LookRotation(direction);
            GameObject redBall = Instantiate(prefab, transform.position, rotation);
            Rigidbody rb = redBall.GetComponent<Rigidbody>();
            AttackColor attackColor = redBall.GetComponent<AttackColor>();
            if (attackColor != null)
                attackColor.SetAttackColor(R, G, B);

            rb.AddForce(direction * launchForce, ForceMode.Impulse);
        }
    }


    protected void SingleShot(float launchForce, GameObject prefab, GameObject target, float R = 125f, float G = 125f, float B = 125f)
    {
        Vector3 targetDirection = AimingWithoutLooking(target);
        Quaternion rotation = Quaternion.LookRotation(targetDirection);
        GameObject redBall = Instantiate(prefab, transform.position, rotation);
        Rigidbody rb = redBall.GetComponent<Rigidbody>();
        rb.AddForce(targetDirection * launchForce, ForceMode.Impulse);
        AttackColor attackColor = redBall.GetComponent<AttackColor>();
        if (attackColor != null) attackColor.SetAttackColor(R, G, B);
    }

    protected void SingleShot(GameObject spawner, float launchForce, GameObject prefab, GameObject target, float R = 125f, float G = 125f, float B = 125f)
    {
        Vector3 targetDirection = (target.transform.position - spawner.transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(targetDirection);
        GameObject redBall = Instantiate(prefab, spawner.transform.position, rotation);
        Rigidbody rb = redBall.GetComponent<Rigidbody>();
        rb.AddForce(targetDirection * launchForce, ForceMode.Impulse);
        AttackColor attackColor = redBall.GetComponent<AttackColor>();
        if (attackColor != null) attackColor.SetAttackColor(R, G, B);
    }

    protected void SingleShot(Transform spawner, float launchForce, GameObject prefab, GameObject target, float R = 125f, float G = 125f, float B = 125f)
    {
        Vector3 targetDirection = (target.transform.position - spawner.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(targetDirection);
        GameObject redBall = Instantiate(prefab, spawner.position, rotation);
        Rigidbody rb = redBall.GetComponent<Rigidbody>();
        rb.AddForce(targetDirection * launchForce, ForceMode.Impulse);
        AttackColor attackColor = redBall.GetComponent<AttackColor>();
        if (attackColor != null) attackColor.SetAttackColor(R, G, B);
    }

    protected void SingleShot(Transform spawner, float launchForce, GameObject prefab, Transform target, float R = 125f, float G = 125f, float B = 125f)
    {
        Vector3 targetDirection = (target.position - spawner.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(targetDirection);
        GameObject redBall = Instantiate(prefab, spawner.position, rotation);
        Rigidbody rb = redBall.GetComponent<Rigidbody>();
        rb.AddForce(targetDirection * launchForce, ForceMode.Impulse);
        AttackColor attackColor = redBall.GetComponent<AttackColor>();
        if (attackColor != null) attackColor.SetAttackColor(R, G, B);
    }


    protected virtual void BasicSpin(int projectileNum, float launchForce, GameObject prefab, float rotationSpeed, float R = 125f, float G = 125f, float B = 125f)
    {
        //Vector3 rotationAxis = transform.position;

        float goldenRatio = (1 + Mathf.Sqrt(5)) / 2;
        float angleIncrement = Mathf.PI * 2 * goldenRatio;

        for (int i = 0; i < projectileNum; i++)
        {
            // 구의 표면에서 발사 방향 계산
            float t = (float)i / projectileNum;
            float inclination = Mathf.Acos(1 - 2 * t);
            float azimuth = angleIncrement * i;

            Vector3 direction = new Vector3(
                Mathf.Sin(inclination) * Mathf.Cos(azimuth),
                Mathf.Sin(inclination) * Mathf.Sin(azimuth),
                Mathf.Cos(inclination)
            );

            Quaternion rotation = Quaternion.LookRotation(direction);
            GameObject redBall = Instantiate(prefab, transform.position, rotation);

            Rigidbody rb = redBall.GetComponent<Rigidbody>();
            rb.AddForce(direction * launchForce, ForceMode.Impulse);
            AttackColor attackColor = redBall.GetComponent<AttackColor>();
            if (attackColor != null) attackColor.SetAttackColor(R, G, B);

            // 발사체에 회전 궤적 추가
            StartCoroutine(RotateAroundCenter(redBall.transform, rotationSpeed));
        }
    }

    protected IEnumerator RotateAroundCenter(Transform obj,  float speed)
    {
        while (obj != null && obj.transform != null) // obj와 transform이 null인지 확인
        {
            // 구의 중심을 기준으로 회전
            obj.transform.RotateAround(transform.position, Vector3.up, speed * Time.deltaTime);

            yield return null; // 다음 프레임까지 대기
        }
    }

    protected virtual void ShootAround(GameObject target, int num, GameObject prefab, float radius, float speed, float randSpeed, float R = 125f, float G = 125f, float B = 125f)
    {
        Vector3 targetPosition = target.transform.position;

        for (int i = 0; i < num; i++)
        {
            GameObject projectile = Instantiate(prefab, transform.position, Quaternion.identity);

            //주변을 향해 발사
            Vector3 randomOffset = Random.onUnitSphere * radius; //(5: 반지름 크기)
            Vector3 targetDirection = (targetPosition + randomOffset - transform.position).normalized;
            
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            AttackColor attackColor = projectile.GetComponent<AttackColor>();
            if (attackColor != null) attackColor.SetAttackColor(R, G, B);

            float randomSpeedFactor = Random.Range(1 - randSpeed, 1 + randSpeed);
            rb.velocity = targetDirection * speed * randomSpeedFactor;
        }

    }

    protected virtual void ShootAround(GameObject spawner, GameObject target, int num, GameObject prefab, float radius, float speed, float randSpeed, float R = 125f, float G = 125f, float B = 125f)
    {
        Vector3 targetPosition = target.transform.position;

        for (int i = 0; i < num; i++)
        {
            GameObject projectile = Instantiate(prefab, spawner.transform.position, Quaternion.identity);

            //주변을 향해 발사
            Vector3 randomOffset = Random.onUnitSphere * radius; //(5: 반지름 크기)
            Vector3 targetDirection = (targetPosition + randomOffset - spawner.transform.position).normalized;

            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            AttackColor attackColor = projectile.GetComponent<AttackColor>();
            if (attackColor != null) attackColor.SetAttackColor(R, G, B);

            float randomSpeedFactor = Random.Range(1 - randSpeed, 1 + randSpeed);
            rb.velocity = targetDirection * speed * randomSpeedFactor;
        }

    }

    protected virtual void ShootAround(Transform spawner, GameObject target, int num, GameObject prefab, float radius, float speed, float randSpeed, float R = 125f, float G = 125f, float B = 125f)
    {
        Vector3 targetPosition = target.transform.position;

        for (int i = 0; i < num; i++)
        {
            GameObject projectile = Instantiate(prefab, spawner.position, Quaternion.identity);

            //주변을 향해 발사
            Vector3 randomOffset = Random.onUnitSphere * radius; //(5: 반지름 크기)
            Vector3 targetDirection = (targetPosition + randomOffset - spawner.position).normalized;

            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            AttackColor attackColor = projectile.GetComponent<AttackColor>();
            if (attackColor != null) attackColor.SetAttackColor(R, G, B);

            float randomSpeedFactor = Random.Range(1 - randSpeed, 1 + randSpeed);
            rb.velocity = targetDirection * speed * randomSpeedFactor;
        }

    }


    protected virtual void ShootLasers(GameObject target, int num, GameObject prefab, float radius , float R = 125f, float G = 125f, float B = 125f)
    {
        
        Vector3 targetPosition = target.transform.position;
        for (int i = 0; i < num - 1; i++)
        {
            GameObject laser = Instantiate(prefab, transform.position + transform.up * 1f, transform.rotation * Quaternion.Euler(0, 0, 0));
            
            //AttackColor attackColor = laser.GetComponent<AttackColor>();
            //attackColor.SetAttackColor(R, G, B);
            
            //주변을 향해 발사
            Vector3 randomOffset = Random.onUnitSphere * radius; //(5: 반지름 크기)
            //randomOffset.y = 0;
            Vector3 targetDirection = -(targetPosition + randomOffset - transform.position).normalized;

            laser.transform.forward = targetDirection;
            
        }
        GameObject laser0 = Instantiate(prefab, transform.position + transform.up * 1f, transform.rotation * Quaternion.Euler(0, 0, 0));
        Vector3 directTargetDirection = -(targetPosition - transform.position).normalized;
        laser0.transform.forward = directTargetDirection;
    }

    protected virtual void ShootLasers(GameObject spawner, GameObject target, int num, GameObject prefab, float radius, float R = 125f, float G = 125f, float B = 125f)
    {

        Vector3 targetPosition = target.transform.position;
        for (int i = 0; i < num - 1; i++)
        {
            GameObject laser = Instantiate(prefab, spawner.transform.position + spawner.transform.up * 1f, spawner.transform.rotation * Quaternion.Euler(0, 0, 0));

            //AttackColor attackColor = laser.GetComponent<AttackColor>();
            //attackColor.SetAttackColor(R, G, B);

            //주변을 향해 발사
            Vector3 randomOffset = Random.onUnitSphere * radius; //(5: 반지름 크기)
            //randomOffset.y = 0;
            Vector3 targetDirection = -(targetPosition + randomOffset - spawner.transform.position).normalized;

            laser.transform.forward = targetDirection;

        }
        GameObject laser0 = Instantiate(prefab, spawner.transform.position + spawner.transform.up * 1f, spawner.transform.rotation * Quaternion.Euler(0, 0, 0));
        Vector3 directTargetDirection = -(targetPosition - spawner.transform.position).normalized;
        laser0.transform.forward = directTargetDirection;
    }

    protected virtual void ShootLasers(Transform spawner, GameObject target, int num, GameObject prefab, float radius, float R = 125f, float G = 125f, float B = 125f)
    {

        Vector3 targetPosition = target.transform.position;
        for (int i = 0; i < num - 1; i++)
        {
            GameObject laser = Instantiate(prefab, spawner.transform.position + spawner.transform.up * 1f, spawner.transform.rotation * Quaternion.Euler(0, 0, 0));

            //AttackColor attackColor = laser.GetComponent<AttackColor>();
            //attackColor.SetAttackColor(R, G, B);

            //주변을 향해 발사
            Vector3 randomOffset = Random.onUnitSphere * radius; //(5: 반지름 크기)
            //randomOffset.y = 0;
            Vector3 targetDirection = -(targetPosition + randomOffset - spawner.position).normalized;

            laser.transform.forward = targetDirection;

        }
        GameObject laser0 = Instantiate(prefab, spawner.position + spawner.up * 1f, spawner.rotation * Quaternion.Euler(0, 0, 0));
        Vector3 directTargetDirection = -(targetPosition - spawner.position).normalized;
        laser0.transform.forward = directTargetDirection;
    }

    protected virtual void AttackBlocks(GameObject prefab, Vector3 from, Vector3 to, float gap = 5f, float speed = 30)
    {
        float x, y, z;
        x = from.x;

        Vector3 direction = new Vector3(0, 0, 1);

        while (x >= to.x)
        {
            y = from.y;
            while (y >= to.y)
            {
                z = from.z;
                while (z >= to.z)
                {
                    Vector3 spawnPosition = new Vector3(x, y, z);
                    GameObject Attack = Instantiate(prefab, spawnPosition, Quaternion.identity);
                    Rigidbody rb = Attack.GetComponent<Rigidbody>();

                    rb.AddForce(direction * speed, ForceMode.Impulse);
                    z -= gap;
                }
                y -= gap;
            }
            x -= gap;
        }
    }

    protected virtual void SlowdownAttack(int projectileNum, float launchForce, float angleDivision, GameObject target, GameObject prefab, float R = 125f, float G = 125f, float B = 125f, float afterSpeedPercent = 0.1f, float delay = 0.5f, bool ifInstance = false)
    {
        Vector3 targetDirection = AimingWithoutLooking(target);
        Quaternion baseRotation = Quaternion.LookRotation(targetDirection);

        // 균등하게 퍼지도록 방향 조절
        float goldenRatio = (1 + Mathf.Sqrt(5)) / 2;
        float angleIncrement = Mathf.PI * 2 * goldenRatio;

        for (int i = 0; i < projectileNum; i++)
        {
            float t = (float)i / projectileNum;
            float inclination = Mathf.Acos(1 - 2 * t) / angleDivision; // 반구 형태로 제한 (180 / ?)
            float azimuth = angleIncrement * i;

            Vector3 direction = new Vector3(
                Mathf.Sin(inclination) * Mathf.Cos(azimuth),
                Mathf.Sin(inclination) * Mathf.Sin(azimuth),
                Mathf.Cos(inclination)
            );

            // 방향을 플레이어를 향한 방향 기준으로 회전
            direction = baseRotation * direction;

            // 발사체 생성 및 발사
            Quaternion rotation = Quaternion.LookRotation(direction);
            GameObject redBall = Instantiate(prefab, transform.position, rotation);
            Rigidbody rb = redBall.GetComponent<Rigidbody>();
            AttackColor attackColor = redBall.GetComponent<AttackColor>();
            if (attackColor != null) attackColor.SetAttackColor(R, G, B);

            rb.AddForce(direction * launchForce, ForceMode.Impulse);

            StartCoroutine(SlowDown(rb, afterSpeedPercent, delay, ifInstance));
        }
    }

    protected virtual void SlowdownAttack(GameObject spawner, int projectileNum, float launchForce, float angleDivision, GameObject target, GameObject prefab, float R = 125f, float G = 125f, float B = 125f, float afterSpeedPercent = 0.1f, float delay = 0.5f, bool ifInstance = false)
    {
        Vector3 targetDirection = (target.transform.position - spawner.transform.position).normalized;
        Quaternion baseRotation = Quaternion.LookRotation(targetDirection);

        // 균등하게 퍼지도록 방향 조절
        float goldenRatio = (1 + Mathf.Sqrt(5)) / 2;
        float angleIncrement = Mathf.PI * 2 * goldenRatio;

        for (int i = 0; i < projectileNum; i++)
        {
            float t = (float)i / projectileNum;
            float inclination = Mathf.Acos(1 - 2 * t) / angleDivision; // 반구 형태로 제한 (180 / ?)
            float azimuth = angleIncrement * i;

            Vector3 direction = new Vector3(
                Mathf.Sin(inclination) * Mathf.Cos(azimuth),
                Mathf.Sin(inclination) * Mathf.Sin(azimuth),
                Mathf.Cos(inclination)
            );

            // 방향을 플레이어를 향한 방향 기준으로 회전
            direction = baseRotation * direction;

            // 발사체 생성 및 발사
            Quaternion rotation = Quaternion.LookRotation(direction);
            GameObject redBall = Instantiate(prefab, spawner.transform.position, rotation);
            Rigidbody rb = redBall.GetComponent<Rigidbody>();
            AttackColor attackColor = redBall.GetComponent<AttackColor>();
            if (attackColor != null) attackColor.SetAttackColor(R, G, B);

            rb.AddForce(direction * launchForce, ForceMode.Impulse);

            StartCoroutine(SlowDown(rb, afterSpeedPercent, delay, ifInstance));
        }
    }

    protected virtual void SlowdownAttack(Transform spawner, int projectileNum, float launchForce, float angleDivision, GameObject target, GameObject prefab, float R = 125f, float G = 125f, float B = 125f, float afterSpeedPercent = 0.1f, float delay = 0.5f, bool ifInstance = false)
    {
        Vector3 targetDirection = (target.transform.position - spawner.position).normalized;
        Quaternion baseRotation = Quaternion.LookRotation(targetDirection);

        // 균등하게 퍼지도록 방향 조절
        float goldenRatio = (1 + Mathf.Sqrt(5)) / 2;
        float angleIncrement = Mathf.PI * 2 * goldenRatio;

        for (int i = 0; i < projectileNum; i++)
        {
            float t = (float)i / projectileNum;
            float inclination = Mathf.Acos(1 - 2 * t) / angleDivision; // 반구 형태로 제한 (180 / ?)
            float azimuth = angleIncrement * i;

            Vector3 direction = new Vector3(
                Mathf.Sin(inclination) * Mathf.Cos(azimuth),
                Mathf.Sin(inclination) * Mathf.Sin(azimuth),
                Mathf.Cos(inclination)
            );

            // 방향을 플레이어를 향한 방향 기준으로 회전
            direction = baseRotation * direction;

            // 발사체 생성 및 발사
            Quaternion rotation = Quaternion.LookRotation(direction);
            GameObject redBall = Instantiate(prefab, spawner.position, rotation);
            Rigidbody rb = redBall.GetComponent<Rigidbody>();
            AttackColor attackColor = redBall.GetComponent<AttackColor>();
            if (attackColor != null) attackColor.SetAttackColor(R, G, B);

            rb.AddForce(direction * launchForce, ForceMode.Impulse);

            StartCoroutine(SlowDown(rb, afterSpeedPercent, delay, ifInstance));
        }
    }

    private IEnumerator SlowDown(Rigidbody rb, float afterSpeedPercent = 0.1f, float delay = 0.5f, bool ifInstance = false)
    {
        yield return new WaitForSeconds(delay);

        float elapsedTime = 0f;
        float slowDuration = 0.5f;

        Vector3 initialVelocity = rb.velocity; //초기 속도

        
        if (!ifInstance)
        {
            while (elapsedTime < slowDuration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / slowDuration;

                rb.velocity = Vector3.Lerp(initialVelocity, initialVelocity * afterSpeedPercent, t);

                yield return null;
            }
        }
        else
        {
            rb.velocity *= afterSpeedPercent;
        }

    }

    //이동 관련
    protected IEnumerator ObjectMover(GameObject target, Vector3 from, Vector3 to, float time)   //Stage에도 동일한 스크립트 존재
    {
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;

            // 이동 비율 계산
            float t = Mathf.Clamp01(elapsedTime / time); //균일한 속도로 이동
            t = 1f - Mathf.Pow(1f - t, 2f); // Ease-Out 적용

            //float t = Mathf.SmoothStep(0f, 1f, elapsedTime / time); //느려지면서 이동

            // 위치 보간
            target.transform.position = Vector3.Lerp(from, to, t);

            // 다음 프레임까지 대기
            yield return null;
        }
    }

    protected IEnumerator ObjectMover(GameObject target, Vector3 to, float time)   //오버로딩, from = 원래 자리, 역시 Stage에도 존재
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

    protected IEnumerator ObjectMover(Vector3 to, float time)   //from = 원래 자리, 자기 자신을 이동하므로 Stage에는 존재하진 않음
    {
        float elapsedTime = 0f;
        Vector3 from = gameObject.transform.position;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;

            // 이동 비율 계산
            float t = Mathf.Clamp01(elapsedTime / time); //균일한 속도로 이동
            t = 1f - Mathf.Pow(1f - t, 2f); // Ease-Out 적용

            // 위치 보간
            gameObject.transform.position = Vector3.Lerp(from, to, t);

            // 다음 프레임까지 대기
            yield return null;

        }
    }

    protected void RandomMove(float distance, float duration)
    {
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);

        Vector3 randomDirection = new Vector3(randomX, randomY, randomZ).normalized;
        Vector3 targetPosition = transform.position + randomDirection * distance;

        int maxrange = 45;

        if (targetPosition.x > maxrange) targetPosition.x = maxrange;
        if (targetPosition.y > maxrange) targetPosition.y = maxrange;
        if (targetPosition.z > maxrange) targetPosition.z = maxrange;
        if (targetPosition.x < -maxrange) targetPosition.x = -maxrange;
        if (targetPosition.y < -maxrange) targetPosition.y = -maxrange;
        if (targetPosition.z < -maxrange) targetPosition.z = -maxrange;


        StartCoroutine(ObjectMover(targetPosition, duration));

    }

    protected void PlaySFX(int index, float delayTime = 0f)
    {
        if (BGM_Script != null)
            BGM_Script.PlaySFX(index);
        else Debug.Log("Sound not able");
    }

    protected IEnumerator RotateObject(float time, float x, float y, float z)
    {
        Vector3 targetRotation = new Vector3(x, y, z);
        Vector3 rotationAxis = Vector3.up; // 회전 방향

        yield return new WaitForSeconds(time);
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(transform.eulerAngles + targetRotation);


        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / time);
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            yield return null;
        }
        transform.rotation = endRotation; // 최종 각도로 설정
    }

    /*
    private void OnDisable()
    {
        // 자동 반환 (예시)
        PoolManager.Instance.ReturnToPool("Enemy", gameObject);
    }
    */
}
