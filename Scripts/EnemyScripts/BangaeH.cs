using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangaeH : Enemy_Minion
{
    protected override void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        Life = 1;
        Health = 80f;

        player = FindPlayer();
        if (player == null) return;
        AimingObject(player);

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

        for (int i = 0; i < 12; i++)
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
        for (int i = 0; i < 30; i++)
        {
            yield return new WaitForSeconds(0.15f);
            PlaySFX(4);
            BasicAttack(10, 80, 12, player, AttackPrefab[0], r, g, b);
        }

        StartCoroutine(ObjectMover(new Vector3(transform.position.x, transform.position.y, -100), 2f));
    }
}
