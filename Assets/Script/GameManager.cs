using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    #region Singleton

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    #endregion 

    public Dictionary<string, int> savedPlayerData = new Dictionary<string, int>();

    // Start is called before the first frame update
    void Start()
    {
        GetSavedPlayerPrefs();
    }

    public void ShowPlayerPrefabs()
    {
        var star = GameData.Star_World1;
        var worldName = GameData.worldNameList;
        var totalWorld = GameData.TotalWorld;
        var totalStage = GameData.TotalStagePerWorld;
    }

    public void SetSavedPlayerPrefs()
    {
        Debug.Log("Start Save Data");
        int[] tempWorldStages = new int[90];
        for (int World = 1; World <= GameData.TotalWorld; World++)
        {
            switch (World)
            {
                case 1: tempWorldStages = GameData.Star_World1; break;
                case 2: tempWorldStages = GameData.Star_World2; break;
                case 3: tempWorldStages = GameData.Star_World3; break;
                case 4: tempWorldStages = GameData.Star_World4; break;
                case 5: tempWorldStages = GameData.Star_World5; break;
                case 6: tempWorldStages = GameData.Star_World6; break;
                case 7: tempWorldStages = GameData.Star_World7; break;
                case 8: tempWorldStages = GameData.Star_World8; break;
                case 9: tempWorldStages = GameData.Star_World9; break;
                case 10: tempWorldStages = GameData.Star_World10; break;
                case 11: tempWorldStages = GameData.Star_World11; break;
                case 12: tempWorldStages = GameData.Star_World12; break;
            }

            for (int Stage = 0; Stage < GameData.TotalStagePerWorld; Stage++)
            {
                string StarDataPrefKey = "StarWorld" + World.ToString() + "Stage" + Stage.ToString();
                // Debug.Log(StarDataPrefKey + " " + tempWorldStages[Stage]);
                // PlayerPrefs.SetInt(StarDataPrefKey, tempWorldStages[Stage]);
            }
        }
        Debug.Log("End Save Data");
    }

    public void GetSavedPlayerPrefs()
    {
        savedPlayerData.Clear();
        Debug.Log("Start Load Data");
        for (int World = 0; World <= GameData.TotalWorld; World++)
        {
            for (int Stage = 0; Stage < GameData.TotalStagePerWorld; Stage++)
            {
                string StarDataPrefKey = "StarWorld" + World.ToString() + "Stage" + Stage.ToString();
                savedPlayerData.Add(StarDataPrefKey, PlayerPrefs.GetInt(StarDataPrefKey));
                // Debug.Log(StarDataPrefKey + " " + PlayerPrefs.GetInt(StarDataPrefKey));
            }
        }
        Debug.Log("End Load Data");
    }
}
