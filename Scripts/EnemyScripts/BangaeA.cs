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
        //attack = StartCoroutine(AttackPattern());

        base.Start();

    }

    protected override IEnumerator AttackPattern()
    {

        yield return new WaitForSeconds(1.5f);

        if (gameObject.transform.position.x < 0)
        {
            //Debug.Log("Left to Right");
            StartCoroutine(ObjectMover(new Vector3(transform.position.x + 80, transform.position.y, transform.position.z), 15f));
        }
        else
        {
            //Debug.Log("Right to Left");
            StartCoroutine(ObjectMover(new Vector3(transform.position.x - 80, transform.position.y, transform.position.z), 15f));
        }


        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(1f);
            PlaySFX(4);
            //BGM_Script.PlaySFX(3);
            SingleShot(40f, AttackPrefab[0], player);
        }
        StartCoroutine(ObjectMover(new Vector3(transform.position.x, transform.position.y, -100), 4f));
        //RandomMove(2, 2f);
    }

}
