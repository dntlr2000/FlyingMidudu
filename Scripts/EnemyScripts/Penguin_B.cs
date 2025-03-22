using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penguin_B : Enemy_Minion { 
     // Start is called before the first frame update
    protected override void Start()
    {
    Rigidbody rb = GetComponent<Rigidbody>();
    animator = GetComponent<Animator>();

    Life = 1;
    Health = 30f;

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
            StartCoroutine(ObjectMover(new Vector3(transform.position.x + 160, transform.position.y, transform.position.z), 10f));

        }
        else
        {
            //Debug.Log("Right to Left");
            animator.SetInteger("State", 2);
            StartCoroutine(ObjectMover(new Vector3(transform.position.x - 160, transform.position.y, transform.position.z), 10f));

        }
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(1f); ;

            PlaySFX(5);
            BasicAttack(30, 40, 3, player, AttackPrefab[0], 240, 255, 49);
        }
        
    }

    /*
    protected override void Death() //잡몹은 생성한 공격 오브젝트를 삭제하지 않고 그냥 삭제
    {
        ShootAround(player, 5, AttackPrefab[1], 10f, 30, 0.3f);
        base.Death();
    }
    */
}
