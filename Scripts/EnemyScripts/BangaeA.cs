using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangaeA : Enemy_Minion
{
    // Start is called before the first frame update
    protected override void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        Life = 1;
        Health = 30f;

        player = FindPlayer();
        if (player == null) return;
        AimingObject(player);

        base.Start();

    }

    protected override IEnumerator AttackPattern()
    {
        yield return new WaitForSeconds(1.5f);
        if (gameObject.transform.position.x < 0)
        {
            StartCoroutine(ObjectMover(new Vector3(transform.position.x + 100, transform.position.y, transform.position.z), 15f));
        }
        else
        {
            StartCoroutine(ObjectMover(new Vector3(transform.position.x - 100, transform.position.y, transform.position.z), 15f));
        }

        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(1f);
            PlaySFX(4);
            SingleShot(40f, AttackPrefab[0], player, 200f, 0f, 0f);
        }
        StartCoroutine(ObjectMover(new Vector3(transform.position.x, transform.position.y, -100), 4f));
    }

}
