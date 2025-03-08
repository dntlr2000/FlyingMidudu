using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using static System.Net.Mime.MediaTypeNames;

public class BossText : MonoBehaviour
{
    public TextMeshProUGUI Description;
    public TextMeshProUGUI BossName;
    public Image Background;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator FadeIn(TextMeshProUGUI text, float durationTime)
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

    private IEnumerator FadeOut(TextMeshProUGUI text, float durationTime)
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

    private IEnumerator FadeIn(Image image, float durationTime)
    {
        float elapsedTime = 0f;
        Color imageColor = image.color;
        imageColor.a = 0f; 
        image.color = imageColor;

        while (elapsedTime < durationTime)
        {
            elapsedTime += Time.deltaTime;
            imageColor.a = Mathf.Clamp01(elapsedTime / durationTime);
            image.color = imageColor;

            yield return null; 
        }

        imageColor.a = 1f;
        image.color = imageColor;
    }

    private IEnumerator FadeOut(Image image, float durationTime)
    {
        float elapsedTime = 0f;
        Color imageColor = image.color;
        imageColor.a = 1f; 
        image.color = imageColor;

        while (elapsedTime < durationTime)
        {
            elapsedTime += Time.deltaTime;
            imageColor.a = 1f - Mathf.Clamp01(elapsedTime / durationTime);
            image.color = imageColor;

            yield return null;
        }

        imageColor.a = 0f;
        image.color = imageColor;
    }

    public IEnumerator BossInformation(string description, string bossName)
    {

        StartCoroutine(FadeIn(Background, 0.5f));
        StartCoroutine(FadeIn(Description, 0.5f));
        StartCoroutine(FadeIn(BossName, 0.5f));
        yield return null;
        SetText(Description, description);
        SetText(BossName, bossName);
        yield return new WaitForSeconds(3f);
        StartCoroutine(FadeOut(Description, 0.5f));
        StartCoroutine(FadeOut(BossName, 0.5f));
        StartCoroutine(FadeOut(Background, 0.5f));
        yield return null;
    }

    public void SetText(TextMeshProUGUI baseText, string text)
    {
        Debug.Log($"SetText called : {text}");
        baseText.text = text;

    }
}
