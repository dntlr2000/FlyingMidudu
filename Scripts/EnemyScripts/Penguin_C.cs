using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penguin_C : Enemy_Minion { 
    // Start is called before the first frame update
    protected override void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        Life = 1;
        Health = 100f;

        player = FindPlayer();
        if (player == null) return;
        //AimingObject(player);
        //attack = StartCoroutine(AttackPattern());

        base.Start();
    }

    // Update is called once per frame
    protected override IEnumerator AttackPattern()
    {
        
        if (gameObject.transform.position.x < 0)
        {
            StartCoroutine(RotateObject(0.5f, 0, 90, 0));
            StartCoroutine(ObjectMover(new Vector3(transform.position.x + 60, transform.position.y, transform.position.z), 5f));
            yield return new WaitForSeconds(5f);
            StartCoroutine(RotateObject(1f, 0, -90, 0));
        }
        else
        {
            //Debug.Log("Right to Left");
            StartCoroutine(RotateObject(0.5f, 0, -90, 0));
            StartCoroutine(ObjectMover(new Vector3(transform.position.x - 60, transform.position.y, transform.position.z), 5f));
            yield return new WaitForSeconds(5f);
            StartCoroutine(RotateObject(1f, 0, 90, 0));
        }
        yield return new WaitForSeconds(3f);

        ShootLasers(player, 1, AttackPrefab[1], 3);
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.4f); ;
            PlaySFX(5);
            BasicAttack(60, 40, 3, player, AttackPrefab[0], 108, 230, 227);
        }
        yield return new WaitForSeconds(2f);
        StartCoroutine(ObjectMover(new Vector3(transform.position.x, transform.position.y, -150), 4f));
    }

    
}

