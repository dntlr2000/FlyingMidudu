using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangaeJ : Enemy_Minion
{

    protected override void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        Life = 1;
        Health = 40f;

        player = FindPlayer();
        if (player == null) return;
        //AimingObject(player);
        //attack = StartCoroutine(AttackPattern());

        base.Start();
    }

    // Update is called once per frame
    protected override IEnumerator AttackPattern()
    {
        int R, G, B;

        if (gameObject.transform.position.x < 0)
        {
            //Debug.Log("Left to Right");
            animator.SetInteger("State", 1);
            StartCoroutine(ObjectMover(new Vector3(transform.position.x + 100, transform.position.y, transform.position.z), 10f));
            R = 232;
            G = 32;
            B = 32;

        }
        else
        {
            //Debug.Log("Right to Left");
            animator.SetInteger("State", 2);
            StartCoroutine(ObjectMover(new Vector3(transform.position.x - 100, transform.position.y, transform.position.z), 10f));
            R = 32;
            G = 32;
            B = 232;
        }
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 10; i++)
        {
            ShootAround(player, 20, AttackPrefab[0], 5, 90, 0.1f, R, G, B);
            PlaySFX(5);
            yield return new WaitForSeconds(1f);
        }

        StartCoroutine(ObjectMover(new Vector3(transform.position.x, transform.position.y, -100), 2f));
    }

    protected override void Death() //잡몹은 생성한 공격 오브젝트를 삭제하지 않고 그냥 삭제
    {
        SlowdownAttack(20, 80, 4, player, AttackPrefab[1], 212, 110, 110, 0.2f, 0.2f);
        PlaySFX(4);
        base.Death();
    }
}
