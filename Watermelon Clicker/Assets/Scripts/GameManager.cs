﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    instance = new GameObject("GameManager").AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    [SerializeField]
    private Text watermelonAmount = null; //가지고 있는 수박의 양
    [SerializeField]
    private Text[] watermelonObjectAmount = null; //UI 상에서 생산시설을 몇개 가지고 있는지 보여주는 텍스트

    private string SAVE_PATH = "";
    private string SAVE_FILENAME = "/SaveFile.txt";
    [SerializeField]
    private PlayerData data = null;
    public PlayerData playerData { get { return data; } }


    public void UpdateUI()
    {
        watermelonAmount.text = string.Format("{0}", playerData.watermelonAmount); // 수박 보유 수를 표시
        for(int i = 0; i < watermelonObjectAmount.Length; i++)
        {
            watermelonObjectAmount[i].text = string.Format("Have {0}", playerData.watermelonObjectAmount[i]); // 생산시설 보유 수 텍스트를 표시하는 코드
        }
        SaveToJson();
    }

    private void Awake()
    {
        SAVE_PATH = Application.dataPath + "/Save";

        if (Directory.Exists(SAVE_PATH) == false)
        {
            Directory.CreateDirectory(SAVE_PATH);
        }
        InvokeRepeating("SaveToJson", 1f, 10f);
        LoadFromJson();
    }

    private void LoadFromJson()
    {
        string json = "";
        if (File.Exists(SAVE_PATH + SAVE_FILENAME) == true)
        {
            json = File.ReadAllText(SAVE_PATH + SAVE_FILENAME);
            data = JsonUtility.FromJson<PlayerData>(json);
        }
    }

    private void SaveToJson()
    {
        SAVE_PATH = Application.dataPath + "/Save";
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SAVE_PATH + SAVE_FILENAME, json, System.Text.Encoding.UTF8);
    }

    private void OnApplicationPause(bool pause)
    {
        SaveToJson();
    }
    private void OnApplicationQuit()
    {
        SaveToJson();
    }


    [System.Serializable]

    public class PlayerData
    {
        //저장할 데이터
        public int watermelonAmount = 0;
        public int wmPerClick = 1;
        public int wps = 0;

        public int[] watermelonObjectAmount = new int[5]; // 생산시설 보유 수
        public int[] watermelonObjectPrice = { 20, 80, 3000, 6000, 10000 }; // 생산시설마다 현재 가격
    }
}
