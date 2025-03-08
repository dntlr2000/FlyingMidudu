using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class StageLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider progressBar;
    //public TextMeshProUGUI loadingText;
    //public Image fadeImage;

    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        loadingScreen.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progress;
            //loadingText.text = $"Loading.. {Mathf.RoundToInt(progress * 100)}";

            if (operation.progress >= 0.9f)
            {
                //loadingText.text = "Load Complete!";
                yield return new WaitForSeconds(1f);
                //yield return StartCoroutine(FadeOut());
                operation.allowSceneActivation = true;
                //yield return StartCoroutine(FadeIn());
            }

            yield return null;
        }
    }

    /*
    private IEnumerator FadeIn()
    {
        float duration = 1f;
        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = 1f - (elapsed / duration);
            fadeImage.color = color;
            yield return null;
        }
        color.a = 0f;
        fadeImage.color = color;
    }

    private IEnumerator FadeOut()
    {
        float duration = 1f;
        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = elapsed / duration;
            fadeImage.color = color;
            yield return null;
        }
        color.a = 1f;
        fadeImage.color = color;
    }
    */

}

/*
 * 사용 예시
 StageLoader sceneLoader = FindObjectOfType<StageLoader>();
 sceneLoader.LoadSceneAsync("Stage2"); 

SceneLoaderAsync 스크립트를 빈 GameObject에 추가.
loadingScreen, progressBar, loadingText를 Inspector에서 연결.
 메뉴 기준으로 UI 버튼의 OnClick() 이벤트에서 LoadSceneAsync("스테이지명") 실행.
 */