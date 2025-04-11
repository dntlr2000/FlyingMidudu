using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangaeI : Enemy_Minion
{
    // Start is called before the first frame update
    protected override void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        Life = 1;
        Health = 40f;

        player = FindPlayer();
        if (player == null) return;
        AimingObject(player);
        //attack = StartCoroutine(AttackPattern());

        base.Start();

    }

    protected override IEnumerator AttackPattern()
    {

        yield return new WaitForSeconds(0.5f);

        if (gameObject.transform.position.y < 0)
        {
            StartCoroutine(RotateObject(0.5f, 0, 90, 0));
            StartCoroutine(ObjectMover(new Vector3(transform.position.x - 10, transform.position.y + 80, transform.position.z), 12f));
        }

        else
        {
            StartCoroutine(RotateObject(0.5f, 0, -90, 0));
            StartCoroutine(ObjectMover(new Vector3(transform.position.x + 10, transform.position.y - 80, transform.position.z), 12f));
        }

        //yield return new WaitForSeconds(0.5f);

        for (int k = 0; k < 10; k++)
        {
            for (int i = 0; i < 5; i++)
            {
                SingleShot(90, AttackPrefab[0], player, 232, 23, 23);
                SingleShot(80, AttackPrefab[0], player, 232, 23, 23);
                SingleShot(70, AttackPrefab[0], player, 232, 23, 23);
                PlaySFX(4);
                yield return new WaitForSeconds(0.2f);
            }
            BasicAttack(20, 70, 4, player, AttackPrefab[1], 240, 152, 9);
            yield return null;
        }

        StartCoroutine(ObjectMover(transform.position + new Vector3(0, 0, -100), 1f));
    }
}
