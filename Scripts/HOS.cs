using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HOS : MonoBehaviour
{
    private Animator animator;
    private Collider myCollider;
    void Start()
    {
        myCollider= GetComponent<Collider>();
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("ifAlive", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShutDown()
    {
        StartCoroutine(ShutDownCoroutine());
    }

    private IEnumerator ShutDownCoroutine()
    {
        animator.SetBool("ifAlive", false);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
