using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using YumiMediationSDK.Api;
using YumiMediationSDK.Common;
using UnityEngine.SceneManagement;

public class YumiMainDemoScript : MonoBehaviour
{

    private YumiBannerView bannerView;
    private YumiInterstitialAd interstitialAd;
    private YumiRewardVideoAd rewardVideoAd;
    private YumiSplashAd splashAd;

    private YumiDebugCenter debugCenter;

    private string BannerPlacementId = "";
    private string RewardedVideoPlacementId = "";
    private string InterstitialsPlacementId = "";
    private string NativeAdPlacementId = "";
    private string SplashPlacementId = "";
    private string GameVersionId = "";
    private string ChannelId = "";

    // gdpr consent status
    private string gdprBtnInfo;
    private bool isPersonalized = false;

    void Start()
    {
        // set gdpr button value
        gdprBtnInfo = "GDPR Consent Status is unknown";
        // Whether to display log log
        Logger.SetDebug(true);

        //get ad info
        GameVersionId = YumiMediationSDKSetting.GetGameVersion;

        ChannelId = YumiMediationSDKSetting.ChannelId();
        RewardedVideoPlacementId = YumiMediationSDKSetting.RewardVideoPlacementId();
        InterstitialsPlacementId = YumiMediationSDKSetting.InterstitialPlacementId();
        BannerPlacementId = YumiMediationSDKSetting.BannerPlacementId();
        NativeAdPlacementId = YumiMediationSDKSetting.NativeAdPlacementId();
        SplashPlacementId = YumiMediationSDKSetting.SplashPlacementId();

        debugCenter = new YumiDebugCenter();
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
        int bannerBtnWidth = (Screen.width - 40 * 2 - 10) / 3;
        if (GUI.Button(new Rect(40, 84, bannerBtnWidth, 120), "request banner", myButtonStyle))
        {

            if (this.bannerView == null)
            {
                YumiBannerViewOptions bannerOptions = new YumiBannerViewOptionsBuilder().Build();
                this.bannerView = new YumiBannerView(BannerPlacementId, ChannelId, GameVersionId, bannerOptions);
                // banner add ad event
                this.bannerView.OnAdLoaded += HandleAdLoaded;
                this.bannerView.OnAdFailedToLoad += HandleAdFailedToLoad;
                this.bannerView.OnAdClick += HandleAdClicked;
            }


            this.bannerView.LoadAd();

        }
        //remove banner
        if (GUI.Button(new Rect(40 + bannerBtnWidth + 10, 84, bannerBtnWidth, 120), "hide banner", myButtonStyle))
        {
            if (this.bannerView != null)
            {
                this.bannerView.Hide();
            }

        }

        //show banner
        if (GUI.Button(new Rect(40 + bannerBtnWidth * 2 + 10, 84, bannerBtnWidth , 120), "show banner", myButtonStyle))
        {
            if (this.bannerView != null)
            {
                this.bannerView.Show();
            }

        }

        //Yumi interstital
        if (GUI.Button(new Rect(40, 214, btnWidth, 120), "request interstital", myButtonStyle))
        {

            if (this.interstitialAd == null)
            {
                this.interstitialAd = new YumiInterstitialAd(InterstitialsPlacementId, ChannelId, GameVersionId);
                // add interstitial event 
                this.interstitialAd.OnAdLoaded += HandleInterstitialAdLoaded;
                this.interstitialAd.OnAdFailedToLoad += HandleInterstitialAdFailedToLoad;
                this.interstitialAd.OnAdClicked += HandleInterstitialAdClicked;
                this.interstitialAd.OnAdClosed += HandleInterstitialAdClosed;
                this.interstitialAd.OnAdFailedToShow += HandleInterstitialAdFailedToShow;
                this.interstitialAd.OnAdOpening += HandleInterstitialAdOpened;
                this.interstitialAd.OnAdStartPlaying += HandleInterstitialAdStartPlaying;
            }       

        }

        if (GUI.Button(new Rect(40 + btnWidth + 10, 214, btnWidth, 120), "present interstital", myButtonStyle))
        {

            if (this.interstitialAd.IsReady())
            {
                this.interstitialAd.Show();
            }

        }

        //Yumi video
        if (GUI.Button(new Rect(40, 344, btnWidth, 120), "Load video", myButtonStyle))
        {

            if (this.rewardVideoAd == null)
            {
                this.rewardVideoAd = YumiRewardVideoAd.Instance;
                this.rewardVideoAd.OnAdOpening += HandleRewardVideoAdOpened;
                this.rewardVideoAd.OnAdStartPlaying += HandleRewardVideoAdStartPlaying;
                this.rewardVideoAd.OnAdRewarded += HandleRewardVideoAdReward;
                this.rewardVideoAd.OnRewardVideoAdClosed += HandleRewardVideoAdClosed;
                this.rewardVideoAd.OnAdLoaded += HandleRewardVideoAdLoaded;
                this.rewardVideoAd.OnAdFailedToLoad += HandleRewardVideoAdFailedToLoad;
                this.rewardVideoAd.OnAdFailedToShow += HandleRewardVideoAdFailedToShow;
                this.rewardVideoAd.OnAdClicked += HandleRewardVideoAdClicked;
            }
           

            this.rewardVideoAd.LoadAd(RewardedVideoPlacementId, ChannelId, GameVersionId);
        }

        if (GUI.Button(new Rect(40 + btnWidth + 10, 344, btnWidth, 120), "play video", myButtonStyle))
        {

            if (this.rewardVideoAd.IsReady())
            {
                this.rewardVideoAd.Play();
            }
        }

        //native

        if (GUI.Button(new Rect(40, 474, btnWidth, 120), "Show Native Scene", myButtonStyle))
        {
            destroyAds();
            SceneManager.LoadScene("YumiNativeDemoScene");

        }
        //splash

        if (GUI.Button(new Rect(40, 594, btnWidth, 120), "Request Splash", myButtonStyle))
        {
            if (splashAd == null)
            {
                YumiSplashOptionsBuilder builder = new YumiSplashOptionsBuilder().setAdBottomViewHeight(100);
                YumiSplashOptions splashOptions = new YumiSplashOptions(builder);
               
                splashAd = new YumiSplashAd(SplashPlacementId, ChannelId, GameVersionId, splashOptions);
                // add splash event
                splashAd.OnAdSuccessToShow += HandleSplashAdSuccssToShow;
                splashAd.OnAdFailedToShow += HandleSplashAdFailToShow;
                splashAd.OnAdClicked += HandleSplashAdClicked;
                splashAd.OnAdClosed += HandleSplashAdClosed;
            }

            splashAd.LoadAdAndShow();
        }
        // gdpr test
        if (GUI.Button(new Rect(40, 714, btnWidth * 2 , 120), gdprBtnInfo, myButtonStyle))
        {
            
            isPersonalized = !isPersonalized;
            if (isPersonalized)
            {
                YumiGDPRManager.Instance.UpdateNetworksConsentStatus(YumiConsentStatus.PERSONALIZED);
                gdprBtnInfo = "GDPR Consent Status is personalized"; 
            }
            else {
                YumiGDPRManager.Instance.UpdateNetworksConsentStatus(YumiConsentStatus.NONPERSONALIZED);
                gdprBtnInfo = "GDPR Consent Status is non personalized";
            }
        }

        if (YumiMediationSDKSetting.GetDebugMode)
        {
            if (GUI.Button(new Rect(40, 834, btnWidth, 120), "Call DebugCenter", myButtonStyle))
            {
                if (this.debugCenter == null)
                {
                    this.debugCenter = new YumiDebugCenter();
                }

                //Destroy ad
                destroyAds();
                this.debugCenter.PresentYumiMediationDebugCenter(BannerPlacementId, InterstitialsPlacementId, RewardedVideoPlacementId, NativeAdPlacementId, SplashPlacementId, ChannelId, GameVersionId);
            }
        }
    }

    private void destroyAds()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
            bannerView = null;
        }
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }
        if (rewardVideoAd != null)
        {
            rewardVideoAd = null;
        }
        if (splashAd != null)
        {
            splashAd.DestroySplashAd();
            splashAd = null;
        }
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

    public void HandleInterstitialAdFailedToShow(object sender, YumiAdFailedToShowEventArgs args)
    {
        Logger.Log("HandleInterstitialAdFailedToShow event received with message: " + args.Message);
    }
    public void HandleInterstitialAdOpened(object sender, EventArgs args)
    {
        Logger.Log("HandleInterstitialAdOpened  ad opened ");
    }
    public void HandleInterstitialAdStartPlaying(object sender, EventArgs args)
    {
        Logger.Log("HandleInterstitialAdStartPlaying event StartPlaying ");
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
    public void HandleRewardVideoAdClosed(object sender, YumiAdCloseEventArgs args)
    {
        Logger.Log("HandleRewardVideoAdClosed Ad closed result is  " + args.IsRewarded);
    }
    public void HandleRewardVideoAdLoaded(object sender, EventArgs args)
    {
        Logger.Log("HandleRewardVideoAdLoaded event received");
    }

    public void HandleRewardVideoAdFailedToLoad(object sender, YumiAdFailedToLoadEventArgs args)
    {
        Logger.Log("HandleRewardVideoAdFailedToLoad event received with message: " + args.Message);
    }

    public void HandleRewardVideoAdFailedToShow(object sender, YumiAdFailedToShowEventArgs args)
    {
        Logger.Log("HandleRewardVideoAdFailedToShow event with message: " + args.Message);
    }
    public void HandleRewardVideoAdClicked(object sender, EventArgs args)
    {
        Logger.Log("HandleRewardVideoAdClicked Clicked");
    }

    #endregion
    #region  splash callback handlers

    public void HandleSplashAdSuccssToShow(object sender, EventArgs args)
    {
        Logger.Log("HandleSplashSuccssToShow event success to show");
    }

    public void HandleSplashAdFailToShow(object sender, YumiAdFailedToShowEventArgs args)
    {
        Logger.Log("HandleSplashAdFailToShow + fail error is =  " +  args.Message);
    }

    public void HandleSplashAdClicked(object sender, EventArgs args)
    {
        Logger.Log("HandleSplashAdClicked clicked");
    }
    public void HandleSplashAdClosed(object sender, EventArgs args)
    {
        Logger.Log("HandleSplashAdClosed Ad closed ");
    }

    #endregion
}
