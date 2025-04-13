using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class GrowingPug : Enemy_Minion
{
    public float duration = 1f;           // 걸리는 시간
    public float startScale = 0.1f;       // 시작 배율
    public float endScale = 3f;

    private float moveSpeed = 40f;
    private CameraController playerCamera;
    int[] r = { 214, 214, 214, 11, 0, 18, 139 };
    int[] g = { 0, 139, 214, 214, 139, 0, 0 };
    int[] b = { 0, 0, 0, 0, 214, 214, 214 };
    protected override void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        Life = 1;
        Health = 1000f;
        player = FindPlayer();
        if (player == null) return;
        AimingObject(player);
        playerCamera = FindAnyObjectByType<CameraController>();

        base.Start();

    }


    protected override IEnumerator AttackPattern()
    {
        float elapsedTime = 0f;
        Vector3 initial = Vector3.one * startScale;
        Vector3 target = Vector3.one * endScale;

        transform.localScale = initial;

        StartCoroutine(ObjectMover(transform.position + new Vector3(0f, 5f, 0f), 1f));
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            transform.localScale = Vector3.Lerp(initial, target, t);
            yield return null;
        }

        transform.localScale = target;

        //yield return new WaitForSeconds(0.5f);

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * moveSpeed, ForceMode.Impulse);

        AimConstraint aimConstraint = GetComponent<AimConstraint>();
        if (aimConstraint != null) { aimConstraint.enabled= false; }

        yield return new WaitForSeconds(1f);
        

        while (true)
        {
            for (int i = 0; i < 7; i++)
            {
                PlaySFX(5);
                BasicAttack(40, 40, 2, player, AttackPrefab[0], r[i], g[i], b[i]);
                yield return new WaitForSeconds(0.3f);
            }
            
        }

    }
    

    /*
    private void OnDestroy()
    {
        PlaySFX(4);
        if (playerCamera != null) playerCamera.CameraShake(1);
        SlowdownAttack(100, 20, 2, player, AttackPrefab[1], 200, 5, 5, 0.5f, 0.4f);
    }
    */
}
