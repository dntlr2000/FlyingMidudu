using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSmall : MonoBehaviour
{
    //Quaternion.Euler(0, 180f, 0)로 보정해야 정면으로 나간다

    private Collider hitbox;
    public GameObject parent;
    public Animator animator;
    //public float shrinkTIme;
    // Start is called before the first frame update
    //private BGMController BGM_Script;

    void Start()
    {
        hitbox= GetComponent<Collider>();
        //animator = parent.GetComponent<Animator>();
        AbleLaser(1f, 2f);
        //BGM_Script = FindObjectOfType<BGMController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AbleLaser(float startDelay = 1f, float shrinkTime = 3f)
    {
        StartCoroutine(LaserCoroutine(startDelay, shrinkTime));
    }

    IEnumerator LaserCoroutine(float startDelay, float shrinkTime)
    {
        yield return new WaitForSeconds(startDelay);
        //BGM_Script.PlaySFX(5); //임시
        hitbox.enabled = true;
        Vector3 hitBoxScale = hitbox.transform.localScale;
        Vector3 targetScale = new Vector3(2.27f, 2.27f, hitBoxScale.z);

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
