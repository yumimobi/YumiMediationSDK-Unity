using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using YumiMediationSDK.Api;
using YumiMediationSDK.Common;
using UnityEngine.SceneManagement;

public class YumiSDKDemo : MonoBehaviour
{

    private YumiBannerView bannerView;
    private YumiInterstitialAd interstitialAd;
    private YumiRewardVideoAd rewardVideoAd;
    private YumiDebugCenter debugCenter;

    private string BannerPlacementId = "";
    private string RewardedVideoPlacementId = "";
    private string InterstitialsPlacementId = "";
    private string NativeAdPlacementId = "";
    private string GameVersionId = "";
    private string ChannelId = "";

    private bool IsSmartBanner;


    void Start()
    {
        // Whether to display log log
        Logger.SetDebug(true);

        //get ad info
        GameVersionId = YumiMediationSDKSetting.GetGameVersion;
        IsSmartBanner = YumiMediationSDKSetting.GetAutomaticAdaptionBanner;

        ChannelId = YumiMediationSDKSetting.ChannelId();
        RewardedVideoPlacementId = YumiMediationSDKSetting.RewardVideoPlacementId();
        InterstitialsPlacementId = YumiMediationSDKSetting.InterstitialPlacementId();
        BannerPlacementId = YumiMediationSDKSetting.BannerPlacementId();
        NativeAdPlacementId = YumiMediationSDKSetting.NativeAdPlacementId();

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

        if (GUI.Button(new Rect(40, 84, btnWidth, 120), "request banner", myButtonStyle))
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
            this.bannerView.Show();

        }
        //remove banner
        if (GUI.Button(new Rect(40 + btnWidth + 10, 84, btnWidth, 120), "hide banner", myButtonStyle))
        {
            if (this.bannerView != null)
            {
                this.bannerView.Hide();
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
            SceneManager.LoadScene("YumiNativeScene");

        }

        if (YumiMediationSDKSetting.GetDebugMode)
        {
            if (GUI.Button(new Rect(40, 594, btnWidth, 120), "Call DebugCenter", myButtonStyle))
            {
                if (this.debugCenter == null)
                {
                    this.debugCenter = new YumiDebugCenter();
                }

                //Destroy ad
                destroyAds();
                this.debugCenter.PresentYumiMediationDebugCenter(BannerPlacementId, InterstitialsPlacementId, RewardedVideoPlacementId, NativeAdPlacementId, ChannelId, GameVersionId);
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
    public void HandleRewardVideoAdClosed(object sender, YumiAdCloseEventArgs args)
    {
        Logger.Log("HandleRewardVideoAdClosed Ad closed" + args.IsRewarded);
    }


    #endregion
}
