using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class GrowingPug : Enemy_Minion
{
    public float duration = 2f;           // 걸리는 시간
    public float startScale = 0.1f;       // 시작 배율
    public float endScale = 5f;

    public float moveSpeed = 20f;

    protected override void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        Life = 1;
        Health = 1000f;
        GameObject player = FindPlayer();
        if (player == null) return;
        AimingObject(player);

        base.Start();
        
    }


    protected override IEnumerator AttackPattern()
    {
        float elapsedTime = 0f;
        Vector3 initial = Vector3.one * startScale;
        Vector3 target = Vector3.one * endScale;

        transform.localScale = initial;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            transform.localScale = Vector3.Lerp(initial, target, t);
            yield return null;
        }

        transform.localScale = target;

        yield return new WaitForSeconds(1f);

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * moveSpeed, ForceMode.Impulse);

        AimConstraint aimConstraint = GetComponent<AimConstraint>();
        if (aimConstraint != null) { aimConstraint.enabled= false; }


        yield return null;
    }

}
