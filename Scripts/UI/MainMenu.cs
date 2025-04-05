using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MenuParent
{
    public GameObject CreditScreen;

    public TextMeshProUGUI[] MainButtons;
    private RectTransform[] MainButtonsRectTransform;

    public TextMeshProUGUI[] GameButtons;
    private RectTransform[] GameButtonsRectTransform;

    private Vector2[] MainButtonOriginPos;
    private Vector2[] GameButtonOriginPos;

    public SaveManager saveManager;

    protected override void Start()
    {
        //SetBGM(0);
        //optionsPanel.SetActive(false);
        //base.Start();
        BGM_Script = FindObjectOfType<BGMController>();
        SetBGM(0);


        MainButtonsRectTransform = new RectTransform[MainButtons.Length];
        MainButtonOriginPos = new Vector2[MainButtons.Length];
        for (int i = 0; i < MainButtons.Length; i++)
        {
            MainButtonsRectTransform[i] = MainButtons[i].GetComponent<RectTransform>();
            MainButtonOriginPos[i] = new Vector2(MainButtonsRectTransform[i].anchoredPosition.x, MainButtonsRectTransform[i].anchoredPosition.y);
        }

        GameButtonsRectTransform = new RectTransform[GameButtons.Length];
        GameButtonOriginPos = new Vector2[GameButtons.Length];
        for (int i = 0; i < GameButtons.Length; i++)
        {
            GameButtonsRectTransform[i] = GameButtons[i].GetComponent<RectTransform>();
            GameButtonOriginPos[i] = new Vector2(GameButtonsRectTransform[i].anchoredPosition.x, GameButtonsRectTransform[i].anchoredPosition.y);
        }

        saveManager = GetComponent<SaveManager>();

        ApplyProgress();
    }

    // Update is called once per frame

    public void StartGame(string StageName = "Stage1")
    {        //BGM_Script.PauseBGM();
        BGM_Script.PlaySFX(0);
        StageLoader(StageName);
    }


    public void OpenCredits()
    {
        BGM_Script.PlaySFX(0);
        CreditScreen.SetActive(true);
    }

    public void CloseCredits()
    {
        //BGM_Script.PlaySFX(0);
        CreditScreen.SetActive(false);
    }

    protected void SetBGM(int index)
    {

        if (BGM_Script == null)
        {
            Debug.Log("BGMScript NOT FOUND");
            return;
        }
        BGM_Script.BgmIndex = index;
        BGM_Script.ChangeBGM(BGM_Script.BgmIndex);
        BGM_Script.PlayBGM();
    }

    /*
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
    */

    private IEnumerator TextMover(RectTransform Buttons, Vector2 positionA, Vector3 positionB)
    {
        float elapsedTime = 0f;
        float moveTime = 0.5f;

        while (elapsedTime < moveTime)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / moveTime);

            Buttons.anchoredPosition = Vector2.Lerp(positionA, positionB, t);

            yield return null;
        }

        Buttons.anchoredPosition = positionB;
    }

    private IEnumerator MainUIDisAppear()
    {
        for (int i = 0; i < 3; i++)
        {
            StartCoroutine(TextMover(MainButtonsRectTransform[i], MainButtonOriginPos[i], new Vector2(MainButtonOriginPos[i].x - 400, MainButtonOriginPos[i].y))); //120¹øÂ° ÁÙ
            yield return new WaitForSecondsRealtime(0.1f);
        }
        for (int i = 3; i < 6; i++)
        {
            if (i == 5) yield return new WaitForSecondsRealtime(0.2f);
            StartCoroutine(TextMover(MainButtonsRectTransform[i], MainButtonOriginPos[i], new Vector2(MainButtonOriginPos[i].x + 500, MainButtonOriginPos[i].y)));
            yield return new WaitForSecondsRealtime(0.1f);
            
        }

        yield return new WaitForSecondsRealtime(0.5f);
        for (int i = 0; i < 4; i++)
        {
            StartCoroutine(TextMover(GameButtonsRectTransform[i], GameButtonOriginPos[i], new Vector2(GameButtonOriginPos[i].x + 350, GameButtonOriginPos[i].y)));
            yield return new WaitForSecondsRealtime(0.1f);
        }

        for (int i = 4; i < 6; i++)
        {
            StartCoroutine(TextMover(GameButtonsRectTransform[i], GameButtonOriginPos[i], new Vector2(GameButtonOriginPos[i].x - 400, GameButtonOriginPos[i].y)));
            yield return new WaitForSecondsRealtime(0.1f);
        }

        yield return null;
    }

    private IEnumerator MainUIAppear()
    {

        for (int i = 0; i < 4; i++)
        {
            StartCoroutine(TextMover(GameButtonsRectTransform[i], new Vector2(GameButtonOriginPos[i].x + 350, GameButtonOriginPos[i].y), GameButtonOriginPos[i]));
            yield return new WaitForSecondsRealtime(0.1f);
        }

        for (int i = 4; i < 6; i++)
        {
            StartCoroutine(TextMover(GameButtonsRectTransform[i], new Vector2(GameButtonOriginPos[i].x - 400, GameButtonOriginPos[i].y), GameButtonOriginPos[i]));
            yield return new WaitForSecondsRealtime(0.1f);
        }

        yield return new WaitForSecondsRealtime(0.5f);
        for (int i = 0; i < 3; i++)
        {
            StartCoroutine(TextMover(MainButtonsRectTransform[i], new Vector2(MainButtonOriginPos[i].x - 400, MainButtonOriginPos[i].y), MainButtonOriginPos[i]));
            yield return new WaitForSecondsRealtime(0.1f);
        }
        for (int i = 3; i < 6; i++)
        {
            StartCoroutine(TextMover(MainButtonsRectTransform[i], new Vector2(MainButtonOriginPos[i].x + 500, MainButtonOriginPos[i].y), MainButtonOriginPos[i]));
            yield return new WaitForSecondsRealtime(0.1f);
        }

        yield return null;
    }

    public void MainUIMode()
    {
        StartCoroutine(MainUIAppear());
        BGM_Script.PlaySFX(0);
    }

    public void GameSelectMode()
    {
        StartCoroutine(MainUIDisAppear());
        BGM_Script.PlaySFX(0);
    }

    public void ApplyProgress()
    {
        int progress = saveManager.LoadProgress();
        
        if (progress < 1)
        {
            disableButton(GameButtons[1]);
        }

        if (progress < 2)
        {
            disableButton(GameButtons[2]);
        }

        if (progress < 3)
        {
            disableButton(GameButtons[3]);
        }

        if (progress < 6)
        {
            disableButton(MainButtons[1]);
        }


    }

    public void disableButton(TextMeshProUGUI text)
    {
        Button buttonComp = text.gameObject.GetComponent<Button>();
        if (buttonComp == null)
        {
            Debug.Log("Button Component doesn't Exist on text");
            return;
        }
        buttonComp.enabled = false;

        Color color = text.color;
        color.a = 0.3f;
        text.color = color;
    }

}
