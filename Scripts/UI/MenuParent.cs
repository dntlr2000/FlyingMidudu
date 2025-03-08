using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuParent : MonoBehaviour
{
    public StageLoader loader;
    public GameObject optionsPanel;

    //private GameObject AudioObject;

    //public GameObject AudioObj;
    //private GameObject AudioObject;
    public BGMController BGM_Script;
    


    // Start is called before the first frame update
    protected virtual void Start()
    {
        //SetBGM();
        //BGM_Script = FindObjectOfType<BGMController>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    //Stage에서 가져옴
    public void StageLoader(string NextStageName, bool isInstant = true)
    {
        if (NextStageName == null) { return; }
        //isInstant: 메뉴 등에서 바로 넘어갈 필요가 있을 때 true로 설정

        if (BGM_Script != null) { BGM_Script.PauseBGM(); }
        

        if (isInstant)
        {
            loader.LoadSceneAsync(NextStageName);
        }
        else
        {
            StartCoroutine(toNextStageCoroutine(NextStageName));
        }

    }

    private IEnumerator toNextStageCoroutine(string sceneName)
    {
        yield return new WaitForSeconds(3f); //보스 사망 연출 시작으로부터 지연 시간
        loader.LoadSceneAsync(sceneName);
    }


    public void OpenOptions()
    {
        BGM_Script.PlaySFX(0);
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        //BGM_Script.PlaySFX(0);
        optionsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        BGM_Script.PlaySFX(0);
        Debug.Log("Game Quit");
        Application.Quit();
    }

    /*
    protected void SetBGM(int index = 0) //Stage와 코드 동일
    {
        /*
        BGM_Script = FindObjectOfType<BGMController>();
        //BGM_Script = AudioObject.GetComponent<BGMController>();
        
        AudioObject = Instantiate(AudioObj);
        BGM_Script = AudioObject.GetComponent<BGMController>();


        if (BGM_Script == null) {
            Debug.Log("BGMScript NOT FOUND");
            return; }
        Debug.Log("BGM_Script Found");
        BGM_Script.BgmIndex = index;
        BGM_Script.ChangeBGM(BGM_Script.BgmIndex);
        BGM_Script.PlayBGM();
    }
    */

}
