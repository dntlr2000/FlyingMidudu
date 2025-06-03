using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoopCusion : MonoBehaviour
{

    protected virtual void OnTriggerEnter(Collider other) //피격 시
    {
        if (other.tag == "EnemyAttack") //기본 탄막
        {
            if (other.transform.parent != null) //레이저의 경우
            {
                Destroy(other.transform.parent.gameObject);
            }
            else //일반 탄막
            {
                Destroy(other.gameObject);
            }
        }
        return;

    }
}
