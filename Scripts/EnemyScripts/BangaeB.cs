using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangaeB : Enemy_Minion
{

    // Start is called before the first frame update
    protected override void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        Life = 1;
        Health = 5f;

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
            //Debug.Log("Left to Right");
            animator.SetInteger("State", 1);
            StartCoroutine(ObjectMover(new Vector3(transform.position.x + 120, transform.position.y, transform.position.z), 5f));
            yield return new WaitForSeconds(5f);
            StartCoroutine(ObjectMover(new Vector3(transform.position.x -40, transform.position.y - 20, transform.position.z + 20), 2f));
            yield return new WaitForSeconds(2f);
            StartCoroutine(ObjectMover(new Vector3(transform.position.x + 80, transform.position.y +20, transform.position.z -50), 5f));

        }
        else
        {
            //Debug.Log("Right to Left");
            animator.SetInteger("State", 2);
            StartCoroutine(ObjectMover(new Vector3(transform.position.x - 120, transform.position.y, transform.position.z), 5f));
            yield return new WaitForSeconds(5f);
            StartCoroutine(ObjectMover(new Vector3(transform.position.x + 40, transform.position.y - 20, transform.position.z + 20), 5f));
            yield return new WaitForSeconds(2f);
            StartCoroutine(ObjectMover(new Vector3(transform.position.x - 80, transform.position.y + 20, transform.position.z -50), 5f));
        }
        yield return new WaitForSeconds(1f);
        PlaySFX(5);
        BasicAttack(60, 40, 2, player, AttackPrefab[0]);
    }

    protected override void Death() //잡몹은 생성한 공격 오브젝트를 삭제하지 않고 그냥 삭제
    {
        ShootAround(player, 5, AttackPrefab[1], 10f, 30, 0.3f);
        base.Death();
    }
}
