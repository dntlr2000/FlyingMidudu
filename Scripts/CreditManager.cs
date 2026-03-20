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
    bool ifEnd = true;

    private float creditSpeed = 60f;
    private float endPositionY;

    public GameObject endingCutscene;
    public GameObject toMainScreen;

    IEnumerator scroll;
    IEnumerator credit;


    protected override void Start()
    {
        creditRect = creditObject.GetComponent<RectTransform>();
        BGM_Script = BGMController.Instance;
        CursorSwitch(false);
        

        if (creditRect == null)
        {
            Debug.LogError("크레딧 할당 안됨");
            return;
        }

        scroll = Scroll();
        credit = Credits();


        float viewportHeight = ((RectTransform)transform).rect.height;
        float contentHeight = creditRect.rect.height;

        endPositionY = contentHeight + viewportHeight + creditRect.anchoredPosition.y;

        StartCoroutine(credit);
    }

    protected override void Update()
    {
        if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Space))
        {
            if (!ifEnd)
            {
                ifEnd = true;
                StopCoroutine(credit);
                StopCoroutine(scroll);

                background.SetActive(true);
                endingCutscene.SetActive(false);
                creditObject.SetActive(false);
                toMainScreen.SetActive(true);
                BGM_Script.FadeOutBGM(2);
                BGM_Script.PlaySFX(1);
            }
            
        }
    }

    private IEnumerator Scroll()
    {
        
        while (creditRect.anchoredPosition.y < endPositionY)
        {
            // 매 프레임 위로 이동
            creditRect.anchoredPosition += Vector2.up * creditSpeed * Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(3f);
        creditObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        toMainScreen.SetActive(true);
        //BGM_Script.StopBGM();
        ifEnd = true;
        BGM_Script.FadeOutBGM(2);
        BGM_Script.PlaySFX(1);
        yield return null;
    }

    private IEnumerator Credits()
    {
        BGM_Script.ChangeBGM(13);
        BGM_Script.PlayBGM();
        background.SetActive(false);
        creditObject.SetActive(false);
        endingCutscene.SetActive(true);
        yield return new WaitForSeconds(2f);
        ifEnd = false;
        yield return new WaitForSeconds(28f);
        background.SetActive(true);
        endingCutscene.SetActive(false);
        creditObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        StartCoroutine(scroll);
    }

    public void toMain()
    {
        BGM_Script.PlaySFX(0);
        StageLoader("MainMenu");
    }
}
