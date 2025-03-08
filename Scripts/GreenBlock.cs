using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBlock : MonoBehaviour
{
    BGMController BGM_Script;
    TutorialStage stage;
    private void Start()
    {
        stage = FindObjectOfType<TutorialStage>();
        BGM_Script = FindObjectOfType<BGMController>();
    }
    protected virtual void OnTriggerEnter(Collider other) //피격 시
    {

        if (other.tag == "Player") //기본 탄막
        {
            stage.goNext = true;
            BGM_Script.PlaySFX(0);
            Destroy(gameObject);
        }
        
    }
}
