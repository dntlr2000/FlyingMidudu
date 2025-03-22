using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangaeG_Slowdown : BangaeG
{

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

        yield return new WaitForSeconds(1.5f);

        for (int i = 0; i < 2; i++)
        {
            PlaySFX(4);
            SingleShot(60, AttackPrefab[0], player, 255, 103, 136);
            yield return new WaitForSeconds(4f);
        }

        yield return new WaitForSeconds(1f);

        StartCoroutine(ObjectMover(new Vector3(transform.position.x, 100, transform.position.z), 4f));
    }
}