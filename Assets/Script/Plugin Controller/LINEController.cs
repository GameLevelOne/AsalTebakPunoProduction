using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LGCoreLite;

public class LINEController : MonoBehaviour {
	public static LINEController lineControllerInstance;

	public GameObject loginScreen,punoLogo;
	public GameObject btnLineLogin, btnLinePlay, btnLineLogout, btnFBLogin, btnFBPlay, btnFBLogout, btnGuestLogin, btnGuestPlay, btnBack;
	public GameObject errorNotif;

	public string       appId           = "LGPUNO";
	public int          timeoutInterval = 20;
	public LGCLogLevel  logLevel        = LGCLogLevel.LGCLogLevelDebug;
	LGCPhase 	phase 			= LGCPhase.LGCPhaseReal;

	private bool m_initialized = false;

	/// <summary>
	/// for Growthy login id:
	/// pgs: punoguest
	/// pfb: punofb
	/// </summary>

	void Awake ()
	{
		lineControllerInstance = this;

		if (!GameData._lineInit) {
			LineInitialize ();
			GameData._lineInit=true;
		}
	}

	// Use this for initialization
	void Start () {
	}

	private void LineInitialize(){
		Debug.Log( "initialize");
		
#if !UNITY_EDITOR
		using( LGCoreLiteAPI instance = LGCoreLiteAPIFactory.createInstance() ) {
			if( instance != null ) {
				
//				LGCoreLiteAPI.OnRetryLogin = () => {
//						Debug.Log( "OnRetryLogin" );
//						//PostLogout();
//				};
			
				LGCoreLiteAPI.OnLoginResult = ( LGCLoginState loginState, UserAuth authInfo, Error error ) => {

						Debug.Log( "OnLoginResult: [" + loginState + "] " + authInfo );
						if( loginState == LGCLoginState.LGCLoginStateSuccess ) {
							if( authInfo != null ) {
								Debug.Log( "access token: " + authInfo.getAccessToken() );
								if( authInfo.getUserProfile() != null ) {
									postLogin(authInfo.getUserProfile().getDisplayName());
								}
								GameData.loginTypeValue=(int)eLoginType.LINE;
							}
						}
						else if( error != null ) {
							
							if(error.getCode() == Error.ErrorCode.EC_LOGIN_WEB_AUTH_FAIL)
							{
								Debug.Log("web login error, you can display dialog to remind user about wrong account or password");
								errorNotif.SetActive(true);
								LINEErrorNotif.instance.setNotifText("Wrong username or password");
							}
//							else{
//								errorNotif.SetActive(true);
//								LINEErrorNotif.instance.setNotifText("Error[" + error.getCode() + "]: " + error.getMessage());
//							}

							Debug.Log( "Error[" + error.getCode() + "]: " + error.getMessage() );
							//PostLogout();
						}

				};

				Debug.Log( "Helga Initializing: " + appId + ", " + logLevel + ", " + timeoutInterval + ", " + phase);

				LGCInitState state = instance.init( appId, logLevel, phase, timeoutInterval );

				Debug.Log( "Initializing: " + appId + ", " + logLevel + ", " + timeoutInterval + ", " + phase);
		
				if( state ==  LGCInitState.LGCInitStateSuccess ) {

					//enable NELO crash report after init (optinal)
					instance.enableCrashReport();

					Debug.Log( "Helga LGCoreLiteAPI Initialization Success. phase = " + phase );
					m_initialized = true;
					//return true;

					instance.isAuthValid((bool isValid, Error error) => 

						{
							if(error == null){
								Debug.Log("IsAuthValid: "+isValid);
								if(!isValid){
									Debug.Log("auth not valid");
									//show welcome screen
									if(GameData.loginTypeValue == (int)eLoginType.LINE){
										loginScreen.SetActive(true);
										punoLogo.SetActive(false);
									}
								}
								else{
									Debug.Log("auth valid");
//									instance.login(LGCoreLite.LoginInfo.buildLineLoginInfo());
									onLineLoginClicked();
									loginScreen.SetActive(false);
									punoLogo.SetActive(true);
									MenuSceneController.instance.setDisplayName(GameData.loginUserNameValue);
									GameData._isLoggedIn=true;

								}
							}
							else{
								Debug.Log("call isAuthValid encountered unexpected error, don't rely on the isValid result");
							}
						}
					);

				}
				else {
					Debug.Log( "LGCoreLiteAPI Initialization Failed: " + state );
				}
			}
			else {
				Debug.Log( "Initialization Failed: Cannot create LGCoreLiteAPI instance" );
			}
		}
		System.GC.Collect();
		//m_initialized = false;
#else
		m_initialized = true;
#endif
	}

	public void onLineLoginClicked()
	{
		if( m_initialized ) {
			// do login
			Debug.Log( "start login..." );

#if !UNITY_EDITOR
			using( LGCoreLiteAPI instance = LGCoreLiteAPIFactory.getInstance() ) {
				if( instance != null ) {
					instance.login(LGCoreLite.LoginInfo.buildLineLoginInfo());
				}
				else {
					// TODO: show error
				}
			}
#endif
		}
		else {
			//TODO: show error
			Debug.Log("error");
		}

		System.GC.Collect();
	}

	private void postLogin (string username){
//		#if UNITY_ANDROID
//			LGCoreLiteAPI.OnLoginResult = ( LGCLoginState loginState, UserAuth authInfo, Error error ) => {
//
//				Debug.Log( "OnLoginResult: [" + loginState + "] " + authInfo );
//				if( loginState == LGCLoginState.LGCLoginStateSuccess ) {
//					if( authInfo != null ) {
//						Debug.Log( "access token: " + authInfo.getAccessToken() );
//						if( authInfo.getUserProfile() != null ) {
//							
//						}
//					}
//					else if( error != null ) {
//						
//						if(error.getCode() == Error.ErrorCode.EC_LOGIN_WEB_AUTH_FAIL)
//						{
//							Debug.Log("web login error, you can display dialog to remind user about wrong account or password");
//							errorNotif.SetActive(true);
//							LINEErrorNotif.instance.setNotifText("Wrong username or password");
//						}
//	//					else{
//	//						errorNotif.SetActive(true);
//	//						LINEErrorNotif.instance.setNotifText("Error[" + error.getCode() + "]: " + error.getMessage());
//	//					}
//
//						Debug.Log( "Error[" + error.getCode() + "]: " + error.getMessage() );
//						//PostLogout();
//					}
//				}
//			};
//		#else
//			btnLineLogin.SetActive(false);
//		#endif

		GameData.loginTypeValue = (int)eLoginType.LINE;

		GameData.loginUserNameValue = username;
		btnLineLogin.SetActive(false);
		btnLinePlay.SetActive(true);
		btnFBLogin.SetActive(false);
		btnFBPlay.SetActive(false);
		btnGuestLogin.SetActive(false);
		btnGuestPlay.SetActive (false);
		btnBack.SetActive (false);

		onClickPlay ();
	}

	public void LineLogout(){
		#if !UNITY_EDITOR
		using( LGCoreLiteAPI instance = LGCoreLiteAPIFactory.getInstance() ) {
			if( instance != null ) {
				instance.deleteAuthInfo();
			}
			else {
				// TODO: show error
			}
		}
		#endif
		LinePostLogout();
	}

	private void LinePostLogout (){
		btnLineLogout.SetActive(false);
		btnLinePlay.SetActive(false);
		btnLineLogin.SetActive(true);
		btnFBLogin.SetActive(true);
		btnGuestLogin.SetActive(true);
		loginScreen.SetActive(true);
		punoLogo.SetActive(false);
	}

	private void FBPostLogout(){
		btnFBLogout.SetActive(false);
		btnFBPlay.SetActive(false);
		btnFBLogin.SetActive(true);
		btnLineLogin.SetActive(true);
		btnGuestLogin.SetActive(true);
		loginScreen.SetActive(true);
		punoLogo.SetActive(false);
	}

	public void OnFbLoginSuccessful ()
	{
		GameData.loginTypeValue=(int)eLoginType.FACEBOOK;
		string fbLoginID = "pfb"+GameData.totalPlayer.ToString();

		#if !UNITY_EDITOR
		using( LGCoreLiteAPI instance = LGCoreLiteAPIFactory.getInstance() ) {
				if( instance != null ) {
					instance.login(LGCoreLite.LoginInfo.buildLoginInfo(LoginInfo.LoginType.LGFacebookLogin,fbLoginID));
				}
				else {
					// TODO: show error
				}
			}
		#endif

		GameData.totalPlayer++;

		Debug.Log("fb login id: "+fbLoginID);
		Debug.Log("on login fb success");
		btnFBLogin.SetActive(false);
		btnFBPlay.SetActive(true);
		btnLineLogin.SetActive(false);
		btnLinePlay.SetActive(false);
		btnGuestLogin.SetActive(false);
		btnGuestPlay.SetActive (false);
		btnBack.SetActive (false);
	}

	public void ButtonFBLogin_OnClick(){
		FBController.instance.CallFBLogin();
	}

	public void ButtonFBLogout_OnClick (){
		FBController.instance.CallFBLogout();
		FBPostLogout();
	}

	public void onClickPlay(){
		loginScreen.SetActive(false);
		MenuSceneController.instance.setDisplayName(GameData.loginUserNameValue);
		punoLogo.SetActive(true);
		GameData._isLoggedIn = true;
	}

	public void onClickGuestPlay(){
		GameData.loginTypeValue = (int)eLoginType.GUEST;
		string guestID = "pgs"+GameData.totalPlayer.ToString();

		#if !UNITY_EDITOR
		using( LGCoreLiteAPI instance = LGCoreLiteAPIFactory.getInstance() ) {
				if( instance != null ) {
					instance.login(LGCoreLite.LoginInfo.buildGuestLoginInfo(guestID));
				}
				else {
					// TODO: show error
				}
			}
		#endif

		GameData.totalPlayer++;

		Debug.Log("guest login id: "+guestID);
		loginScreen.SetActive(false);
		MenuSceneController.instance.setDisplayName("Guest");
		punoLogo.SetActive(true);
		GameData._isLoggedIn = true;
	}

	public void showLoginScreen (int loginType)
	{
		switch (loginType) {
		case (int)eLoginType.GUEST:
			btnLineLogout.SetActive (false);
			btnFBLogout.SetActive (false);
			btnGuestLogin.SetActive (false);
			btnGuestPlay.SetActive (false);
			btnBack.SetActive (true);
			break;

		default:
			btnLineLogout.SetActive (false);
			btnLinePlay.SetActive (false);
			btnLineLogin.SetActive (false);
			btnFBLogin.SetActive (false);
			btnFBLogout.SetActive (false);
			btnFBPlay.SetActive (false);
			btnGuestLogin.SetActive (false);
			btnGuestPlay.SetActive (false);
			btnBack.SetActive (true);
		break;
		}
	}

	public void backButton(){
		loginScreen.SetActive (false);
		punoLogo.SetActive (true);
	}
}
