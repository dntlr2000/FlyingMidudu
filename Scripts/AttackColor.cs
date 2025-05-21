using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackColor : MonoBehaviour
{
    public GameObject Target;
    Color custom = new Color(102f / 255f, 255f / 255f, 180f / 255f);
    public int changeIndex = 0;

    private Vector3 originScale;
    private Vector3 StartScale;

    void Start()
    {
        originScale = new Vector3(gameObject.transform.localScale.x, transform.localScale.y, transform.localScale.z);
        StartScale = new Vector3(0.1f, 0.1f, 0.1f);
        transform.localScale = StartScale;
        //if (Target == null) Target = gameObject;
        //SetAttackColor(125, 0, 0);
        StartCoroutine(ScaleToOrigin());
    }

    /*
    public AttackColor(float R, float B, float G, float multiply = 1f)
    {
        custom = custom = new Color(R / 255f, G / 255f, B / 255f) * multiply;
        //SetAttackColor(R, B, G, multiply);
    }
    */
    
    //추후 생성자를 사용하는 구조로 갈아치울수도?
    public void SetAttackColor(float R, float G, float B, float multiply = 1f)
    {
        custom = new Color(R / 255f, G /255f, B / 255f) * multiply;
        if (Target == null) Target = gameObject;

        Renderer renderer = Target.GetComponent<Renderer>();
        if (renderer == null) return;

        // 메터리얼 복사 (공유 방지)
        //Material newMat = new Material(renderer.sharedMaterial);
        Material[] mats = renderer.materials;

        mats[changeIndex] = new Material(mats[changeIndex]);

        // Emission 활성화
        mats[changeIndex].EnableKeyword("_EMISSION");

        // Emission 색 설정
        mats[changeIndex].SetColor("_EmissionColor", custom);

        // 새 메터리얼 적용
        renderer.material = mats[changeIndex];

    }

    private IEnumerator ScaleToOrigin(float duration = 0.3f)
    {
        float elapsedTime = 0f;
        transform.localScale = StartScale;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            transform.localScale = Vector3.Lerp(StartScale, originScale, t);
            yield return null;
        }

        transform.localScale = originScale; // 정확하게 맞추기
    }
}
