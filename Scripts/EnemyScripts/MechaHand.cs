using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaHand : Enemy_Minion
{
    // Start is called before the first frame update
    public GameObject ParentObject;
    //private Vector3 rgb;
    private Collider myCollider;

    
    private struct RGB
    {
        public int r;
        public int g;
        public int b;
    }

    RGB rgb;

    protected override void Start()
    {
        //Rigidbody rb = GetComponent<Rigidbody>();
        animator = ParentObject.GetComponent<Animator>();

        Life = 9999;
        Health = 30f;

        player = FindPlayer();
        if (player == null) return;

        BGM_Script = FindObjectOfType<BGMController>();

        myCollider = GetComponent<Collider>();



        if (gameObject.transform.position.x > 0)
        {
            animator.SetBool("ifRightHand", true);

            rgb = new RGB { r =  255, g= 102, b =  51 };
        }
        else
        {
            animator.SetBool("ifRightHand", false);
            //rgb = new Vector3(51, 102, 255);
            rgb = new RGB { r = 51, b = 102, g = 255 };
        }

        //Vector3 StartLocation = new Vector3(transform.position.x, transform.position.y, -50f);
        //StartCoroutine(ObjectMover(StartLocation, 1f));
    }

    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */

    public void MotionChange(int state)
    {
        animator.SetInteger("Motion", state);
    }

    private IEnumerator PunchCoroutine(int num, float ChargeSpeed, float delay, int chaseTime = 1)
    {
        MotionChange(1);
        Transform PlayerTransform = player.transform;
        Vector3 startLocation;
        Vector3 handleLocation;
        Vector3 punchLocation;
        for (int i  = 0; i < num; i++)
        {
            myCollider.enabled = false;
            for (int k = 0; k < 1 * chaseTime; k++) {
                Vector3 playerPos = PlayerTransform.position;
                startLocation = new Vector3(playerPos.x, playerPos.y, -50f);
                StartCoroutine(ObjectMover(startLocation, 0.1f));
                yield return new WaitForSeconds(0.1f);
            }
            myCollider.enabled = true;
            handleLocation = new Vector3(transform.position.x, transform.position.y, -60f);
            StartCoroutine(ObjectMover(handleLocation, ChargeSpeed));
            yield return new WaitForSeconds(ChargeSpeed);

            punchLocation = new Vector3(transform.position.x, transform.position.y, 70f);
            StartCoroutine(ObjectMover(punchLocation, 0.2f));
            yield return new WaitForSeconds(0.2f);

            BasicAttack(punchLocation, 60, 30f, 2, handleLocation, AttackPrefab[0], rgb.r, rgb.g, rgb.b);
            PlaySFX(3);

            yield return new WaitForSeconds(delay);
        }
        StartCoroutine(ObjectMover(new Vector3(ParentObject.transform.position.x, ParentObject.transform.position.y, -60f), 0.5f));
        yield return new WaitForSeconds(0.5f);
        MotionChange(0);

    }

    public void Punch(int num, float ChargeSpeed, float delay, int chaseTime = 1)
    {
        StartCoroutine(PunchCoroutine(num, ChargeSpeed, delay, chaseTime));
    }

    
    protected override void Death() //잡몹은 생성한 공격 오브젝트를 삭제하지 않고 그냥 삭제
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        PlaySFX(1);
        Destroy(ParentObject.gameObject);

    }

    
    public void DisableHand()
    {
        Destroy(ParentObject);
        //ParentObject.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Destroy(ParentObject.gameObject);
    }


}
