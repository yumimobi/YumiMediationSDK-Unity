using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using YumiMediationSDK.Api;
using YumiMediationSDK.Common;

public class YumiSDKDemo : MonoBehaviour
{

    private YumiBannerView bannerView;
    private YumiInterstitialAd interstitialAd;
    private YumiRewardVideoAd rewardVideoAd;


    private string BannerPlacementId = "";
    private String RewardedVideoPlacementId = "";
    private String InterstitialsPlacementId = "";
    private string GameVersionID = "";
    private String ChannelId = "";

    private bool IsSmartBanner;

    void Start()
    {
        // Whether to display log log
        Logger.SetDebug(true);

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
    }

    void OnGUI()
    {

        // Create style for a button
        GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
        myButtonStyle.fontSize = 25;

        // Load and set Font
        Font myFont = (Font)Resources.Load("Fonts/comic", typeof(Font));
        myButtonStyle.font = myFont;

        // Set color for selected and unselected buttons
        myButtonStyle.normal.textColor = Color.white;
        myButtonStyle.hover.textColor = Color.white;

        //Yumi banner
        int btnWidth = (Screen.width - 40 * 2 - 10) / 2;

        if (GUI.Button(new Rect(40, 84, btnWidth, 120), "request banner", myButtonStyle))
        {
            //YumiSDKAdapter.Instance.ShowBanner();
            if(this.bannerView != null)
            {
                this.bannerView.Destroy();
            }

            this.bannerView = new YumiBannerView(BannerPlacementId, ChannelId, GameVersionID,YumiAdPosition.Bottom);

            // banner add ad event
            this.bannerView.OnAdLoaded += this.HandleAdLoaded;
            this.bannerView.OnAdFailedToLoad += HandleAdFailedToLoad;
            this.bannerView.OnAdClick += HandleAdClicked;

            this.bannerView.LoadAd(IsSmartBanner);
        }
        //remove banner
        if (GUI.Button(new Rect(40 + btnWidth + 10, 84, btnWidth, 120), "reomve banner", myButtonStyle))
        {
            // iOS get banner size for developer layout
#if UNITY_IPHONE && !UNITY_EDITOR

			string bannerSize = YumiMediationSDK_Unity.fetchBannerAdSize ();
			string[] sizeArray = bannerSize.Split ('_');
			string width = "0.0";
			string height = "0.0";
			if(sizeArray.Length == 2)
			{
			width = sizeArray[0];
			height = sizeArray[1];
			}

			string sizeString = string.Format ("width = {0:N2} , height = {1:N2}",Convert.ToDouble(width),Convert.ToDouble(height));

			Debug.Log (sizeString);

#endif

            //YumiSDKAdapter.Instance.DismissBanner();
            if(this.bannerView != null){
                this.bannerView.Destroy();
            }

        }

        //Yumi interstital
        if (GUI.Button(new Rect(40, 214, btnWidth, 120), "request interstital", myButtonStyle))
        {

            //YumiSDKAdapter.Instance.InitInterstitial();
            if(this.interstitialAd != null){
                this.interstitialAd.DestroyInterstitial();
            }

            this.interstitialAd = new YumiInterstitialAd(InterstitialsPlacementId, ChannelId,GameVersionID);
            // add interstitial event 
            this.interstitialAd.OnAdLoaded += HandleInterstitialAdLoaded;
            this.interstitialAd.OnAdFailedToLoad += HandleInterstitialAdFailedToLoad;
            this.interstitialAd.OnAdClicked += HandleInterstitialAdClicked;
            this.interstitialAd.OnAdClosed += HandleInterstitialAdClosed;

        }

        if (GUI.Button(new Rect(40 + btnWidth + 10, 214, btnWidth, 120), "present interstital", myButtonStyle))
        {

            //YumiSDKAdapter.Instance.PresentInterstitial();
            if(this.interstitialAd.IsInterstitialReady()){
                this.interstitialAd.ShowInterstitial();
            }

        }

        //Yumi video
        if (GUI.Button(new Rect(40, 344, btnWidth, 120), "Load video", myButtonStyle))
        {
            //YumiSDKAdapter.Instance.InitVideo();
            if(this.rewardVideoAd != null){
                this.rewardVideoAd.DestroyRewardVideo();
            }
            this.rewardVideoAd = new YumiRewardVideoAd();
            this.rewardVideoAd.OnAdOpening += HandleRewardVideoAdOpened;
            this.rewardVideoAd.OnAdStartPlaying += HandleRewardVideoAdStartPlaying;
            this.rewardVideoAd.OnAdRewarded += HandleRewardVideoAdReward;
            this.rewardVideoAd.OnAdClosed += HandleRewardVideoAdClosed;

            this.rewardVideoAd.LoadRewardVideoAd(RewardedVideoPlacementId,ChannelId,GameVersionID);
        }

        if (GUI.Button(new Rect(40 + btnWidth + 10, 344, btnWidth, 120), "play video", myButtonStyle))
        {
            //YumiSDKAdapter.Instance.PlayVideo();
            if(this.rewardVideoAd.IsRewardVideoReady()){
                this.rewardVideoAd.PlayRewardVideo();
            }
        }
        if (YumiSDKAdapter.Instance.GetDebugMode())
        {
            if (GUI.Button(new Rect(40, 474, btnWidth, 120), "call debugCenter", myButtonStyle))
            {
                Logger.Log("call debugcenter");
                YumiSDKAdapter.Instance.CallDebugCenter();
            }
        }

#if UNITY_IOS
         if (GUI.Button(new Rect(40, 604, btnWidth, 120), "show splash", myButtonStyle))
        {
            Logger.Log("iOS splash");
            YumiSDKAdapter.Instance.RequestSplash();
        }

#endif


    }
    #region Banner callback handlers

    public void HandleAdLoaded(object sender, EventArgs args)
    {
        Logger.Log("HandleAdLoaded event received");
    }

    public void HandleAdFailedToLoad(object sender, YumiAdFailedToLoadEventArgs args)
    {
        Logger.Log("HandleFailedToReceiveAd event received with message: " + args.Message);
    }

    public void HandleAdClicked(object sender, EventArgs args)
    {
        Logger.Log("Handle Ad Clicked");
    }


    #endregion
    #region interstitial callback handlers

    public void HandleInterstitialAdLoaded(object sender, EventArgs args)
    {
        Logger.Log("HandleInterstitialAdLoaded event received");
    }

    public void HandleInterstitialAdFailedToLoad(object sender, YumiAdFailedToLoadEventArgs args)
    {
        Logger.Log("HandleInterstitialAdFailedToLoad event received with message: " + args.Message);
    }

    public void HandleInterstitialAdClicked(object sender, EventArgs args)
    {
        Logger.Log("HandleInterstitialAdClicked Clicked");
    }
    public void HandleInterstitialAdClosed(object sender, EventArgs args)
    {
        Logger.Log("HandleInterstitialAdClosed Ad closed");
    }


    #endregion
    #region reward video callback handlers

    public void HandleRewardVideoAdOpened(object sender, EventArgs args)
    {
        Logger.Log("HandleRewardVideoAdOpened event opened");
    }

    public void HandleRewardVideoAdStartPlaying(object sender, EventArgs args)
    {
        Logger.Log("HandleRewardVideoAdStartPlaying event start playing ");
    }

    public void HandleRewardVideoAdReward(object sender, EventArgs args)
    {
        Logger.Log("HandleRewardVideoAdReward reward");
    }
    public void HandleRewardVideoAdClosed(object sender, EventArgs args)
    {
        Logger.Log("HandleRewardVideoAdClosed Ad closed");
    }


    #endregion
}
