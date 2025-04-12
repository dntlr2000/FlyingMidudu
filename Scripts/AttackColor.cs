using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackColor : MonoBehaviour
{
    public GameObject Target;
    Color custom = new Color(102f / 255f, 255f / 255f, 180f / 255f);
    public int changeIndex = 0;

    void Start()
    {
        //if (Target == null) Target = gameObject;
        //SetAttackColor(125, 0, 0);
    }


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
    
}
