using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangaeD : Enemy_Minion
{
    // Start is called before the first frame update
    protected override void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        Life = 1;
        Health = 50f;

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
            if (gameObject.transform.position.y < 0)
            {
                StartCoroutine(ObjectMover(new Vector3(transform.position.x + 20, transform.position.y + 20, transform.position.z + 30), 6f));
            }
            else
            {
                StartCoroutine(ObjectMover(new Vector3(transform.position.x + 20, transform.position.y - 20, transform.position.z + 30), 6f));
            }
            
        }
        else
        {
            //Debug.Log("Right to Left");
            if (gameObject.transform.position.y < 0)
            {
                StartCoroutine(ObjectMover(new Vector3(transform.position.x - 20, transform.position.y + 20, transform.position.z + 30), 6f));
            }
            else
            {
                StartCoroutine(ObjectMover(new Vector3(transform.position.x - 20, transform.position.y - 20, transform.position.z + 30), 6f));
            }
        }


        for (int i = 0; i < 3; i++)
        {
            PlaySFX(4);
            ShootAround(player, 30, AttackPrefab[0], 20, 40, 0.2f, 255, 201, 0);
            yield return new WaitForSeconds(2f);
        }
        StartCoroutine(ObjectMover(new Vector3(transform.position.x, transform.position.y, -100), 8f));

    }
    
}
