using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HOS : MonoBehaviour
{
    private Animator animator;
    private Collider myCollider;

    public GameObject player;
    IEnumerator coroutine;

    void Start()
    {
        myCollider= GetComponent<Collider>();
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("ifAlive", true);


        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyAttack")
        {
            Destroy(other.gameObject);
        }
    }

    public void ShutDown()
    {
        StartCoroutine(ShutDownCoroutine());
        StopCoroutine(coroutine);
    }

    private IEnumerator ShutDownCoroutine()
    {
        animator.SetBool("ifAlive", false);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    public void PullPlayer(float delay)
    {
        coroutine = PullPlayerCoroutine(delay);
        StartCoroutine(coroutine);
    }

    private IEnumerator PullPlayerCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        while (true)
        {
            if (player != null)
            {
                Vector3 direction = (transform.position - player.transform.position).normalized;
                float pullStrength = 8f; // 당기는 세기

                player.transform.position = Vector3.MoveTowards(
                        player.transform.position,
                        transform.position,
                        pullStrength * Time.deltaTime
                    );
            }
            yield return null;
        }
    }
}
