using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using YumiMediationSDK.Common;

public class YumiSDKAdapter : MonoBehaviour
{

    private string BannerPlacementId = "";
    private String RewardedVideoPlacementId = "";
    private String InterstitialsPlacementId = "";
    private string GameVersionID = "";
    private String ChannelId = "";

    private bool IsSmartBanner;

    // only iOS
#if UNITY_IOS
    private string SplashPlacementId = "8lmodwa9";
#endif
    public static YumiSDKAdapter Instance;

    // setup your app yumi ads placementid
#if UNITY_IOS
    private YumiMediationSDK_Unity.YumiMediationBannerPosition position = YumiMediationSDK_Unity.YumiMediationBannerPosition.YumiMediationBannerPositionBottom;
    /// default: iPhone and iPod Touch ad size. Typically 320x50.
    /// default: iPad ad size. Typically 728x90.
    // private YumiMediationSDK_Unity.YumiMediationAdViewBannerSize bannerSize = YumiMediationSDK_Unity.YumiMediationAdViewBannerSize.kYumiMediationAdViewBanner320x50;
#endif
#if UNITY_ANDROID
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
        //get ad info
        GameVersionID = MediationManagerSetting.GetGameVersion;
        IsSmartBanner = MediationManagerSetting.GetAutomaticAdaptionBanner;
#if UNITY_IOS
        ChannelId = MediationManagerSetting.GetIOSZChannelId;
        RewardedVideoPlacementId = MediationManagerSetting.GetIOSZRewardedVideoPlacementId;
        InterstitialsPlacementId = MediationManagerSetting.GetIOSZInterstitialsPlacementId;
        BannerPlacementId = MediationManagerSetting.GetIOSZBannelPlacementId;

#endif

#if UNITY_ANDROID

		ChannelId = MediationManagerSetting.GetAndroidZChannelId;
		RewardedVideoPlacementId = MediationManagerSetting.GetAndroidZRewardedVideoPlacementId;
		InterstitialsPlacementId = MediationManagerSetting.GetAndroidZInterstitialsPlacementId;
		BannerPlacementId = MediationManagerSetting.GetAndroidZBannelPlacementId;

#endif

        Logger.LogError("start yumi adapter");

#if UNITY_ANDROID
		androidAdInstance = new YumiUnityAdUtils (BannerPlacementId,InterstitialsPlacementId, RewardedVideoPlacementId,ChannelId,GameVersionID);

#endif

    }
    void Start()
    {

        Logger.LogError("start yumi adapter");

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
    public void ShowBanner()
    {
        Logger.LogError("click init banner");

        Logger.LogError("banner id = " + BannerPlacementId + "isSmart =" + IsSmartBanner);
#if UNITY_IOS
        YumiMediationSDK_Unity.initYumiMediationBanner(BannerPlacementId, ChannelId, GameVersionID, position);
        // set banner custom size 
        //		YumiMediationSDK_Unity.setBannerAdSize(bannerSize);
        YumiMediationSDK_Unity.loadAd(IsSmartBanner);
#endif

        //android 
#if UNITY_ANDROID
		androidAdInstance.AddBannerAd(gameObject.name, IsSmartBanner);

#endif

    }

    public void DismissBanner()
    {
        Logger.LogError("click reomve banner");
#if UNITY_IOS

        YumiMediationSDK_Unity.removeBanner();
#endif
#if UNITY_ANDROID
		androidAdInstance.DismissBanner();

#endif
    }

    //interstitial
    public void InitInterstitial()
    {

#if UNITY_IOS
        YumiMediationSDK_Unity.initYumiMediationInterstitial(InterstitialsPlacementId, ChannelId, GameVersionID);
#endif

#if UNITY_ANDROID
		androidAdInstance.InitInterstitialAd(gameObject.name);

		androidAdInstance.RequestInterstitial();

#endif
    }

    public void PresentInterstitial()
    {
#if UNITY_IOS
        bool isplay = YumiMediationSDK_Unity.isInterstitialReady();
        if (!isplay)
        {
            Logger.LogError("interstitial not ready");
            return;
        }
        YumiMediationSDK_Unity.present();
#endif

#if UNITY_ANDROID
		androidAdInstance.ShowInterstitialAd();

#endif
    }

    //video 
    public void InitVideo()
    {
#if UNITY_IOS
        YumiMediationSDK_Unity.loadYumiMediationVideo(RewardedVideoPlacementId, ChannelId, GameVersionID);
#endif

#if UNITY_ANDROID
		androidAdInstance.InitMedia(gameObject.name);

		androidAdInstance.RequestMedia();
#endif
    }

    public void PlayVideo()
    {

#if UNITY_IOS

        bool isplay = YumiMediationSDK_Unity.isVideoReady();
        if (!isplay)
        {
            Logger.LogError("video not ready");
            return;
        }

        YumiMediationSDK_Unity.playVideo();
#endif

#if UNITY_ANDROID
		androidAdInstance.ShowMedia();

#endif
    }

    public bool isVideoReady()
    {

#if UNITY_IOS

        return YumiMediationSDK_Unity.isVideoReady();

#endif

#if UNITY_ANDROID

		return androidAdInstance.IsMediaPrepared();
#endif

        return false;
    }

    public void CallDebugCenter()
    {
#if UNITY_IOS
        YumiMediationSDK_Unity.presentYumiMediationDebugCenter(BannerPlacementId, InterstitialsPlacementId, RewardedVideoPlacementId, "", ChannelId, GameVersionID);
#endif

#if UNITY_ANDROID
		androidAdInstance.StartDebugging ();
#endif
    }
    public bool GetDebugMode()
    {
        return MediationManagerSetting.GetDebugMode;
    }

#if UNITY_ANDROID
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


    // splash
#if UNITY_IOS
    public void RequestSplash()
    {
        YumiMediationSDK_Unity.showYumiAdsSplash(SplashPlacementId, "");
    }
#endif

}
