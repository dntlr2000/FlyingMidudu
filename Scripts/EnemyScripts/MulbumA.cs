using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MulbumA : Enemy_Minion
{
    // Start is called before the first frame update
    protected override void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        animator= GetComponent<Animator>();

        Life = 1;
        Health = 100f;

        player = FindPlayer();

        StartCoroutine(Motion(1f));
        attack = StartCoroutine(AttackPattern());
        
    }

    // Update is called once per frame
    protected override IEnumerator AttackPattern() 
    {

        yield return new WaitForSeconds(2f);
        Debug.Log("Attack started");
        for (int i = 0; i < 4; i++)
        {
            //Debug.Log("Attack!");
            yield return new WaitForSeconds(2f);
            //SingleShot(40f, AttackPrefab[0], player);
            BasicAttack(6, 30, 6, player, AttackPrefab[0], 0, 92, 255);
            PlaySFX(5);
        }

        yield return new WaitForSeconds(1f);
        StartCoroutine(Motion(2f));
        yield return new WaitForSeconds(0.25f);
        StartCoroutine(ObjectMover(new Vector3(transform.position.x, transform.position.y, transform.position.z + 150f), 3f));

        for (int i = 0; i < 10; i++)
        {
            //Debug.Log("Attack!");
            yield return new WaitForSeconds(0.5f);
            SingleShot(40f, AttackPrefab[0], player, 255, 0, 0);
            PlaySFX(4);
        }

    }

    protected virtual IEnumerator Motion(float duration)
    {
        animator.SetBool("ifDash", true);
        yield return new WaitForSeconds(duration);
        animator.SetBool("ifDash", false);

        yield return null;
    }
}



