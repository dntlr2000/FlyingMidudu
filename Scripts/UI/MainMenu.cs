using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MenuParent
{
    public GameObject CreditScreen;
    
    protected override void Start()
    {
        //SetBGM(0);
        //optionsPanel.SetActive(false);
        //base.Start();
        BGM_Script = FindObjectOfType<BGMController>();
        SetBGM(0);
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

}
