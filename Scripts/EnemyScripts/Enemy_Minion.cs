using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Enemy_Minion : Enemy
{
    protected Coroutine attack;
    public GameObject deathEffect;

    public GameObject[] AttackPrefab;

    protected GameObject player;

    // Start is called before the first frame update
    protected override void Start()
    {
        //Debug.Log("Minion spawned");
        //Rigidbody rb = GetComponent<Rigidbody>();
        //animator = GetComponent<Animator>();

        //Life = 1;
        //Health = 200f;
        
        //player = FindPlayer();
        //if (player == null) return;
        //AimingObject(player);
        

        attack = StartCoroutine(AttackPattern());
        BGM_Script = FindObjectOfType<BGMController>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    
    protected virtual IEnumerator AttackPattern()  //예시, 이후 파생 클래스에서 오버라이드해서 제대로 된 패턴 구현
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Attack started");
        //while (true)
        for (int i = 0; i < 10; i++)
        {
            Debug.Log("Attack!");
           yield return new WaitForSeconds(3f);

        }
        Debug.Log("Attack ended");
    }

    protected override void Death() //잡몹은 생성한 공격 오브젝트를 삭제하지 않고 그냥 삭제
    {
        Debug.Log("Minion died");
        //StopCoroutine(attack);
        //작은 규모로 터지는 이펙트 추가 예정
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        PlaySFX(1);
        Destroy(gameObject);
        
    }
}
