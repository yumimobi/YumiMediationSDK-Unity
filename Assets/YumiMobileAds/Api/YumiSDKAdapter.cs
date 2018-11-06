using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class YumiSDKAdapter:MonoBehaviour {

	public static YumiSDKAdapter Instance;
// setup your app yumi ads placementid
	#if UNITY_IPHONE && !UNITY_EDITOR
	private string bannerPlacementID_iOS = "Your banner placementId for iOS";
	private string interstitialPlacementID_iOS = "Your interstitial placementId for iOS";
	private string videoPlacementID_iOS = "Your video placementId for iOS";
	private string channelID_iOS = "Your channel id for iOS";
	private string versionID_iOS = "Your version id for iOS";
	private YumiMediationSDK_Unity.YumiMediationBannerPosition position = YumiMediationSDK_Unity.YumiMediationBannerPosition.YumiMediationBannerPositionBottom;
	/// default: iPhone and iPod Touch ad size. Typically 320x50.
	/// default: iPad ad size. Typically 728x90.
	// private YumiMediationSDK_Unity.YumiMediationAdViewBannerSize bannerSize = YumiMediationSDK_Unity.YumiMediationAdViewBannerSize.kYumiMediationAdViewBanner320x50;
	#endif
	#if UNITY_ANDROID
	string bannerPlacementID_Android = "Your banner placementId for Android";
	string interstitialPlacementID_Android = "Your interstitial placementId for Android";
	string videoPlacementID_Android = "Your video placementId for Android";
	private string channelID_Android = "Your channel id for Android";
	private string versionID_Android = "Your version id for Android";
	private YumiUnityAdUtils androidAdInstance;

	private bool rotaIsMediaPrepared;
	private float timeNum;
	#endif

	void Awake()
	{   
		//Use as a singleton
		Instance = this;
		//Follow all the scenes
		DontDestroyOnLoad(gameObject);
		#if UNITY_ANDROID
		androidAdInstance = new YumiUnityAdUtils (bannerPlacementID_Android,interstitialPlacementID_Android, videoPlacementID_Android,channelID_Android,versionID_Android);
		#endif

	}
	void Start () {  

		Logger.LogError("start yumi adapter");
		//Remind users to get permission  
		#if UNITY_ANDROID
		androidAdInstance.CheckPermission();
		#endif
	}


	void Update()
	{
		#if UNITY_ANDROID
		//Ask about video every 5 seconds is prepared 
		if (!rotaIsMediaPrepared)
		{
			timeNum += Time.fixedDeltaTime;
			if (timeNum > 5f)
			{
				timeNum = 0;
				IsMediaPrepared();
			}
		}
		#endif
	}

	//banner 
	public void ShowBanner(bool isSmartBanner){
		Logger.LogError ("click init banner");
		#if UNITY_IPHONE && !UNITY_EDITOR
		YumiMediationSDK_Unity.initYumiMediationBanner(bannerPlacementID_iOS,channelID_iOS,versionID_iOS,position);
		// set banner custom size 
//		YumiMediationSDK_Unity.setBannerAdSize(bannerSize);
		YumiMediationSDK_Unity.loadAd(isSmartBanner);
		#endif

		//android 
		#if UNITY_ANDROID
		androidAdInstance.AddBannerAd(gameObject.name, isSmartBanner);

		#endif

	}

	public void DismissBanner(){
		Logger.LogError ("click reomve banner");
		#if UNITY_IPHONE && !UNITY_EDITOR

		YumiMediationSDK_Unity.removeBanner();
		#endif
		#if UNITY_ANDROID
		androidAdInstance.DismissBanner();

		#endif
	}

	//interstitial
	public void InitInterstitial(){
		
		#if UNITY_IPHONE && !UNITY_EDITOR
		YumiMediationSDK_Unity.initYumiMediationInterstitial(interstitialPlacementID_iOS,channelID_iOS,versionID_iOS);
		#endif

		#if UNITY_ANDROID
		androidAdInstance.InitInterstitialAd(gameObject.name);

		androidAdInstance.RequestInterstitial();

		#endif
	}

	public void PresentInterstitial(){
		#if UNITY_IPHONE && !UNITY_EDITOR
		bool isplay = YumiMediationSDK_Unity.isInterstitialReady();
		if (!isplay){
		Logger.LogError ("interstitial not ready");
		return;
		} 
		YumiMediationSDK_Unity.present ();
		#endif

		#if UNITY_ANDROID
		androidAdInstance.ShowInterstitialAd();

		#endif
	}

	//video 
	public void InitVideo(){
		#if UNITY_IPHONE && !UNITY_EDITOR
		YumiMediationSDK_Unity.loadYumiMediationVideo(videoPlacementID_iOS,channelID_iOS,versionID_iOS);
		#endif

		#if UNITY_ANDROID
		androidAdInstance.InitMedia(gameObject.name);

		androidAdInstance.RequestMedia();
		#endif
	}

	public void PlayVideo(){

		#if UNITY_IPHONE && !UNITY_EDITOR

		bool isplay = YumiMediationSDK_Unity.isVideoReady();
		if (!isplay){
		Logger.LogError ("video not ready");
		return;
		} 

		YumiMediationSDK_Unity.playVideo();
		#endif

		#if UNITY_ANDROID
		androidAdInstance.ShowMedia();

		#endif
	}

	public bool isVideoReady(){

		#if UNITY_IPHONE && !UNITY_EDITOR

		return YumiMediationSDK_Unity.isVideoReady();

		#endif

		#if UNITY_ANDROID

		return androidAdInstance.IsMediaPrepared();
		#endif

		return false;
	}

	#if UNITY_ANDROID
	public void AndroidStartDebug(){
		androidAdInstance.StartDebugging ();
	}
	public void SetAppIsGooglePlayVersions(){
		androidAdInstance.SetAppIsGooglePlayVersions ();
	}

	/// <summary>
	///  Video Is Prepared
	/// </summary>
	public void IsMediaPrepared()
	{
		bool isPrepared = androidAdInstance.IsMediaPrepared();
		if (isPrepared)
		{
			GetRotaIsMediaPrepared = true;

			//The video load completes the shutdown of the rotation request video logic           
			Logger.Log("yumiMobi SDK Media IsMediaPrepared : true");
		}
		else
		{
			//The video screen does not have 5 seconds to request a screen for loading
			Logger.Log("yumiMobi SDK Media IsMediaPrepared : false");
		}
	}

	/// <summary>
	/// Return to video to see if the AD is ready to be completed
	/// </summary>
	public bool GetRotaIsMediaPrepared
	{
		get
		{
			return rotaIsMediaPrepared;
		}
		set
		{
			rotaIsMediaPrepared = value;
		}
	}
	#endif

}
