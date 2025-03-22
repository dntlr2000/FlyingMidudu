using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangaeF : Enemy_Minion
{
    // Start is called before the first frame update
    protected override void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        Life = 1;
        Health = 100f;

        player = FindPlayer();
        if (player == null) return;
        AimingObject(player);
        //attack = StartCoroutine(AttackPattern());

        base.Start();

    }

    protected override IEnumerator AttackPattern()
    {

        for (int k = 0; k < 5; k++)
        {
            yield return new WaitForSeconds(1.5f);
            for (int i = 0; i < 5; i++)
            {
                yield return new WaitForSeconds(0.2f);
                PlaySFX(4);
                //BGM_Script.PlaySFX(3);
                //SingleShot(40f, AttackPrefab[0], player);
                //SlowdownAttack(30, 20, 4, player, AttackPrefab[0], 4, 1f);
                BasicAttack(20, 60, 6, player, AttackPrefab[0], 255, 103, 136);
            }

            RandomMove(20, 3f);
        }
        


        StartCoroutine(ObjectMover(new Vector3(transform.position.x, transform.position.y, -100), 4f));
    }
}