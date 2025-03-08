using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MulbumB : Enemy_Minion
{
    protected override void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        Life = 1;
        Health = 10f;

        player = FindPlayer();
        if (player == null) return;
        //AimingObject(player);
        //attack = StartCoroutine(AttackPattern());

        base.Start();
    }

    // Update is called once per frame
    protected override IEnumerator AttackPattern()
    {

        if (gameObject.transform.position.x < 0)
        {
            StartCoroutine(RotateObject(0.5f, 0, 60, 0));
            yield return new WaitForSeconds(1f);
            StartCoroutine(ObjectMover(new Vector3(transform.position.x + 180, transform.position.y, transform.position.z + 120), 7f));

        }
        else
        {
            StartCoroutine(RotateObject(0.5f, 0, -60, 0));
            yield return new WaitForSeconds(1f);
            StartCoroutine(ObjectMover(new Vector3(transform.position.x - 180, transform.position.y, transform.position.z + 120), 7f));

        }
        animator.SetBool("ifDash", true);
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(2f); ;

            PlaySFX(5);
            BasicAttack(10, 10, 3, player, AttackPrefab[0]);
        }

    }
    protected override void Death() //잡몹은 생성한 공격 오브젝트를 삭제하지 않고 그냥 삭제
    {
        ShootAround(player, 10, AttackPrefab[1], 20f, 20, 0.4f);
        base.Death();
    }
}
