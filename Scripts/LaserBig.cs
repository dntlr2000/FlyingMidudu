using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBig : LaserSmall
{
    // Start is called before the first frame update
    protected override void Start()
    {
        startDelay = 1f;
        laserDuration = 4f;
        base.Start();
        hitbox.transform.localScale = new Vector3(0, 0, 0);
    }

    protected override IEnumerator LaserCoroutine(float startDelay, float shrinkTime)
    {

        hitbox.transform.localScale = new Vector3(0, 0, 0);
        hitbox.enabled = false;
        yield return new WaitForSeconds(startDelay);
        //BGM_Script.PlaySFX(5); //임시
        hitbox.enabled = true;
        hitBoxScale = new Vector3(0.01f, 0.01f, 1f);//hitbox.transform.localScale;
        targetScale = new Vector3(1f, 1f, hitBoxScale.z);

        animator.SetBool("ifOff", false);

        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime);
            hitbox.transform.localScale = Vector3.Lerp(hitBoxScale, targetScale, t);
            yield return null; //다음 프레임
        }
        hitbox.transform.localScale = targetScale;

        yield return new WaitForSeconds(shrinkTime);


        animator.SetBool("ifOff", true);
        hitBoxScale = hitbox.transform.localScale;
        targetScale = new Vector3(0.01f, 0.01f, hitBoxScale.z);

        elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime);
            hitbox.transform.localScale = Vector3.Lerp(hitBoxScale, targetScale, t);
            yield return null; //다음 프레임
        }
        hitbox.transform.localScale = targetScale;

        Destroy(parent.gameObject);


    }
}
