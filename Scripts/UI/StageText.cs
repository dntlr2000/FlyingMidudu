using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class StageText : MonoBehaviour
{
    public TextMeshProUGUI text;
    private RectTransform textRectTransform;

    void Start() //Start()보다 Stage 스크립트에서 호출되는 StageStartAnimation 스크립트가 먼저 실행되는듯
    {
        textRectTransform = text.GetComponent<RectTransform>();
        if (textRectTransform == null) Debug.Log("textRectTransform is Null!");
        //textRectTransform.anchoredPosition = new Vector2(0, -10);
        //gameObject.SetActive(false);
        SetText("");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetText(string stageText)
    {
        Debug.Log($"SetText called : {stageText}");        
        text.text = stageText;

    }

    private IEnumerator FadeIn(float durationTime)
    {
        float elapsedTime = 0f;

        Color textColor = text.color;
        textColor.a = 0f; //alpha
        text.color = textColor;

        while (elapsedTime < durationTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / durationTime);

            textColor.a = alpha;
            text.color = textColor;

            yield return null;

        }

        textColor.a = 1f;
        text.color = textColor;

        //StartCoroutine(FadeOut(4f));
    }

    private IEnumerator FadeOut(float durationTime)
    {

        float elapsedTime = 0f;

        Color textColor = text.color;
        textColor.a = 1f;
        text.color = textColor;

        while (elapsedTime < durationTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = 1f - Mathf.Clamp01(elapsedTime / durationTime);

            textColor.a = alpha;
            text.color = textColor;

            yield return null;
        }

        textColor.a = 0f;
        text.color = textColor;

    }

    private IEnumerator TextMover3D(Vector3 positionA, Vector3 positionB)
    {
        float elapsedTime = 0f;
        float moveTime = 1f;

        while (elapsedTime < moveTime)
        {
            elapsedTime += Time.deltaTime;

            float t = Mathf.Clamp01(elapsedTime / moveTime);

            text.transform.position = Vector3.Lerp(positionA, positionB, t);
            yield return null;
        }
        text.transform.position = positionB;
    }

    private IEnumerator TextMover(Vector2 positionA, Vector2 positionB)
    {
        float elapsedTime = 0f;
        float moveTime = 1f;

        while (elapsedTime < moveTime)
        {
            elapsedTime += Time.deltaTime;

            float t = Mathf.Clamp01(elapsedTime / moveTime);

            textRectTransform.anchoredPosition = Vector2.Lerp(positionA, positionB, t);
            yield return null;
        }
        textRectTransform.anchoredPosition = positionB;
    }

    public IEnumerator StageTextAnimation(string stageText)
    {
        yield return new WaitForSeconds(0.5f); // 이거 안하면 이 코드의 Start가 늦게 실행되어서 에러 발생함
        Debug.Log($"StageTextAnimation Called : {stageText}");  
        SetText(stageText);
        StartCoroutine(FadeIn(1f));
        StartCoroutine(TextMover(new Vector2(0, -10), new Vector2(0, 0)));
        yield return new WaitForSeconds(5f);
        StartCoroutine(FadeOut(1f));
        StartCoroutine(TextMover(new Vector2(0, 0), new Vector2(0, 10)));
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
     
    public IEnumerator SpellCardAnimation(string spellName)
    {
        //textRectTransform.anchoredPosition = new Vector2(-600, -250);
        StartCoroutine(FadeIn(0.3f));
        StartCoroutine(TextMover(new Vector2(-600, -250), new Vector2(-600, -10)));
        SetText(spellName);
        yield return new WaitForSeconds(0.5f);
        //gameObject.SetActive(false);

    }

    public IEnumerator StageClearAnimation(string stageText)
    {
        yield return new WaitForSeconds(0.5f); // 이거 안하면 이 코드의 Start가 늦게 실행되어서 에러 발생함
        Debug.Log($"StageTextAnimation Called : {stageText}");
        SetText(stageText);
        StartCoroutine(FadeIn(0.5f));
       // StartCoroutine(TextMover(new Vector2(0, -10), new Vector2(0, 0)));
        yield return new WaitForSeconds(10f);
        //StartCoroutine(FadeOut(1f));
        //StartCoroutine(TextMover(new Vector2(0, 0), new Vector2(0, 10)));
        //yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}

   
