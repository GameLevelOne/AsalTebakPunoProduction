using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class CloudSave : MonoBehaviour
{
    private Dictionary<string, int> _savedPlayerData = new Dictionary<string, int>();

    public CloudSave()
    {
        init();
    }

    void init()
    {
        GetSavedPlayerPrefs();
        //string json = SavedPlayersPrefsToJSON();
        //var parsData = SavedPlayersPrefsFromJSON(json);
        //if (IsSameDict(_savedPlayerData, parsData))
        //    Debug.Log("Same");
        //else
        //    Debug.Log("Not Same");
        //GetSavedPlayerPrefsTester();
    }

    /// <summary>
    /// Only Call When SignOut
    /// </summary>
    public void DeleteSavedPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        
        // Initialize Primer Key
        for (int i = 1; i < 8; i++)
        {
            string key = "powerUp" + i.ToString();
            PlayerPrefs.SetInt(key, 0);
        }
        PlayerPrefs.SetInt("Pref/BannerCount", 0);
        PlayerPrefs.SetInt("Pref/PosterShown", 0);

        PlayerPrefs.Save();
    }

    private void SetSavedPlayerPrefs(Dictionary<string, int> savedPlayerData)
    {
        Debug.Log("Start Save Data In Local");

        if (savedPlayerData.ContainsKey("TotalKey"))
            savedPlayerData.Remove("TotalKey");

        foreach (var save in savedPlayerData)
            PlayerPrefs.SetInt(save.Key, save.Value);
        PlayerPrefs.Save();

        Debug.Log("End Save Data In Local");
    }

    public void GetSavedPlayerPrefs()
    {
        _savedPlayerData.Clear();
        Debug.Log("Start Load Data In Local");
        int totalKey = 0;
        List<string> keys = GetSavedPlayerPrefsAllKey();
        foreach (string key in keys)
            if (GameData.CheckKeyPlayerPrefs(key))
            {
                _savedPlayerData.Add(key, GameData.GetIntValuePlayerPrefs(key));
                totalKey++;
            }

        _savedPlayerData.Add("TotalKey", totalKey);
        Debug.Log("End Load Data In Local");
    }

    private List<string> GetSavedPlayerPrefsAllKey()
    {
        Debug.Log("Start Get key Data In Local");

        // Initialize Data
        List<string> intSavedKey = new List<string>()
        { "GilaMode/Star", "EnglishMode/Star","SelectedWorld","WorldComic",
        "firstPlay", "starCurrency"};

        // Power Up Data
        for (int i = 1; i < 8; i++)
            intSavedKey.Add("powerUp" + i.ToString());

        // Star Data
        for (int World = 0; World <= GameData.TotalWorld; World++)
            for (int Stage = 0; Stage <= GameData.TotalStagePerWorld; Stage++)
                intSavedKey.Add("StarWorld" + World.ToString() + "Stage" + Stage.ToString());

        // Unlock Data
        for (int i = 1; i < GameData.TotalWorld; i++)
            intSavedKey.Add("unlockWorld" + i.ToString());

        // Gila Mode Data
        intSavedKey.Add(GameData.GilaMode.KEY_GILAMODEUNLOCKED);
        intSavedKey.Add("firstPlayGilaMode");
        for (int World = 0; World <= GameData.TotalWorld; World++)
            for (int Stage = 0; Stage <= GameData.TotalStagePerWorld; Stage++)
                intSavedKey.Add(GameData.GilaMode.GetKeyStar(World, Stage));

        // English Mode Data
        intSavedKey.Add(GameData.EnglishMode.KEY_ENGLISHMODEUNLOCKED);
        intSavedKey.Add("firstPlayEnglishMode");
        for (int World = 0; World <= GameData.TotalWorld; World++)
            for (int Stage = 0; Stage <= GameData.TotalStagePerWorld; Stage++)
                intSavedKey.Add(GameData.EnglishMode.GetKeyStar(World, Stage));

        Debug.Log("End Get Key Data In Local");

        return intSavedKey;
    }

    public byte[] LocalToCloud()
    {
        string save = SavedPlayersPrefsToJSON();
        return Encoding.UTF8.GetBytes(save);
    }

    public void CloudToLocal(byte[] cloudData)
    {
        if(cloudData.Length > 0)
        {
            string tmp = Encoding.UTF8.GetString(cloudData);
            Dictionary<string, int> cloudSave = SavedPlayersPrefsFromJSON(tmp);
            if (cloudSave["TotalKey"] >= _savedPlayerData["TotalKey"])
            {
                _savedPlayerData = cloudSave;
                SetSavedPlayerPrefs(cloudSave);
            }
        }
        
    }

    #region JSON Utility

    private string SavedPlayersPrefsToJSON()
    {
        string json = JsonConvert.SerializeObject(_savedPlayerData);
        Debug.Log(json);
        return json;
    }

    private Dictionary<string, int> SavedPlayersPrefsFromJSON(string json)
    {
        Dictionary<string, int> parserData =
            JsonConvert.DeserializeObject<Dictionary<string, int>>(json);
        return parserData;
    }

    #endregion


    #region Testing 

    private void GetSavedPlayerPrefsTester()
    {
        _savedPlayerData.Clear();
        Debug.Log("Start Load Data In Local");

        // Initialize Data
        _savedPlayerData.Add("GilaMode/Star", 0);
        _savedPlayerData.Add("EnglishMode/Star", 0);
        _savedPlayerData.Add("SelectedWorld", 13);
        _savedPlayerData.Add("WorldComic", 3);
        _savedPlayerData.Add("firstPlay", 0);
        _savedPlayerData.Add("starCurrency", 100);


        // Power Up Data
        /// powerUp1 -> powerUp7
        for (int i = 1; i < 8; i++)
        {
            string key = "powerUp" + i.ToString();
            _savedPlayerData.Add(key, 10);
        }

        // Star Data
        for (int World = 0; World <= GameData.TotalWorld; World++)
        {
            for (int Stage = 0; Stage <= GameData.TotalStagePerWorld; Stage++)
            {
                string StarDataPrefKey = "StarWorld" + World.ToString() + "Stage" + Stage.ToString();
                _savedPlayerData.Add(StarDataPrefKey, 3);
            }
        }

        // Unlock Data
        for (int i = 1; i < GameData.TotalWorld; i++)
        {
            string key = "unlockWorld" + i.ToString();
            _savedPlayerData.Add(key, i);
        }

        // Gila Mode Data
        _savedPlayerData.Add(GameData.GilaMode.KEY_GILAMODEUNLOCKED, 1);
        _savedPlayerData.Add("firstPlayGilaMode", 0);
        for (int World = 0; World <= GameData.TotalWorld; World++)
        {
            for (int Stage = 0; Stage <= GameData.TotalStagePerWorld; Stage++)
            {
                _savedPlayerData.Add(GameData.GilaMode.GetKeyStar(World, Stage), 3);
            }
        }

        // English Mode Data
        _savedPlayerData.Add(GameData.EnglishMode.KEY_ENGLISHMODEUNLOCKED, 1);
        _savedPlayerData.Add("firstPlayEnglishMode", 0);
        for (int World = 0; World <= GameData.TotalWorld; World++)
        {
            for (int Stage = 0; Stage <= GameData.TotalStagePerWorld; Stage++)
            {
                _savedPlayerData.Add(GameData.EnglishMode.GetKeyStar(World, Stage), 3);
            }
        }

        Debug.Log("End Load Data In Local");
        DeleteSavedPlayerPrefs();
        SetSavedPlayerPrefs(_savedPlayerData);
    }

    private bool IsSameDict(Dictionary<string,int> a, Dictionary<string, int> b)
    {
        foreach (var item in a)
            if (item.Value != b[item.Key])
                return false;
        return true;
    }

    #endregion
}
