using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangaeE : Enemy_Minion
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

        
        //int index = 0;

        for (int k = 0; k < 2; k++)
        {
            yield return new WaitForSeconds(1.5f);
            RandomMove(2, 3f);
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(0.2f);
                PlaySFX(5);

                SlowdownAttack(50, 80, 4, player, AttackPrefab[0], 0.2f);
            }
            
        }
        yield return new WaitForSeconds(1.5f);


        StartCoroutine(ObjectMover(new Vector3(transform.position.x, transform.position.y, -100), 4f));
    }
    
}