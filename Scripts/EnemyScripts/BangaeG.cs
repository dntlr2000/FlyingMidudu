using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangaeG : Enemy_Minion
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
        //attack = StartCoroutine(AttackPattern());

        base.Start();

    }

    protected override IEnumerator AttackPattern()
    {
        yield return new WaitForSeconds(1.5f);

        if (gameObject.transform.position.x < 0)
        {
            //Debug.Log("Right to Right");
            if (gameObject.transform.position.y < 0)
            {
                StartCoroutine(ObjectMover(new Vector3(transform.position.x - 20, transform.position.y + 10, transform.position.z + 60), 10f));
            }
            else
            {
                StartCoroutine(ObjectMover(new Vector3(transform.position.x - 20, transform.position.y - 10, transform.position.z + 60), 10f));
            }

        }
        else
        {
            //Debug.Log("Left to Left");
            if (gameObject.transform.position.y < 0)
            {
                StartCoroutine(ObjectMover(new Vector3(transform.position.x + 20, transform.position.y + 10, transform.position.z + 60), 10f));
            }
            else
            {
                StartCoroutine(ObjectMover(new Vector3(transform.position.x + 20, transform.position.y - 10, transform.position.z + 60), 10f));
            }
        }

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.4f);
            PlaySFX(4);
            SingleShot(80, AttackPrefab[0], player);
        }

        yield return new WaitForSeconds(1f);

        StartCoroutine(ObjectMover(new Vector3(transform.position.x, 100, transform.position.z), 4f));
    }
}