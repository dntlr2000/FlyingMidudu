using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangaeH : Enemy_Minion
{
    // Start is called before the first frame update
    protected override void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        Life = 1;
        Health = 150f;

        player = FindPlayer();
        if (player == null) return;
        AimingObject(player);
        //attack = StartCoroutine(AttackPattern());

        base.Start();

    }

    protected override IEnumerator AttackPattern()
    {

        yield return new WaitForSeconds(1f);

        int r = 0;
        int g = 0;
        int b = 0;

        if(transform.position.x > 0)
        {
            r = 232;
            g = 23;
            b = 23;
        }

        else
        {
            r = 23;
            g = 23;
            b = 232;
        }

        for (int i = 0; i < 8; i++)
        {
            yield return new WaitForSeconds(0.15f);
            PlaySFX(4);
            BasicAttack(10, 80, 2, player, AttackPrefab[0], r, g, b);
        }
        for (int i = 1; i < 19; i+=2)
        {
            yield return new WaitForSeconds(0.15f);
            PlaySFX(4);
            BasicAttack(10, 80, 2 + i, player, AttackPrefab[0], r, g, b);
        }
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.15f);
            BasicAttack(10, 80, 12, player, AttackPrefab[0], r, g, b);
        }

        StartCoroutine(ObjectMover(new Vector3(transform.position.x, transform.position.y, -100), 2f));
        //RandomMove(2, 2f);
    }
}
