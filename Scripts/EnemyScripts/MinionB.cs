using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionB : MinionA
{
    // Start is called before the first frame update
    protected override IEnumerator AttackPattern()  //예시, 이후 파생 클래스에서 오버라이드해서 제대로 된 패턴 구현
    {

        yield return new WaitForSeconds(1f);
        //Debug.Log("Attack started");
        //while (true)
        for (int i = 0; i < 5; i++)
        {
            //Debug.Log("Attack!");
            ShootProjectile();
            yield return new WaitForSeconds(6f);

        }
        //Debug.Log("Attack ended");
        ObjectMover(new Vector3(-70, 0, 0), 2);
    }

    protected override void ShootProjectile()
    {
        GameObject projectile = Instantiate(attackObject, transform.position + transform.up * 1f, transform.rotation * Quaternion.Euler(0, 180f, 0));
        
        
    }
}
