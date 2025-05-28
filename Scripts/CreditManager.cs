using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreditManager : MenuParent
{
    public GameObject creditObject;
    private RectTransform creditRect;
    public GameObject background;
   

    //public TextMeshPro[] text;


    private float creditSpeed = 80f;
    private float endPositionY;

    public GameObject endingCutscene;
    public GameObject toMainScreen;


    protected override void Start()
    {
        creditRect = creditObject.GetComponent<RectTransform>();
        BGM_Script = FindAnyObjectByType<BGMController>();

        if (creditRect == null)
        {
            Debug.LogError("크레딧 할당 안됨");
            return;
        }


        float viewportHeight = ((RectTransform)transform).rect.height;
        float contentHeight = creditRect.rect.height;

        endPositionY = contentHeight + viewportHeight + creditRect.anchoredPosition.y;

        StartCoroutine(Credits());
    }


    private IEnumerator Scroll()
    {
        
        while (creditRect.anchoredPosition.y < endPositionY)
        {
            // 매 프레임 위로 이동
            creditRect.anchoredPosition += Vector2.up * creditSpeed * Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        toMainScreen.SetActive(true);

        yield return null;
    }

    private IEnumerator Credits()
    {
        background.SetActive(false);
        creditObject.SetActive(false);
        endingCutscene.SetActive(true);
        yield return new WaitForSeconds(30f);
        background.SetActive(true);
        endingCutscene.SetActive(false);
        creditObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        StartCoroutine(Scroll());
    }

    public void toMain()
    {
        StageLoader("MainMenu");
    }
}
