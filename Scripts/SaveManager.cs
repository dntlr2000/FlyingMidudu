using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class SaveManager : MonoBehaviour
{
    [Serializable]
    public class ClearProgress
    {
        //public string name;
        public int clearedStage;
    }

    public class PlayerState
    {
        public int life;
        public int bombs;
    }

    public class OptionData
    {
        public float MouseSpeed;
        public float BGM_Volume;
        public float SFX_Volume;
    }

    void Start()
    {
        LoadProgress();
    }

    public void SaveProgress(int newSave)
    {
        int previousSave = LoadProgress();
        if (previousSave >= newSave) { return; }

        ClearProgress data = new ClearProgress();
        data.clearedStage = newSave;

        string json = JsonUtility.ToJson(data);
        string path = Application.persistentDataPath + $"/save.json";
        File.WriteAllText(path, json);

        Debug.Log($"저장 경로: {path}\n 클리어한 스테이지: {newSave}");

    }

    public int LoadProgress()
    {
        string path = Application.persistentDataPath + $"/save.json";
        int clearedData = 0;
        if (!File.Exists(path))
        {
            Debug.LogWarning("저장된 위치 정보가 없습니다.");

            ClearProgress data2 = new ClearProgress();
            data2.clearedStage = 0;

            string json2 = JsonUtility.ToJson(data2);
            string path2 = Application.persistentDataPath + $"/save.json";
            File.WriteAllText(path, json2);

            Debug.Log($"저장 경로: {path}\n 클리어한 스테이지: {0}");
            return 0;
        }
        
        string json = File.ReadAllText(path);
        ClearProgress data = JsonUtility.FromJson<ClearProgress>(json);

        clearedData = data.clearedStage;
        Debug.Log($"불러오기 완료: {clearedData} 스테이지까지 클리어 하셨습니다.");


        return clearedData;
    }
}

    
