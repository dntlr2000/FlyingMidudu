using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class MinionA : Enemy_Minion
{
    public GameObject attackObject;
    public float shootSpeed = 20f;
    private float shootInterval = 1f;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        //Debug.Log("Minion spawned");
        Rigidbody rb = GetComponent<Rigidbody>();

        Life = 1;
        Health = 60f;
        GameObject player = FindPlayer();
        if (player == null) return;
        AimingObject(player);
        

        base.Start();

        
    }

    protected override IEnumerator AttackPattern()  
    {
        
        
        yield return new WaitForSeconds(1f);
        //Debug.Log("Attack started");
        //while (true)
        for (int i = 0; i < 20; i++)
        {
            //Debug.Log("Attack!");
            PlaySFX(4);
            ShootProjectile();
            yield return new WaitForSeconds(shootInterval);

        }
        StartCoroutine(ObjectMover(new Vector3(transform.position.x, transform.position.y, -100), 2f));
        //RandomMove(2, 2f);
    }

    protected virtual void ShootProjectile()
    {
        GameObject projectile = Instantiate(attackObject, transform.position + transform.up * 1f, transform.rotation);
        Rigidbody prb = projectile.GetComponent<Rigidbody>();
        prb.AddForce(transform.forward * shootSpeed, ForceMode.Impulse);
        StartCoroutine(ObjectRemoveTImer(projectile, 6));
    }

}
