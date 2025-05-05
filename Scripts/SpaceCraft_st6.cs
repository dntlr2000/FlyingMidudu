using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceCraft_st6 : MonoBehaviour
{
    private Animator animator;
    private bool ifOpen = false;
    //public GameObject Boss;

    void Start()
    {
        animator= GetComponent<Animator>();
        //MoveSpaceCraft();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected IEnumerator ObjectMover(Vector3 to, float time)   
    {
        //yield return new WaitForSeconds(delay);

        float elapsedTime = 0f;
        Vector3 from = gameObject.transform.position;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;

            // 이동 비율 계산
            float t = Mathf.Clamp01(elapsedTime / time); //균일한 속도로 이동
            t = 1f - Mathf.Pow(1f - t, 2f); // Ease-Out 적용

            // 위치 보간
            gameObject.transform.position = Vector3.Lerp(from, to, t);

            // 다음 프레임까지 대기
            yield return null;

        }
    }
    /*
    protected IEnumerator ObjectMover(GameObject Boss, Vector3 to, float time, float delay)   //from = 원래 자리, 자기 자신을 이동하므로 Stage에는 존재하진 않음
    {
        yield return new WaitForSeconds(delay);

        float elapsedTime = 0f;
        Vector3 from = Boss.transform.position;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;

            // 이동 비율 계산
            float t = Mathf.Clamp01(elapsedTime / time); //균일한 속도로 이동
            t = 1f - Mathf.Pow(1f - t, 2f); // Ease-Out 적용

            // 위치 보간
            gameObject.transform.position = Vector3.Lerp(from, to, t);

            // 다음 프레임까지 대기
            yield return null;

        }
    }
    */

    public void DoorSwitch()
    {
        ifOpen = !ifOpen;
        animator.SetBool("ifOpen", ifOpen);

    }

    public void MoveSpaceCraft()
    {
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, 250);
        //DoorSwitch();
        StartCoroutine(ObjectMover(newPos, 6f));
        //Vector3 newPosBoss = new Vector3(Boss.transform.position.x, Boss.transform.position.y, 584 - 250 - 60);
        //StartCoroutine(ObjectMover(Boss, newPosBoss, 6f, 3f));
    }


}
