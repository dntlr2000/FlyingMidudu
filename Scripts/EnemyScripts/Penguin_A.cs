using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penguin_A : Enemy_Minion
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
        //AimingObject(player);
        //attack = StartCoroutine(AttackPattern());

        base.Start();
    }

    protected override IEnumerator AttackPattern()
    {

        yield return new WaitForSeconds(1.5f);


        for (int i = 0; i < 10; i++)
        {
            RandomMove(15, 2f);
            PlaySFX(4);
            BasicAttack(20, 30, 4, player, AttackPrefab[0]);
            yield return new WaitForSeconds(2f);

        }
        StartCoroutine(ObjectMover(new Vector3(transform.position.x, transform.position.y, -100), 4f));
        //RandomMove(2, 2f);
    }
}
