using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTestScript : Enemy_Minion
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
        yield return new WaitForSeconds(1.5f);

        while(true)
        {
            SlowdownAttack(30, 70, 3, player, AttackPrefab[0]);

            yield return new WaitForSeconds(1.5f);
        }
    }
}
