using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnimationInitObj : MonoBehaviour {
	public GameObject levelSelect,TapToPlay,MobilPuno,stageSelect,worldSelect,helpBox,settingBtn,buatSoalBtn,transitionOpen,glowScroll,efekCahaya,asap,bubbleResult
	,Star1,Star2,Star3,mainMenuBox,credit,popUpShare,popUpLogin,popUpBuatSoal,popUpUnlockHint,mainHintMenu,hintMenu,shopMenu,rouletteMenu,prizeMenu,rewardShare,selamatBermain,pointer,
	buyNotification,soalNotification,nextWorldNotification,exitNotification,rouletteNotification,
	rouletteStar,webNotification,moreGames,selectMode,resetStarConfirm,resetStarNotif,selectModeNotif,lineErrorNotif;

	public StarController timer;
	public Button submitButton,closeButton;
	public Image Img_roulette;
	public Animator Anim_LevelSelect,Anim_WorldSelect;
	public AudioClip suaraBan,suaraMembal,suaraKlakson,suaraScroll,musicMenu,sfxPrize,sfxLoseStar,sfxGotStar;
	public AudioSource bgmMenu,sfxMenu,sfxMenu2,sfxMenu3;

	#region sound
	public AudioSource GetBGMMenu(){
		return bgmMenu;
	}

	public AudioSource GetSFXMenu(){
		return sfxMenu;
	}

	public AudioSource GetSFXMenu2(){
		return sfxMenu2;
	}

	public AudioSource GetSFXMenu3(){
		return sfxMenu3;
	}

	public AudioClip GetMusicMenu(){
		return musicMenu;
	}

	public AudioClip GetSuaraBan(){
		return suaraBan;
	}

	public AudioClip GetSuaraMembal(){
		return suaraMembal;
	}

	public AudioClip GetSuaraKlakson(){
		return suaraKlakson;
	}

	public AudioClip GetSuaraScroll(){
		return suaraScroll;
	}

	public AudioClip GetSfxPrize(){
		return sfxPrize;
	}

	public AudioClip GetSfxLoseStar(){
		return sfxLoseStar;
	}

	public AudioClip GetSfxGotStar(){
		return sfxGotStar;
	}
	#endregion

	public Image GetImg_roulette(){
		return Img_roulette;
	}

	public GameObject GetWebNotification(){
		return webNotification;
	}

	public GameObject GetRouleteStar(){
		return rouletteStar;
	}

	public GameObject GetRouletteNotif(){
		return rouletteNotification;
	}

	public GameObject GetBuyNotification(){
		return buyNotification;
	}

	public GameObject GetSoalNotification(){
		return soalNotification;
	}

	public GameObject GetNextWorldNotification(){
		return nextWorldNotification;
	}

	public GameObject GetExitNotification(){
		return exitNotification;
	}

	public GameObject GetCredit(){
		return credit;
	}

	public GameObject GetLevelSelect(){
		return levelSelect;
	}

	public GameObject GetTapToPlay(){
		return TapToPlay;
	}

	public GameObject GetMobilPuno(){
		return MobilPuno;
	}

	public GameObject GetStageSelect(){
		return stageSelect;
	}

	public GameObject GetWorldSelect(){
		return worldSelect;
	}

	public GameObject GetHelpBox(){
		return helpBox;
	}

	public GameObject GetSettingBtn(){
		return settingBtn;
	}

	public GameObject GetBuatSoalBtn(){
		return buatSoalBtn;
	}

	public GameObject GetTransitionOpen(){
		return transitionOpen;
	}

	public GameObject GetGlowScroll(){
		return glowScroll;
	}

	public GameObject GetEfekCahaya(){
		return efekCahaya;
	}

	public GameObject GetAsap(){
		return asap;
	}

	public GameObject GetBubbleResult(){
		return bubbleResult;
	}

	public GameObject GetStar1(){
		return Star1;
	}

	public GameObject GetStar2(){
		return Star2;
	}

	public GameObject GetStar3(){
		return Star3;
	}

	public GameObject GetMainMenuBox(){
		return mainMenuBox;
	}

	public GameObject GetPopUpLogin(){
		return popUpLogin;
	}

	public GameObject GetPopUpBuatSoal(){
		return popUpBuatSoal;
	}

	public GameObject GetPopUpShare(){
		return popUpShare;
	}

	public GameObject GetPopUpUnlockHint(){
		return popUpUnlockHint;
	}

	public GameObject GetMainHintMenu(){
		return mainHintMenu;
	}

	public GameObject GetHintMenu(){
		return hintMenu;
	}

	public GameObject GetShopMenu(){
		return shopMenu;
	}

	public GameObject GetRouletteMenu(){
		return rouletteMenu;
	}

	public GameObject GetPrizeMenu(){
		return prizeMenu;
	}

	public GameObject GetRewardShare(){
		return rewardShare;
	}

	public GameObject GetSelamatBermain(){
		return selamatBermain;
	}

	public GameObject GetPointer(){
		return pointer;
	}

	public Button GetSubmitButton(){
		return submitButton;
	}

	public Button GetCloseButton(){
		return closeButton;
	}

	public Animator GetAnim_LevelSelect(){
		return Anim_LevelSelect;
	}

	public Animator GetAnim_WorldSelect(){
		return Anim_WorldSelect;
	}

	public GameObject GetMoreGamesObj(){
		return moreGames;
	}

	public GameObject GetSelectModeObj (){
		return selectMode;
	}

	public GameObject GetResetStarConfirmObj (){
		return resetStarConfirm;
	}

	public GameObject GetResetStarNotifObj (){
		return resetStarNotif;
	}

	public StarController GetTimer(){
		return timer;
	}

	public GameObject GetSelectModeNotif(){
		return selectModeNotif;
	}

	public GameObject GetLineErrorNotif (){
		return lineErrorNotif;
	}
}
