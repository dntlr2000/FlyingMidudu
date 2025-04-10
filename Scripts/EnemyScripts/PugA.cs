using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PugA : Enemy_Minion
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
       // AimingObject(player);


        base.Start();


    }

    protected override IEnumerator AttackPattern()
    {
        yield return new WaitForSeconds(0.5f);

        if (gameObject.transform.position.x < 0)
        {
            StartCoroutine(RotateObject(0.5f, 0, 90, 0));
            StartCoroutine(ObjectMover(new Vector3(transform.position.x + 100, transform.position.y, transform.position.z), 12f));
        }

        else
        {
            StartCoroutine(RotateObject(0.5f, 0, -90, 0));
            StartCoroutine(ObjectMover(new Vector3(transform.position.x - 100, transform.position.y, transform.position.z), 12f));
        }

        yield return new WaitForSeconds(0.5f);

        while (true) {
            for (int i = 1; i < 12; i++)
            {
                BasicAttack(15, 8 * i, 4, player ,AttackPrefab[0] ,242, 239, 42);
                PlaySFX(4);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(1f);
        }
        
    }
}
