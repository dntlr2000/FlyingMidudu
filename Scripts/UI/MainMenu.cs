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

    private PlayerSetting PlayerSettingScript;

    //public TextMeshProUGUI TitleText;
    public RectTransform Title;

    protected override void Start()
    {
        //SetBGM(0);
        //optionsPanel.SetActive(false);
        //base.Start();
        BGM_Script = FindObjectOfType<BGMController>();
        PlayerSettingScript = FindObjectOfType<PlayerSetting>();
        


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
        ApplyOptions();

        //BGM_Script.PlayBGM();
        SetBGM(0);
        CursorSwitch(false);

        //QualitySettings.vSyncCount = 0;           // VSync 끄기
        //Application.targetFrameRate = 120;         // 프레임 제한

        StartCoroutine(TitleMover());
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
            StartCoroutine(TextMover(MainButtonsRectTransform[i], MainButtonOriginPos[i], new Vector2(MainButtonOriginPos[i].x - 400, MainButtonOriginPos[i].y))); //120번째 줄
            yield return new WaitForSecondsRealtime(0.1f);
        }

        for (int i = 3; i < 6; i++)
        {
            if (i == 4) yield return new WaitForSecondsRealtime(0.2f);
            StartCoroutine(TextMover(MainButtonsRectTransform[i], MainButtonOriginPos[i], new Vector2(MainButtonOriginPos[i].x + 500, MainButtonOriginPos[i].y)));
            yield return new WaitForSecondsRealtime(0.1f);
            
        }

        yield return new WaitForSecondsRealtime(0.3f);
        for (int i = 0; i < 5; i++)
        {
            StartCoroutine(TextMover(GameButtonsRectTransform[i], GameButtonOriginPos[i], new Vector2(GameButtonOriginPos[i].x + 350, GameButtonOriginPos[i].y)));
            yield return new WaitForSecondsRealtime(0.1f);
        }

        for (int i = 5; i < 7; i++)
        {
            StartCoroutine(TextMover(GameButtonsRectTransform[i], GameButtonOriginPos[i], new Vector2(GameButtonOriginPos[i].x - 400, GameButtonOriginPos[i].y)));
            yield return new WaitForSecondsRealtime(0.1f);
        }

        yield return null;
    }

    private IEnumerator MainUIAppear()
    {

        for (int i = 0; i < 5; i++)
        {
            StartCoroutine(TextMover(GameButtonsRectTransform[i], new Vector2(GameButtonOriginPos[i].x + 350, GameButtonOriginPos[i].y), GameButtonOriginPos[i]));
            yield return new WaitForSecondsRealtime(0.1f);
        }

        for (int i = 5; i < 7; i++)
        {
            StartCoroutine(TextMover(GameButtonsRectTransform[i], new Vector2(GameButtonOriginPos[i].x - 400, GameButtonOriginPos[i].y), GameButtonOriginPos[i]));
            yield return new WaitForSecondsRealtime(0.1f);
        }

        yield return new WaitForSecondsRealtime(0.3f);
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

        if (progress < 4)
        {
            disableButton(GameButtons[4]);
        }

        if (progress < 5)
        {
            disableButton(MainButtons[5]);
        }



        /*
        if (progress < 6) // 엑스트라 모드가 설정으로 변경됨
        {
            disableButton(MainButtons[1]);
        }
        */

    }

    public void ApplyOptions()
    {
        SaveManager.OptionData data = saveManager.LoadOptions();
        //yield return new WaitForSeconds(0.2f);
        if (data == null)
        {
            Debug.LogWarning("No Options Data Loaded");
            return;
        }
        
        BGM_Script.SettedVolume = data.BGM_Volume;
        BGM_Script.SetBGMVolume(data.BGM_Volume);

        BGM_Script.SetSFXVolume(data.SFX_Volume);

        PlayerSettingScript.MouseSpeed = data.MouseSpeed;

        return;

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

    private IEnumerator TitleMover()
    {
        
        Vector2 originPos = Title.anchoredPosition;
        Vector2 randomOffset;
        Vector2 targetPos;
        Vector2 StartPos;
        while (true)
        {
            randomOffset = Random.insideUnitCircle * 50;
            targetPos = originPos+ randomOffset;

            StartPos = Title.anchoredPosition;
            float elapsed = 0f; 
            float duration = 0.1f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;
                Title.anchoredPosition = Vector2.Lerp(Title.anchoredPosition, targetPos, Time.deltaTime * 2f);
                yield return null;
            }
            Title.anchoredPosition = Vector2.Lerp(Title.anchoredPosition, targetPos, Time.deltaTime * 2f);

            //yield return new WaitForSeconds(0.2f);
        }
        
    }

}
