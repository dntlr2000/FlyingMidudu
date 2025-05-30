using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionC : Enemy_Minion
{
    //GameObject player;
    // Start is called before the first frame update
    public GameObject attackObject;
    public GameObject laserObject;
    protected override void Start()
    {
        //Debug.Log("Minion spawned");
        Rigidbody rb = GetComponent<Rigidbody>();
        Life = 1;
        Health = 500f;
        player = FindPlayer();
        if (player == null) return;
        AimingObject(player);


        //attack = StartCoroutine(AttackPattern());
        base.Start();

    }

    protected override IEnumerator AttackPattern()  //예시, 이후 파생 클래스에서 오버라이드해서 제대로 된 패턴 구현
    {

        yield return new WaitForSeconds(2f);
        //Debug.Log("Attack started");
        //while (true)
        for (int i = 0; i < 40; i++)
        {
            //Debug.Log("Attack!");
            if (i % 3 == 0)
            {
                ShootLasers(player, 6, laserObject, 40, 175, 103, 181);
            }
            PlaySFX(6);
            BasicAttack(50, 20f, 3, player, attackObject, 77, 191, 0);
            yield return new WaitForSeconds(2f);

        }
        StartCoroutine(ObjectMover(new Vector3(-70, transform.position.y, transform.position.z), 2f));
        //RandomMove(2, 2f);
    }

}
