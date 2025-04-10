using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PugB : Enemy_Minion
{
    protected override void Start()
    {
        //Debug.Log("Minion spawned");
        Rigidbody rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        Life = 1;
        Health = 60f;
        player = FindPlayer();
        if (player == null) return;
        //AimingObject(player);


        base.Start();


    }

    protected override IEnumerator AttackPattern()
    {
        yield return new WaitForSeconds(0.5f);

        if (gameObject.transform.position.x < 0)
        {
            StartCoroutine(RotateObject(0.5f, 0, 90, 0));
            StartCoroutine(ObjectMover(new Vector3(transform.position.x + 100, transform.position.y, transform.position.z), 15f));
        }

        else
        {
            StartCoroutine(RotateObject(0.5f, 0, -90, 0));
            StartCoroutine(ObjectMover(new Vector3(transform.position.x - 100, transform.position.y, transform.position.z), 15f));
        }

        yield return new WaitForSeconds(0.5f);

        for (int k = 0; k < 3; k++)
        {
            for (int i = 0; i < 20; i++)
            {
                //BasicAttack(80, 20, 2, player, AttackPrefab[0], 171, 36, 191);
                BasicAttack(80, 30, AttackPrefab[0], 116, 3, 179);
                PlaySFX(4);
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(1f);
        }
        

        StartCoroutine(ObjectMover(transform.position + new Vector3(0, 0, -100), 1f));
    }
}
