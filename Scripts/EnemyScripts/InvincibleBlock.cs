using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleBlock : MonoBehaviour
{

    protected virtual void OnTriggerEnter(Collider other) //피격 시
    {

        if (other.tag == "PlayerAttackA") //기본 탄막
        {
            Destroy(other.gameObject);
        }
        if (other.tag == "UltimateA") 
        {
            Destroy(other.gameObject);

        }
        if (other.tag == "EnemyAttack") 
        {
            Destroy(other.gameObject);

        }
    }
}
