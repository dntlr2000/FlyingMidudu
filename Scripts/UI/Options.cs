using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider SFXSlider;
    public Slider RotationSlider;

    public BGMController BGM_Script;
    private PlayerSetting PlayerSettingScript;

    public SaveManager saveManager;

    void Start()
    {
        //volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1.0f);
        //AudioListener.volume = volumeSlider.value;
        //BGM_Script = FindObjectOfType<BGMController>();
        //PlayerSettingScript = FindObjectOfType<PlayerSetting>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitOption();
        }

        //volumeSlider.value = BGM_Script.SettedVolume;
    }


    void OnEnable()
    {
        BGM_Script = FindObjectOfType<BGMController>();
        PlayerSettingScript = FindObjectOfType<PlayerSetting>();

        saveManager = FindObjectOfType<SaveManager>();
        SaveManager.OptionData data = saveManager.LoadOptions();
        if (data == null)
        {
            volumeSlider.value = 0.5f;
            OnVolumeChanged(0.5f);

            SFXSlider.value = 0.5f;
            OnSFX_VolChanged(0.5f);

            RotationSlider.value = 240f;
            OnRotSpeedChanged(240f);
            return;
        }
        
        volumeSlider.value = data.BGM_Volume;
        OnVolumeChanged(data.BGM_Volume);

        SFXSlider.value = data.SFX_Volume;
        OnSFX_VolChanged(data.SFX_Volume);

        RotationSlider.value = data.MouseSpeed;
        OnRotSpeedChanged(data.MouseSpeed);

        return;
    }
    

    public void OnVolumeChanged(float value)
    {
        //AudioListener.volume = value;
        //PlayerPrefs.SetFloat("Volume", value);
        BGM_Script.SettedVolume = value;
        BGM_Script.SetBGMVolume(value);
    }

    public void OnSFX_VolChanged(float value)
    {
        BGM_Script.SetSFXVolume(value);
    }

    public void OnRotSpeedChanged(float value)
    {
        PlayerSettingScript.MouseSpeed = value;
    }

    public void ExitOption()
    {
        saveManager.SaveOptions(RotationSlider.value, volumeSlider.value, SFXSlider.value);
        ChangeRotationOption();
        gameObject.SetActive(false);
    }

    public void ChangeRotationOption()
    {
        CameraController camObj = FindAnyObjectByType<CameraController>();
        if (camObj != null)
        {
            camObj.SetRotationSpeed(PlayerSettingScript.MouseSpeed, PlayerSettingScript.MouseSpeed);
            Debug.Log($"감도 설정: {PlayerSettingScript.MouseSpeed}");
        }
    }
}
