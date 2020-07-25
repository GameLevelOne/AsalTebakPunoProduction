using System;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;

public class GPSController : MonoBehaviour
{
    public static GPSController Instance { get; private set; }
    #region Singelton
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            DestroyObject(this.gameObject);
        }
            
    }

    private void OnDestroy()
    {
        if(Instance == this)
            Instance = null;
    }

    #endregion Singelton

    public Action OnLoginSuccessful;
    public Action<string> OnGetName;
    public Action<bool> OnLoginSuccess;
    public Action<bool,string> OnSilentSignInSuccess;
    public Action OnSilentSignInFailed;
    public Action OnSignOut;
    public Action OnFinish;

    private string SAVE_NAME = "SaveGame";
    private CloudSave cloudSave;
    public Action<ISavedGameMetadata> OnSavedGameOpenedSuccess;

    private void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();

        // Enable debugging output (recommended)
        PlayGamesPlatform.DebugLogEnabled = true;

        // Initialize and activate the platform
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        cloudSave = new CloudSave();
    }

    public void SignInCallback(bool success)
    {
        if (success)
        {
            LoadGame();
            OnLoginSuccess(success);
            Debug.Log("Success");
            string name = Social.localUser.userName;
            OnGetName(name);
            OnLoginSuccessful();
        }
        else
        {
            Debug.Log("Sign-in failed...");
        }
    }

    public void SignIn()
    {
        if (!PlayGamesPlatform.Instance.localUser.authenticated)
        {
            // Sign in with Play Game Services, showing the consent dialog
            // by setting the second parameter to isSilent=false.
            PlayGamesPlatform.Instance.Authenticate(SignInCallback, false);
        }
    }

    public void SignOut()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            SaveGame();
            PlayGamesPlatform.Instance.SignOut();
            OnSignOut();
        }
    }

    public void SilentSignIn()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            LoadGame();
            OnSilentSignInSuccess(true, PlayGamesPlatform.Instance.GetUserDisplayName());
        }
        else
        {
            PlayGamesPlatform.Instance.Authenticate((bool success) => {
                if (success)
                {
                    LoadGame();
                    OnSilentSignInSuccess(success, PlayGamesPlatform.Instance.GetUserDisplayName());
                }
                else
                    OnSilentSignInFailed();
            }, true);
        }
    }

    public void SaveGame()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            OnSavedGameOpenedSuccess = SaveGame;
            OpenSavedGame("SaveGame");
        }
    }

    public void LoadGame()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            OnSavedGameOpenedSuccess = LoadGameData;
            OpenSavedGame("SaveGame");
        }
        
    }

    #region Save and Load Game
    void OpenSavedGame(string filename)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution(filename, DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpened);
    }

    public void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // handle reading or writing of saved game.
            cloudSave.GetSavedPlayerPrefs(); // Get local Game Data
            OnSavedGameOpenedSuccess(game);
        }
        else
        {
            // handle error
            Debug.Log("Error Status : " + status);
        }
    }

    void SaveGame(ISavedGameMetadata game)
    {
        byte[] savedData = cloudSave.LocalToCloud();
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
        builder = builder
            .WithUpdatedPlayedTime(TimeSpan.FromMinutes(game.TotalTimePlayed.Minutes + 1))
            .WithUpdatedDescription("Saved game at " + DateTime.Today);
        SavedGameMetadataUpdate updatedMetadata = builder.Build();
        savedGameClient.CommitUpdate(game, updatedMetadata, savedData, OnSavedGameWritten);
    }

    public void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // handle reading or writing of saved game.
            Debug.Log("Success Save File");
            cloudSave.DeleteSavedPlayerPrefs();
        }
        else
        {
            // handle error
            Debug.Log("Error Status : " + status);
        }
    }

    void LoadGameData(ISavedGameMetadata game)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ReadBinaryData(game, OnSavedGameDataRead);
    }

    public void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // handle processing the byte array data
            cloudSave.CloudToLocal(data);
            Debug.Log("Success Load File");
        }
        else
        {
            // handle error
            Debug.Log("Error Status : " + status);
        }
    }
    #endregion
}
