using System;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

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

    private void Start()
    {
        // PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();

        // Enable debugging output (recommended)
        PlayGamesPlatform.DebugLogEnabled = true;

        // Initialize and activate the platform
        // PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        //PlayGamesPlatform.Instance.Authenticate((bool success) => {
        //    Debug.Log("Success");
        //    // if (OnFinish != null) OnFinish();
        //});
    }

    public void SignInCallback(bool success)
    {
        if (success)
        {
            OnLoginSuccess(success);
            Debug.Log("Success");
            string name = Social.localUser.userName; //PlayGamesPlatform.Instance.GetUserDisplayName();
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
            // Social.localUser.Authenticate(SignInCallback);
        }
    }

    public void SignOut()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.SignOut();
            OnSignOut();
        }
    }

    public void SilentSignIn()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            OnSilentSignInSuccess(true, PlayGamesPlatform.Instance.GetUserDisplayName());
        }
        else
        {
            PlayGamesPlatform.Instance.Authenticate((bool success) => {
                if (success)
                    OnSilentSignInSuccess(success, PlayGamesPlatform.Instance.GetUserDisplayName());
                else
                    OnSilentSignInFailed();
            }, true);
        }
    }
        

    public void SaveGame()
    {

    }

    public void LoadGame()
    {

    }
}
